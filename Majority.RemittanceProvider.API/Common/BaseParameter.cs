namespace Majority.RemittanceProvider.API.Common
{
    public class BaseParameter
    {
    }
    public enum ResponseCode
    {

        Success = 200,
        Created = 201,
        InvalidRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        Failed = 440,
        ServiceUnavailable = 503
    }

    public enum TransactionStatus
    {

        Completed = 200,
        Pending = 201,
        Canceled = 202,
        Declined = 203
    }

    public enum Status
    {

        Completed ,
        Pending ,
        Canceled ,
        Declined 
    }

    public enum Codes
    {

        Success,
        InvalidRequest,
        Unauthorized,
        Forbidden,
        Failed,
        ServiceUnavailable
    }
}
