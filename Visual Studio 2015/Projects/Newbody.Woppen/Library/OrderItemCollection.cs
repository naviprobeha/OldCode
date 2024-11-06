using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// En lista av ett antal OrderItem-objekt.
    /// </summary>
    public class OrderItemCollection : CollectionBase, ServiceArgument
    {
        private string salesId;

        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett OrderItem-objekt.</returns>
        public OrderItem this[int index]
        {
            get { return (OrderItem)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett OrderItem-objekt i listan.
        /// </summary>
        /// <param name="orderItem">Ett OrderItem-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(OrderItem orderItem)
        {
            return (List.Add(orderItem));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet OrderItem-objekt.
        /// </summary>
        /// <param name="orderItem">Ett OrderItem-objekt.</param>
        /// <returns></returns>
        public int IndexOf(OrderItem orderItem)
        {
            return (List.IndexOf(orderItem));
        }

        /// <summary>
        /// Lägger in ett OrderItem-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="orderItem">OrderItem-objekt att lägga till i listan.</param>
        public void Insert(int index, OrderItem orderItem)
        {
            List.Insert(index, orderItem);
        }

        /// <summary>
        /// Tar bort ett OrderItem-objekt ur listan.
        /// </summary>
        /// <param name="orderItem">OrderItem-objekt att radera.</param>
        public void Remove(OrderItem orderItem)
        {
            List.Remove(orderItem);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet OrderItem-objekt.
        /// </summary>
        /// <param name="orderItem">OrderItem-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(OrderItem orderItem)
        {
            return (List.Contains(orderItem));
        }

        /// <summary>
        /// Märker listan med ett givet försäljnings-id.
        /// </summary>
        /// <param name="salesId">Försäljnings-id att märka listan med.</param>
        public void setSalesId(string salesId)
        {
            this.salesId = salesId;
        }

        #region ServiceArgument Members

        /// <summary>
        /// Skapar ett XML-dokument av listan.
        /// </summary>
        /// <param name="xmlDoc">XML-dokument att använda som fabrik.</param>
        /// <returns>Returnerar ett XML-element innehållande listan i XML-form.</returns>
        public XmlElement toDOM(XmlDocument xmlDoc)
        {


            XmlElement orderElement = xmlDoc.CreateElement("salesIdShowCase");


            XmlElement orderLinesElement = xmlDoc.CreateElement("items");

            int i = 0;
            while (i < Count)
            {
                OrderItem orderItem = this[i];

                XmlElement orderLineElement = xmlDoc.CreateElement("item");

                XmlElement salesIdElement = xmlDoc.CreateElement("salesId");
                salesIdElement.AppendChild(xmlDoc.CreateTextNode(salesId));
                orderLineElement.AppendChild(salesIdElement);

                XmlElement itemNoElement = xmlDoc.CreateElement("itemNo");
                itemNoElement.AppendChild(xmlDoc.CreateTextNode(orderItem.itemNo));
                orderLineElement.AppendChild(itemNoElement);

                XmlElement quantityElement = xmlDoc.CreateElement("receivedQuantity");
                quantityElement.AppendChild(xmlDoc.CreateTextNode(orderItem.remainingQuantity.ToString()));
                orderLineElement.AppendChild(quantityElement);

                XmlElement quantity2Element = xmlDoc.CreateElement("qtyPackingMaterial");
                quantity2Element.AppendChild(xmlDoc.CreateTextNode(orderItem.quantity2.ToString()));
                orderLineElement.AppendChild(quantity2Element);

                XmlElement quantity3Element = xmlDoc.CreateElement("qtyPackingSlips");
                quantity3Element.AppendChild(xmlDoc.CreateTextNode(orderItem.quantity3.ToString()));
                orderLineElement.AppendChild(quantity3Element);

                XmlElement methodElement = xmlDoc.CreateElement("method");
                methodElement.AppendChild(xmlDoc.CreateTextNode(orderItem.method.ToString()));
                orderLineElement.AppendChild(methodElement);

                orderLinesElement.AppendChild(orderLineElement);

                i++;
            }

            orderElement.AppendChild(orderLinesElement);

            return orderElement;
        }

        #endregion
    }
}
