namespace Swedbank.Pay.Api.Models.Transaction
{
    using Newtonsoft.Json;

    public class Transactions
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("transactionList")]
        public TransactionList[] TransactionList { get; set; }
    }
}