namespace Swedbank.Pay.Api.Models.PaymentResource
{
    using System;
    using Newtonsoft.Json;

    public class Payment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("updated")]
        public DateTimeOffset Updated { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("remainingCaptureAmount")]
        public long RemainingCaptureAmount { get; set; }

        [JsonProperty("remainingCancellationAmount")]
        public long RemainingCancellationAmount { get; set; }

        [JsonProperty("remainingReversalAmount")]
        public long RemainingReversalAmount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("payerReference")]
        public string PayerReference { get; set; }

        [JsonProperty("initiatingSystemUserAgent")]
        public string InitiatingSystemUserAgent { get; set; }

        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("prices")]
        public Authorizations Prices { get; set; }

        [JsonProperty("payeeInfo")]
        public Authorizations PayeeInfo { get; set; }

        [JsonProperty("urls")]
        public Authorizations Urls { get; set; }

        [JsonProperty("transactions")]
        public Authorizations Transactions { get; set; }

        [JsonProperty("authorizations")]
        public Authorizations Authorizations { get; set; }

        [JsonProperty("captures")]
        public Authorizations Captures { get; set; }

        [JsonProperty("reversals")]
        public Authorizations Reversals { get; set; }

        [JsonProperty("cancellations")]
        public Authorizations Cancellations { get; set; }
    }
}