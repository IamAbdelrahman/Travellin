namespace Travellin.Core.Entities
{
    public class Region : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Country> Countries { get; set; } = new HashSet<Country>();
    }
}
