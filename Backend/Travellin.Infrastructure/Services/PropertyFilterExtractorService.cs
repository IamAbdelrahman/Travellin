using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Properties;
using Travellin.Core.Interfaces;
using OpenAI.Chat;
using System.Text.Json;

namespace Travellin.Infrastructure.Services
{
    public class PropertyFilterExtractorService : IPropertyFilterExtractorService
    {
        private readonly ChatClient _openAiChatClient;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyFilterExtractorService(
            ChatClient openAiChatClient,
            IUnitOfWork unitOfWork)
        {
            _openAiChatClient = openAiChatClient;
            _unitOfWork = unitOfWork;
        }

        public async Task<FilterPropertyQueryParamsDto> ExtractFiltersAsync(string naturalLanguageQuery)
        {
            if (string.IsNullOrWhiteSpace(naturalLanguageQuery))
            {
                return new FilterPropertyQueryParamsDto();
            }

            try
            {
                // Create Chat Messages
                List<ChatMessage> messages = new()
                {
                    new SystemChatMessage(await GetSystemPrompt()),
                    new UserChatMessage(naturalLanguageQuery)
                };

                // Create Chat Completetion
                ChatCompletion completion = await _openAiChatClient.CompleteChatAsync(messages);

                // Extract the AI's message (first response)
                string? jsonResponse = completion.Content[0].Text;

                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    throw new Exception("AI response was empty.");
                }

                // Parse the JSON result
                return ParseAiResponse(jsonResponse);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error extracting filters from natural language\nDetails: {ex.Message}");
            }
        }

