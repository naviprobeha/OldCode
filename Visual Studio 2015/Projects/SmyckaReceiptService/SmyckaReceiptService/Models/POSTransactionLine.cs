using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmyckaReceiptService.Models
{
    public class POSTransactionLine
    {
        
        public string id { get; set; } = "";
        public string transactionNo { get; set; } = "";
        public int lineNo { get; set; } = 0;
        public int state { get; set; } = 0;
        public int transactionType { get; set; } = 0;
        public int lineType { get; set; } = 0;
        public int salesType { get; set; } = 0;
        public string salesNo { get; set; } = "";
        public string variantCode { get; set; } = "";
        public string description { get; set; } = "";
        public string description2 { get; set; } = "";
        public string unitOfMeasureCode { get; set; } = "";
        public string itemCategoryCode { get; set; } = "";
        public string productGroupCode { get; set; } = "";
        public decimal quantity { get; set; } = 0;
        public decimal unitPrice { get; set; } = 0;
        public decimal unitPriceInclVAT { get; set; } = 0;
        public decimal discountProc { get; set; } = 0;
        public decimal lineDiscountAmount { get; set; } = 0;
        public decimal vatProc { get; set; } = 0;
        public decimal vatAmount { get; set; } = 0;
        public decimal amount { get; set; } = 0;
        public decimal amountInclVAT { get; set; } = 0;
        public decimal presentationAmount { get; set; } = 0;
        public decimal presentationAmountInclVAT { get; set; } = 0;
        public int paymentType { get; set; } = 0;
        public string paymentReferenceNo { get; set; } = "";
        public string voucherTypeCode { get; set; } = "";
        public string voucherNo { get; set; } = "";
        public int voucherLedgerEntryNo { get; set; } = 0;
        public string paymentCode { get; set; } = "";
        public string returnReceiptNo { get; set; } = "";
        public int returnReceiptLineNo { get; set; } = 0;
        public string returnPOSDeviceID { get; set; } = "";
        public string returnReasonCode { get; set; } = "";
        public string returnReasonDescription { get; set; } = "";
        public decimal totalDiscountProc { get; set; } = 0;
        public decimal totalDiscountAmount { get; set; } = 0;
        public decimal lineDiscountProc { get; set; } = 0;
        public string discountCode { get; set; } = "";
        public int indent { get; set; } = 0;
        public string posStoreCode { get; set; } = "";
        public string posDeviceID { get; set; } = "";
        public bool registered { get; set; } = false;
        public bool deleted { get; set; } = false;
        public bool closed { get; set; } = false;
        public int posTransactionJournalEntryNo { get; set; } = 0;
        public string vatProdPostingGroup { get; set; } = "";
        public string genProdPostingGroup { get; set; } = "";
        public bool unitPriceNotBound { get; set; } = false;
        public bool manualDiscount { get; set; } = false;
        public int discountType { get; set; } = 0;
        public bool mixMatch { get; set; } = false;
        public decimal mixMatchDiscountAmount { get; set; } = 0;
        public decimal mixMatchOriginalDiscountProc { get; set; } = 0;
        public decimal mixMatchDiscountProc { get; set; } = 0;
        public decimal mixMatchOriginalDiscAmount { get; set; } = 0;
        public int mixMatchReceiptID { get; set; } = 0;
        public bool hide { get; set; } = false;
        public int parentLineNo { get; set; } = 0;
        public DateTime registeredDateTime { get; set; } = new DateTime(1970, 1, 1);
        public string memberNo { get; set; } = "";
        public int prepaymentType { get; set; } = 0;
        public string prepaymentNo { get; set; } = "";
        public bool noReturn { get; set; } = false;
        public string locationCode { get; set; } = "";

        public string deliveryMethodCode { get; set; } = "";
        public string deliveryMethodDescription { get; set; } = "";
        public int deliveryMethodType { get; set; } = 0;

        public string baseColorCode { get; set; } = "";

        public string baseColorName { get; set; } = "";
        public string userId { get; set; } = "";

        public decimal unitCost { get; set; } = 0;
        public decimal costAmount { get; set; } = 0;


        public decimal lcyUnitPrice { get; set; } = 0;
        public decimal lcyUnitPriceInclVAT { get; set; } = 0;
        public decimal lcyLineDiscountAmount { get; set; } = 0;
        public decimal lcyVatAmount { get; set; } = 0;
        public decimal lcyAmount { get; set; } = 0;
        public decimal lcyAmountInclVAT { get; set; } = 0;
        public string currencyCode { get; set; } = "";


        public string discountText { get; set; }
    }
}