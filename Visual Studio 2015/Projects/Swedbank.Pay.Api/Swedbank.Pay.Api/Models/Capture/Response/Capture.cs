namespace Swedbank.Pay.Api.Models.Capture.Response
{
    using Newtonsoft.Json;

    public class Capture
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }
    }
}
