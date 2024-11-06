using System;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// OrderItem illustrerar en post i varukorgen i olika lägen. Varukorgen läses ut i OrderItems ifrån klassen SalesId.
    /// </summary>
    public class OrderItem
    {
        private int _lineNo;
        private string _itemNo;
        private string _description;
        private float _unitPrice;
        private float _quantity;
        private float _quantity2;
        private float _quantity3;
        private float _quantityShowCase;
        private float _quantityToOrder;
        private float _soldQuantity;
        private float _remainingQuantity;
        private float _amount;
        private string _salesId;
        private int _method;

        /// <summary>
        /// Konstruktor som bygger upp en post utifrån en rad i varukorgen.
        /// </summary>
        /// <param name="webCartLine">Motsvarar en rad i varukorgen.</param>
        public OrderItem(WebCartLine webCartLine)
        {
            //
            // TODO: Add constructor logic here
            //
            _lineNo = webCartLine.entryNo;
            _itemNo = webCartLine.itemNo;
            _unitPrice = webCartLine.unitPrice;
            _quantity = webCartLine.quantity;
            _amount = webCartLine.amount;
            _salesId = webCartLine.extra1;
        }

        /// <summary>
        /// Tom konstruktor.
        /// </summary>
        public OrderItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Radnr i varukorgen.
        /// </summary>
        public int lineNo
        {
            get
            {
                return _lineNo;
            }
        }

        /// <summary>
        /// Artikelnr.
        /// </summary>
        public string itemNo
        {
            get
            {
                return _itemNo;
            }
            set
            {
                _itemNo = value;
            }
        }

        /// <summary>
        /// Beskrivning.
        /// </summary>
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// Enhetspris.
        /// </summary>
        public float unitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                _unitPrice = value;
            }
        }

        /// <summary>
        /// Antal.
        /// </summary>
        public float quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }

        /// <summary>
        /// Ytterligare antal. Ex. antal förpackningsmaterial.
        /// </summary>
        public float quantity2
        {
            get
            {
                return _quantity2;
            }
            set
            {
                _quantity2 = value;
            }
        }

        /// <summary>
        /// Ytterligare antal. Ex. antal förpackningsmaterial.
        /// </summary>
        public float quantity3
        {
            get
            {
                return _quantity3;
            }
            set
            {
                _quantity3 = value;
            }
        }

        /// <summary>
        /// Fördelningsmetod.
        /// </summary>
        public int method
        {
            get
            {
                return _method;
            }
            set
            {
                _method = value;
            }
        }

        /// <summary>
        /// Belopp.
        /// </summary>
        public float amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        /// <summary>
        /// Antal visningspaket.
        /// </summary>
        public float quantityShowCase
        {
            get
            {
                return _quantityShowCase;
            }
            set
            {
                _quantityShowCase = value;
            }
        }

        /// <summary>
        /// Antal att beställa.
        /// </summary>
        public float quantityToOrder
        {
            get
            {
                return _quantityToOrder;
            }
            set
            {
                _quantityToOrder = value;
            }
        }

        /// <summary>
        /// Antal sålda paket.
        /// </summary>
        public float soldQuantity
        {
            get
            {
                return _soldQuantity;
            }
            set
            {
                _soldQuantity = value;
            }
        }

        /// <summary>
        /// Återstående antal.
        /// </summary>
        public float remainingQuantity
        {
            get
            {
                return _remainingQuantity;
            }
            set
            {
                _remainingQuantity = value;
            }
        }

        /// <summary>
        /// Försäljnings-ID.
        /// </summary>
        public string salesId
        {
            get
            {
                return _salesId;
            }
            set
            {
                _salesId = value;
            }
        }

        /// <summary>
        /// Antal visningspaket formaterat som text.
        /// </summary>
        public string formatedQuantityShowCase
        {
            get
            {
                if (quantityShowCase > 0) return quantityShowCase.ToString();
                return "";
            }
        }

        /// <summary>
        /// Antal att beställa, formaterat som text.
        /// </summary>
        public string formatedQuantityToOrder
        {
            get
            {

                if (quantityToOrder > 0) return quantityToOrder.ToString();
                return "";
            }
        }

        /// <summary>
        /// Antal sålda paket, formaterat som text.
        /// </summary>
        public string formatedSoldQuantity
        {
            get
            {
                if (_soldQuantity > 0) return _soldQuantity.ToString();
                return "";
            }
        }

        /// <summary>
        /// Återstående antal, formaterat som text.
        /// </summary>
        public string formatedRemainingQuantity
        {
            get
            {

                if (_remainingQuantity > 0) return _remainingQuantity.ToString();
                return "";
            }
        }

        /// <summary>
        /// Sätter maximal textlängd på beskrivningsfältet.
        /// </summary>
        /// <param name="length"></param>
        public void setTextLength(int length)
        {
            if (description.Length > length) description = description.Substring(0, length) + "...";
        }


    }
}
