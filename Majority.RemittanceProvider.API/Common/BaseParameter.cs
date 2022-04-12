namespace Majority.RemittanceProvider.API.Common
{
    public class BaseParameter
    {
    }
    public enum ResponseCode
    {

        Success = 200,
        //Success = 201,
        InvalidRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        Failed = 440,
        ServiceUnavailable = 503
    }
}
