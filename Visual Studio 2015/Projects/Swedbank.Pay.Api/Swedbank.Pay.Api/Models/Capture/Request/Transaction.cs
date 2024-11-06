namespace Swedbank.Pay.Api.Models.Capture.Request
{
    using Newtonsoft.Json;

    public class Transaction
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("vatAmount")]
        public int VatAmount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("payeeReference")]
        public string PayeeReference { get; set; }
    }
}
