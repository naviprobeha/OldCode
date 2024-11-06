namespace Swedbank.Pay.Api.Models.Transaction
{
    using System;
    using Newtonsoft.Json;

    public class TransactionList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("updated")]
        public DateTimeOffset Updated { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("vatAmount")]
        public long VatAmount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("payeeReference")]
        public Guid PayeeReference { get; set; }

        [JsonProperty("isOperational")]
        public bool IsOperational { get; set; }

        [JsonProperty("operations")]
        public object[] Operations { get; set; }
    }
}