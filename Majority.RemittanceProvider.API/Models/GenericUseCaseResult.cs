namespace Majority.RemittanceProvider.API.Models
{
    public class GenericUseCaseResult
    {
        public bool IsSuccess { get; set; }
        public int HttpStatusCode { get; set; }
        public dynamic Result { get; set; }
    }
}
