using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Navipro.CashJet.AddIns
{


    public class Retain24StatusCollection : CollectionBase
    {
        public Retain24Status this[int index]
        {
            get { return (Retain24Status)List[index]; }
            set { List[index] = value; }
        }
        public int Add(Retain24Status retain24Status)
        {
            return (List.Add(retain24Status));
        }
        public int IndexOf(Retain24Status retain24Status)
        {
            return (List.IndexOf(retain24Status));
        }
        public void Insert(int index, Retain24Status retain24Status)
        {
            List.Insert(index, retain24Status);
        }
        public void Remove(Retain24Status retain24Status)
        {
            List.Remove(retain24Status);
        }
        public bool Contains(Retain24Status retain24Status)
        {
            return (List.Contains(retain24Status));
        }

    }


    public class Retain24Status
    {
        public string validateInfo;
        public int voucherType;
        public double balance;
        public double reservedBalance;
        public string currency;
        public double maxAmount;
        public bool addValue;
        public double minAddAmount;
        public double maxAddAmount;
        public int result;
        public string resultDescription;
        public int status;
        public DateTime expireDate;
        public bool requiresPin;
        public bool pinVerified;
        public double discountFactor;
        public double amount;
    }

    public class Retain24Wrapper
    {
        private RetainComObj.CardCOM cardCom;
        private RetainComObj.CardComInit cardComInit;

        public Retain24Wrapper(string serverAddress, string pathToCertificate, string password)
        {
            cardCom = new RetainComObj.CardCOM();
            cardComInit = new RetainComObj.CardComInit();

            cardComInit.server = serverAddress;

            //cardComInit.store = pathToCertificate;

            System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(pathToCertificate, password);
            if (!cardComInit.forceClientCert(cert))
            {
               throw new Exception("Retain24: Ogilltigt certifikat eller lösenord.");
            }

            RetainComObj.common.enuResult initStatus = cardCom.Init(cardComInit);

            if (initStatus != RetainComObj.common.enuResult.Ok)
            {
                throw new Exception("Retain24: Initiering misslyckades: " + initStatus.ToString() + ". Serveradress: " + serverAddress + ", Certifikatnamn: " + pathToCertificate);
            }



        }

        public Retain24StatusCollection validate(string number, string code)
        {
            Retain24StatusCollection collection = new Retain24StatusCollection();

            RetainComObj.CardComRetValidate[] validationStatus = cardCom.Validate(number, code);

            int i = 0;
            while (i < validationStatus.Length)
            {
                Retain24Status retain24Status = new Retain24Status();
                retain24Status.addValue = validationStatus[i].AddValue;
                retain24Status.balance = validationStatus[i].Balance;
                retain24Status.currency = validationStatus[i].Currency;
                retain24Status.discountFactor = validationStatus[i].DiscountFactor;
                retain24Status.expireDate = validationStatus[i].ExpDate;
                retain24Status.maxAddAmount = validationStatus[i].MaxAddAmount;
                retain24Status.maxAmount = validationStatus[i].MaxAmount;
                retain24Status.minAddAmount = validationStatus[i].MinAddAmount;
                retain24Status.pinVerified = validationStatus[i].PinVerified;
                retain24Status.requiresPin = validationStatus[i].RequiresPin;
                retain24Status.reservedBalance = validationStatus[i].ReservedBalance;
                retain24Status.result = (int)validationStatus[i].Result;
                retain24Status.resultDescription = validationStatus[i].Result_Desc;
                retain24Status.status = validationStatus[i].Status;
                retain24Status.validateInfo = validationStatus[i].ValidateInfo;
                retain24Status.voucherType = validationStatus[i].VoucherType;

                collection.Add(retain24Status);


            }

            return collection;

        }

        public bool addValue(string number, string code, double amount, out string resultDescription)
        {

            RetainComObj.CardComRetAddValue addValueResult = cardCom.AddValue(number, amount, code);

            resultDescription = addValueResult.ResultDesc;
            if (addValueResult.Result == RetainComObj.common.enuResult.Ok) return true;
            return false;
        }

        public Retain24Status redeem(string number, string code, double amount, string receiptNo)
        {            

            RetainComObj.CardComRetRedeem[] redeemStatus = cardCom.Redeem(number, code, amount, receiptNo);

            
            if (redeemStatus.Length > 0)
            {
                Retain24Status retain24Status = new Retain24Status();
                retain24Status.amount = redeemStatus[0].Amount;
                retain24Status.result = (int)redeemStatus[0].Result;
                retain24Status.resultDescription = redeemStatus[0].Result_Desc;

                return retain24Status;
            }

            return null;

        }

        public Retain24Status refund(string number, string code, double amount, string receiptNo)
        {

            RetainComObj.CardComRetRefund[] refundStatus = cardCom.Refund(number, code, amount, receiptNo);


            if (refundStatus.Length > 0)
            {
                Retain24Status retain24Status = new Retain24Status();
                retain24Status.amount = refundStatus[0].Amount;
                retain24Status.result = (int)refundStatus[0].Result;
                retain24Status.resultDescription = refundStatus[0].Result_Desc;

                return retain24Status;
            }

            return null;

        }

        public Retain24StatusCollection checkStatus(string number, string code)
        {
            Retain24StatusCollection collection = new Retain24StatusCollection();

            RetainComObj.CardComRetStatus[] status = cardCom.CheckStatus(number, code);


            int i = 0;
            while (i < status.Length)
            {
                Retain24Status retain24Status = new Retain24Status();

                retain24Status.balance = status[i].Balance;
                retain24Status.currency = status[i].Currency;
                retain24Status.expireDate = status[i].UsageEnd;
                retain24Status.maxAmount = status[i].MaxAmount;
                retain24Status.reservedBalance = status[i].ReservedBalance;
                retain24Status.result = (int)status[i].Result;
                retain24Status.resultDescription = status[i].ResultDesc;
                retain24Status.voucherType = status[i].VoucherType;

                collection.Add(retain24Status);
                i++;
            }

            return collection;

        }


    }
}
