namespace Travellin.Travellin.Infrastructure.Shared
{

    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }
        public string SigningKey { get; set; }
    }

}
