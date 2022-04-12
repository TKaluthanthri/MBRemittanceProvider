namespace Majority.RemittanceProvider.API.Models
{
    public class ApplicationConfigurations
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientName { get; set; }
        public string ClientScope { get; set; }
        public string AuthorizeUrl { get; set; }
        public string TokenIssuer { get; set; }
    }
}
