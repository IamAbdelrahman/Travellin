namespace Travellin.Travellin.Core.Shared
{
    public class PaginatedResult<TEntity>
    {
        public IEnumerable<TEntity> Items { get; set; }
        public PaginationMetaData MetaData { get; set; }
    }
}
