using System;

namespace Majority.RemittanceProvider.API.Models
{
    public class CommitTransactionRequest
    {
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }
        public string SenderAddress { get; set; }
        public string SenderCountry { get; set; }
        public string SenderCity { get; set; }
        public string SendFromState { get; set; }
        public string SenderPostalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ToFirstName { get; set; }
        public string ToLastName { get; set; }
        public string ToCountry { get; set; }
        public string ToBankAccountName { get; set; }
        public string ToBankAccountNumber { get; set; }
        public string ToBankName { get; set; }
        public string ToBankCode { get; set; }
        public string FromAmount { get; set; }
        public string ExchangeRate { get; set; }
        public string Fees { get; set; }
        public string TransactionNumber { get; set; }
        public string FromCurrency { get; set; }
    }
}
