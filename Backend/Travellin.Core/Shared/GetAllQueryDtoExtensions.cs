using Travellin.Core.Dtos;

namespace Travellin.Travellin.Core.Shared
{
    public static class GetAllQueryDtoExtensions
    {
        public static int CalcSkippedItems(this GetAllQueryDto dto)
        {
            return (dto.Page - 1) * dto.PageSize;
        }
    }
}
