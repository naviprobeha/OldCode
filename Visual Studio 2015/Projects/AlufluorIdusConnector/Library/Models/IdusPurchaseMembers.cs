using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.Alufluor.Idus.Library.Models
{
    public class IdusPurchaseMembers
    {
        public string FPurchaseNr { get; set; }
        public string FDate { get; set; }

        public string FSupplier { get; set; }
        public string FSupplierAddress { get; set; }
        public string FSupplierAddress2 { get; set; }
        public string FSupplierPostNr { get; set; }
        public string FSupplierPostalArea { get; set; }
        public string FReceiver { get; set; }
        public string FOrderer { get; set; }

        public IdusPurchaseLineObjectList m_PurchaseItems { get; set; }


        public List<BCPurchaseLine> toBCPurchaseLine(List<BCPurchaseLine> purchLineList)
        {

            int i = 0;
            while (i < m_PurchaseItems.fields.FItems.Count)
            {
                BCPurchaseLine purchLine = new BCPurchaseLine();
                purchLine.no = FPurchaseNr;
                purchLine.lineNo = (i+1) * 1000;
                purchLine.orderDate = DateTime.Parse(FDate);
                purchLine.vendorNo = "";
                purchLine.vendorName = Uri.UnescapeDataString(FSupplier);
                purchLine.vendorAddress = Uri.UnescapeDataString(FSupplierAddress);
                purchLine.vendorAddress2 = Uri.UnescapeDataString(FSupplierAddress2);
                purchLine.vendorPostCode = FSupplierPostNr;
                purchLine.vendorCity = Uri.UnescapeDataString(FSupplierPostalArea);
                purchLine.receiver = Uri.UnescapeDataString(FReceiver);
                purchLine.orderer = Uri.UnescapeDataString(FOrderer);

                purchLine.description = Uri.UnescapeDataString(m_PurchaseItems.fields.FItems[i].fields.FDesignation);
                purchLine.quantity = m_PurchaseItems.fields.FItems[i].fields.FAmount;
                purchLine.unitPrice = m_PurchaseItems.fields.FItems[i].fields.FPrice;
                purchLine.dimension1Value = m_PurchaseItems.fields.FItems[i].fields.FAccount;
                purchLine.dimension2Value = m_PurchaseItems.fields.FItems[i].fields.FAccount2;
                purchLine.dimension3Value = m_PurchaseItems.fields.FItems[i].fields.FAccount3;
                purchLine.dimension4Value = m_PurchaseItems.fields.FItems[i].fields.FAccount4;
                purchLine.dimension5Value = m_PurchaseItems.fields.FItems[i].fields.FAccount5;
                purchLine.dimension6Value = m_PurchaseItems.fields.FItems[i].fields.FAccount6;
                purchLine.dimension7Value = m_PurchaseItems.fields.FItems[i].fields.FAccount7;
                purchLine.dimension8Value = "";

                purchLineList.Add(purchLine);
                
                i++;
            }


            return purchLineList;
            
        }
    }
}