        private async Task<string> GetSystemPrompt()
        {
            // Get regions for the prompt
            var regions = (await _unitOfWork.RegionRepository.GetAllAsync(
                new GetAllQueryDto { Page = 1, PageSize = int.MaxValue },
                q => q.OrderBy(x => x.Id)))
                .Items.Select(x => new { x.Id, x.Name });

            var propertyTypes = (await _unitOfWork.PropertyTypeRepository.GetAllAsync(
                new GetAllQueryDto { Page = 1, PageSize = int.MaxValue },
                q => q.OrderBy(x => x.Id)))
                .Items.Select(x => new { x.Id, x.Name });


            var regionsList = string.Join("\n", regions.Select(r => $"- {r.Id}: {r.Name}"));

            var propertyTypeList = string.Join("\n", propertyTypes.Select(r => $"- {r.Id}: {r.Name}"));

            var today = DateOnly.FromDateTime(DateTime.UtcNow).ToString("yyyy-MM-dd");

                    return $@"
        You are an assistant that extracts property search filters from natural language queries.

        TODAY'S DATE IS: {today}

        Use this as the reference point when calculating any relative date expressions like 'next week', 'tomorrow', 'in 3 days', etc.

        Available Regions:
        {regionsList}

        Available PropertyTypes:
        {propertyTypeList}

        Extract ONLY the following information if mentioned in the query:
        1. Country name (as string, don't convert to ID)
        2. Region ID (from the available regions list)
        3. Check-in date (format: YYYY-MM-DD)
        4. Check-out date (format: YYYY-MM-DD)
        5. Guest count (as number) — this is the total of all people, including adults, children, infants, and pets
        6. Sort preference (one of: 'price_asc', 'price_desc', 'rating')
        7. PropertyTypeId (as number)

        Return a VALID JSON object with ONLY these fields (all nullable):
        {{
          ""countryName"": string,
          ""regionId"": number,
          ""checkIn"": string,
          ""checkOut"": string,
          ""guestCount"": number,
          ""sort"": string,
          ""propertyTypeId"": int,
          ""priceMin"": decimal,
          ""priceMax"": decimal
        }}

        Important rules:
        - For prices:
          * Extract numerical values after terms like 'under $X', 'below €Y', 'more than £Z'
          * Convert all currencies to numbers (ignore currency symbols)
          * For ranges: 'between X and Y' → priceMin: X, priceMax: Y
          * For single limits: 'under 500' → priceMax: 500
          * 'cheap' → priceMax: 100 (example threshold)
          * 'luxury' → priceMin: 500 (example threshold)
        - Only include fields that are explicitly mentioned
        - Use the exact field names specified
        - For dates, use YYYY-MM-DD format
        - For regions, only use IDs from the provided list
        - For property types, only use IDs from the provided list
        - If a country name has a typo or is similar to an actual country name, assume it's a match to the closest available country name
        - For regions and property types, if a name has a typo or is similar to an item in the list, assume it's a match to the closest available option and return the corresponding ID from the list
        - Never invent or guess values that aren't in the provided lists
        - ALWAYS return valid JSON

        Price Extraction Examples:
        - ""Apartments under $200"" → priceMax: 200
        - ""Villas between €1000 and €2000"" → priceMin: 1000, priceMax: 2000
        - ""Cheap hostels"" → priceMax: 100
        - ""Luxury resorts over $500"" → priceMin: 500
        - ""Properties around 300-400 dollars"" → priceMin: 300, priceMax: 400

        Location Matching Priority:
        1. First look for explicit country names
        2. If no country found, look for region-like terms and match to closest region ID
        3. If neither is clearly specified, leave both fields null

        Examples:
        - ""Villas in Dubai"" → regionId for Dubai (if in list), countryName: ""United Arab Emirates""
        - ""Middle East vacations"" → regionId for closest matching Middle East region
        - ""France apartments"" → countryName: ""France""
        - ""Paris hotels"" → countryName: ""France"" (assuming Paris is in France)

        IMPORTANT:
        - DO NOT wrap the JSON response in triple backticks or any markdown formatting.
        - Just return a raw JSON object.";
        }

        private FilterPropertyQueryParamsDto ParseAiResponse(string jsonResponse)
        {
            try
            {
                string cleanJson = jsonResponse.Trim();

                if (cleanJson.StartsWith("```"))
                {
                    int firstNewline = cleanJson.IndexOf('\n');
                    int lastBacktick = cleanJson.LastIndexOf("```", StringComparison.Ordinal);

                    if (firstNewline != -1 && lastBacktick > firstNewline)
                    {
                        cleanJson = cleanJson.Substring(firstNewline + 1, lastBacktick - firstNewline - 1).Trim();
                    }
                }

                using var doc = JsonDocument.Parse(cleanJson);
                var root = doc.RootElement;

                return new FilterPropertyQueryParamsDto
                {
                    CountryName = root.TryGetProperty("countryName", out var cn) ? cn.GetString() : null,
                    RegionId = root.TryGetProperty("regionId", out var ri) && ri.ValueKind != JsonValueKind.Null ? ri.GetInt32() : null,
                    PropertyTypeId = root.TryGetProperty("propertyTypeId", out var pt) && pt.ValueKind != JsonValueKind.Null ? pt.GetInt32() : null,
                    CheckIn = TryGetNullableDateOnly(root, "checkIn"),
                    CheckOut = TryGetNullableDateOnly(root, "checkOut"),
                    GuestCount = root.TryGetProperty("guestCount", out var gc) && gc.ValueKind != JsonValueKind.Null ? gc.GetInt32() : null,
                    Sort = root.TryGetProperty("sort", out var s) ? s.GetString() : null,
                    PriceMin = root.TryGetProperty("priceMin", out var pmin) && pmin.ValueKind != JsonValueKind.Null ? pmin.GetDecimal() : null,
                    PriceMax = root.TryGetProperty("priceMax", out var pmax) && pmax.ValueKind != JsonValueKind.Null ? pmax.GetDecimal() : null
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse AI response: {jsonResponse}\nError: {ex.Message}", ex);
            }
        }

        private DateOnly? TryGetNullableDateOnly(JsonElement root, string propertyName)
        {
            if (root.TryGetProperty(propertyName, out var prop) &&
                prop.ValueKind == JsonValueKind.String &&
                DateOnly.TryParse(prop.GetString(), out var date))
            {
                return date;
            }

            return null;
        }

    }
}