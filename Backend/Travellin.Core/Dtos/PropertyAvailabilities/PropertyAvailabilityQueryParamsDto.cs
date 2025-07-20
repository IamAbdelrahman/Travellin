namespace Travellin.Core.Dtos.PropertyAvailabilities
{
    public class PropertyAvailabilityQueryParamsDto : GetAllQueryDto
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
