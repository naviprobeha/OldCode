namespace Swedbank.Pay.Api.Models.Capture
{
    using Newtonsoft.Json;
    using Request;

    public class CaptureRequest
    {
        public CaptureRequest()
        {
            this.Transaction = new Transaction();
        }

        [JsonIgnore]
        public string OrderId { get; set; }

        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }
    }
}
