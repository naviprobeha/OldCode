namespace Swedbank.Pay.Api.Services
{
    using Models.Capture;
    using Models.PaymentResource;
    using Models.Transaction;

    public interface ISwedbankService
    {
        PaymentResource GetPaymentResource(string id);

        Transaction GetTransactions(string id);

        CaptureResponse Capture(CaptureRequest request);
    }
}
