using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Infrastructure.Shared
{
    public static class ErrorMessages
    {
        public const string PropertyNotFound = "Property not found";
        public const string PropertyUpdate = "You don't have permission to modify this property";
        public const string BookingUnavailable = "The property is not available for selected dates";
        public const string PropertyDelete = "You don't have permission to delete this property";
        public const string GuestCreate = "You don't have permission to add guests to this property";
        public const string GuestUpdate = "You don't have permission to update guests for this property";
        public const string GuestDelete = "You don't have permission to delete guests from this property";
        public const string PhotoUpload = "You don't have permission to upload photos to this property";
        public const string PhotoReorder = "You don't have permission to reorder photos for this property";
        public const string PhotoDelete = "You don't have permission to delete photos from this property";
        public const string PhotoUploadFailed = "Photo upload failed";
        public const string PhotoNotFound = "Photo not found";
        public const string PropertyAmenitiesAdd = "You don't have permission to add amenities for this property";
        public const string PropertyAmenitiesUpdate = "You don't have permission to update amenities for this property";
        public const string PropertyAmenitiesDelete = "You don't have permission to delete amenities for this property";
        public const string PropertyAvailabilityAdd = "You don't have permission to add availability for this property";
        public const string PropertyAvailabilityUpdate = "You don't have permission to update availability for this property";
        public const string PropertyAvailabilityDelete = "You don't have permission to delete availability for this property";
        public const string PropertySpaceAdd = "You don't have permission to add space for this property";
        public const string PropertySpaceUpdate = "You don't have permission to update space for this property";
        public const string PropertySpaceDelete = "You don't have permission to delete space from this property";
    }

}
