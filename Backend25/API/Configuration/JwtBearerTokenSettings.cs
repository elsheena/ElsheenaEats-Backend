namespace API.Configuration
{
    public class JwtBearerTokenSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
