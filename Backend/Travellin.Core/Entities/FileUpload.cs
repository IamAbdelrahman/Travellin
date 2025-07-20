namespace Travellin.Core.Entities
{
    public class FileUpload : BaseEntity<string>
    {
        public string Path { get; set; }
        public virtual PropertyPhoto? PropertyPhoto { get; set; }
    }
}
