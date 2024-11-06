namespace Swedbank.Pay.Api.Models.PaymentResource
{
    using Newtonsoft.Json;

    public class Authorizations
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}