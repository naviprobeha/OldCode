using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmyckaReceiptService.Models
{
    public class POSTransactionHeader
    {
        public string id { get; set; } = "";
        public string no { get; set; } = "";
        public int transactionType { get; set; } = 0;//Receipt,System Transaction,Return Receipt
        public int state { get; set; } = 0;
        public string memberNo { get; set; } = "";
        public string memberName { get; set; } = "";
        public string memberPhoneNo { get; set; } = "";
        public string memberEmail { get; set; } = "";
        public string posStoreCode { get; set; } = "";
        public string posDeviceID { get; set; } = "";

        public DateTime registeredDateTime { get; set; }
        public string userId { get; set; } = "";

        public int printed { get; set; } = 0;
        public string noSeriesCode { get; set; } = "";
        public string registeredTransactionNo { get; set; } = "";
        public bool registered { get; set; } = false;
        public bool closed { get; set; } = false;
        public bool acknowledged { get; set; } = false;
        public int erpStatus { get; set; } = 0;
        public int posTransactionJournalEntryNo { get; set; } = 0;
        public string controlNo { get; set; } = "";
        public string controlNoCopy { get; set; } = "";
        public string unitId { get; set; } = "";
        public string klarnaOrderId { get; set; } = "";
        public bool klarnaPayment { get; set; } = false;
        public decimal klarnaAmount { get; set; } = 0;

        public bool createOrder { get; set; } = false;
        public string orderName { get; set; } = "";
        public string orderAddress { get; set; } = "";
        public string orderPostCode { get; set; } = "";
        public string orderCity { get; set; } = "";
        public string orderCountry { get; set; } = "";
        public string orderPhoneNo { get; set; } = "";
        public string orderEmail { get; set; } = "";

        public decimal totalQuantity { get; set; } = 0;
        public decimal totalAmountInclVat { get; set; } = 0;

        public string invoiceCustomerNo { get; set; } = "";
        public string invoiceName { get; set; } = "";
        public string invoiceAddress { get; set; } = "";
        public string invoiceAddress2 { get; set; } = "";
        public string invoicePostCode { get; set; } = "";
        public string invoiceCity { get; set; } = "";
        public string invoiceCountryCode { get; set; } = "";
        public string invoicePhoneNo { get; set; } = "";
        public string invoiceEmail { get; set; } = "";
        public string invoiceContactName { get; set; } = "";
        public string invoiceVatRegNo { get; set; } = "";
        public string invoiceRegNo { get; set; } = "";
        public string invoiceReferenceNo { get; set; } = "";
        public string invoicePersonalNo { get; set; } = "";

        public string currencyCode { get; set; } = "";
        public decimal currencyFactor { get; set; } = 0;

        public string salesPersonName { get; set; } = "";
        public bool newCustomer { get; set; }


        public Store store { get; set; }

        public List<POSTransactionLine> lines { get; set; }


        public Dictionary<string, POSTransactionLine> getVatList()
        {
            Dictionary<string, POSTransactionLine> vatTable = new Dictionary<string, POSTransactionLine>();

            foreach(POSTransactionLine posTransactionLine in lines)
            {
                if (posTransactionLine.lineType == 0)
                {
                    if (vatTable.ContainsKey(posTransactionLine.vatProc.ToString()))
                    {
                        POSTransactionLine vatLine = vatTable[posTransactionLine.vatProc.ToString()];
                        vatLine.amount = vatLine.amount + posTransactionLine.amount;
                        vatLine.amountInclVAT = vatLine.amountInclVAT + posTransactionLine.amountInclVAT;
                        vatLine.vatAmount = vatLine.vatAmount + posTransactionLine.vatAmount;
                        vatTable[posTransactionLine.vatProc.ToString()] = vatLine;
                    }
                    else
                    {
                        POSTransactionLine vatLine = new POSTransactionLine();
                        vatLine.vatProc = posTransactionLine.vatProc;
                        vatLine.amount = vatLine.amount + posTransactionLine.amount;
                        vatLine.amountInclVAT = vatLine.amountInclVAT + posTransactionLine.amountInclVAT;
                        vatLine.vatAmount = vatLine.vatAmount + posTransactionLine.vatAmount;
                        vatTable.Add(posTransactionLine.vatProc.ToString(), vatLine);
                    }
                }

            }


            return vatTable;
        }
    }
}