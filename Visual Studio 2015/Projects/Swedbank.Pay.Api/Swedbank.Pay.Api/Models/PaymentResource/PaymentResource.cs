namespace Swedbank.Pay.Api.Models.PaymentResource
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class PaymentResource
    {
        [JsonProperty("payment")]
        public Payment Payment { get; set; }

        [JsonProperty("operations")]
        public List<Operation> Operations { get; set; }
    }
}