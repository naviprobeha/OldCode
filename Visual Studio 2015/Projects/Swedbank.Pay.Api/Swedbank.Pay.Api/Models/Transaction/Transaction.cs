namespace Swedbank.Pay.Api.Models.Transaction
{
    using Newtonsoft.Json;

    public class Transaction
    {
        [JsonProperty("payment")]
        public string Payment { get; set; }

        [JsonProperty("transactions")]
        public Transactions Transactions { get; set; }
    }
}