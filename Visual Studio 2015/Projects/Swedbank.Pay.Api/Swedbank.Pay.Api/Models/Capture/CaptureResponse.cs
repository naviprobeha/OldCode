namespace Swedbank.Pay.Api.Models.Capture
{
    using Newtonsoft.Json;
    using Response;

    public class CaptureResponse
    {
        [JsonProperty("payment")]
        public string Payment { get; set; }

        [JsonProperty("capture")]
        public Capture Capture { get; set; }
    }
}
