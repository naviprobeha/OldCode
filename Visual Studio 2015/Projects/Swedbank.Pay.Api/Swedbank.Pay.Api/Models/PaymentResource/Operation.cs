namespace Swedbank.Pay.Api.Models.PaymentResource
{
    using System;
    using Newtonsoft.Json;

    public class Operation
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}