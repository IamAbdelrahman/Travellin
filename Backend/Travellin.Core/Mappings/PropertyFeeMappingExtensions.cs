using Travellin.Core.Dtos.PropertyFees;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertyFeeMappingExtensions
    {
        public static PropertyFeeDto ToDto(this PropertyFee propertyFee)
        {
            return new PropertyFeeDto
            {
                Id = propertyFee.Id,
                Name = propertyFee.Name,
                Amount = propertyFee.Amount,
                PropertyId = propertyFee.PropertyId
            };
        }
    }
}
