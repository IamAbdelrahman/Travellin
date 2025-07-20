namespace Travellin.Core.Entities
{
    public class FavoriteProperty
    {
        public string PropertyId { get; set; }
        public string UserId { get; set; }

        public virtual Property Property { get; set; }
        public virtual AppUser User { get; set; }
    }
}
