using Majority.RemittanceProvider.API.Common;

namespace Majority.RemittanceProvider.API.Models
{
    public class GenericUseCaseResult
    {
        public string Status { get; set; }
        public int HttpStatusCode { get; set; }
        public dynamic Result { get; set; }
    }
}
