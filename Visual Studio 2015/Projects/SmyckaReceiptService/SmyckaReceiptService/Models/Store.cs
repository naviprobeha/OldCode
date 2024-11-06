using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmyckaReceiptService.Models
{
    public class Store
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";

        public string address { get; set; } = "";

        public string postalAddress { get; set; } = "";


        public string countryCode { get; set; } = "";

        public string registrationNo { get; set; } = "";

        public string vatRegistrationNo { get; set; } = "";

        public string phoneNo { get; set; } = "";

        public string email { get; set; } = "";

        public string homePage { get; set; } = "";

        public string salesLocationCode { get; set; } = "";
        public string orderLocationCode { get; set; } = "";
        public string currencyCode { get; set; } = "";


        public byte[] logo { get; set; }

        public int receiptItemIdentification { get; set; } = 0;

        public string receiptTextLine1 { get; set; } = "";
        public string receiptTextLine2 { get; set; } = "";
        public string receiptTextLine3 { get; set; } = "";
        public string receiptTextLine4 { get; set; } = "";
        public string receiptTextLine5 { get; set; } = "";
        public string receiptTextLine6 { get; set; } = "";
        public string receiptTextLine7 { get; set; } = "";
        public string receiptTextLine8 { get; set; } = "";
        public string receiptTextLine9 { get; set; } = "";
        public string receiptTextLine10 { get; set; } = "";

        public bool showBarCodeOnReceipt { get; set; } = true;
        public bool showBarCodeOnVoucher { get; set; } = true;

        public bool showLogoOnVoucher { get; set; } = true;

        public string voucherTextLine1 { get; set; } = "";
        public string voucherTextLine2 { get; set; } = "";

        public string creditReceiptTextLine1 { get; set; } = "";
        public string creditReceiptTextLine2 { get; set; } = "";

        public string mainButtonPanelCode { get; set; } = "";

        public string managerButtonPanelCode { get; set; } = "";

        public string closingButtonPanelCode { get; set; } = "";

        public string paymentButtonPanelCode { get; set; } = "";

        public string memberFormCode { get; set; }
        public int displayUnitPrice { get; set; } = 0;

        public int maxQuantityAllowed { get; set; } = 1000;

        public string creditReceiptTypeCode { get; set; }

        public bool askCreditReceiptNegativeAmount { get; set; } = true;

        public string floatPaymentTypeCode { get; set; }

        public bool allowCustomerRefScanning { get; set; } = false;

        public decimal roundingPrecision { get; set; } = 1;

        public string klarnaMerchantId { get; set; } = "";

        public string klarnaUserName { get; set; } = "";

        public string klarnaPassword { get; set; } = "";

        public string klarnaPaymentTypeCode { get; set; } = "";

        public string vatBusPostingGroup { get; set; } = "";
        public string adyenMemberQuestion1 { get; set; } = "";
        public string adyenMemberQuestion2 { get; set; } = "";
        public string adyenMemberInput { get; set; } = "";
        public bool sendElectronicReceiptsToKnownCustomers { get; set; }
        public string smsSenderId { get; set; } = "";
        public string smsReceiptMessage { get; set; } = "";

        public string virtualReceiptHeader { get; set; } = "";
        public string virtualReceiptFormTitle { get; set; } = "";
        public string virtualReceiptFormHelpText { get; set; } = "";
        public string virtualReceiptFormSubmitButton { get; set; } = "";
        public string virtualReceiptFormCode { get; set; } = "";

        public string prepaymentRequestType { get; set; } = "";
        public string prepaymentRequestUrl { get; set; } = "";


        public string companyName { get; set; }

    }
}