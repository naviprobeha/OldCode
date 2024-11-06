using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Navipro.SmartInventory
{
    public class NAVComm
    {

        public static bool getSalesOrders(Configuration configuration, SmartDatabase smartDatabase, Logger logger)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getSalesOrders", smartDatabase, configuration);
            service.setLogger(logger);
            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {

                    XmlElement ordersElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/orders");
                    if (ordersElement != null)
                    {
                        DataSalesHeader.fromDOM(ordersElement, smartDatabase);
                    }

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;


        }

        public static bool getSalesOrder(Configuration configuration, SmartDatabase smartDatabase, Logger logger, int documentType, string documentNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getSalesOrder", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(documentType, documentNo));
            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement orderElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/order");
                if (orderElement != null)
                {
                    
                    DataSalesLine.fromDOM(orderElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Ordern ej funnen."));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }
    



        public static bool getPurchaseOrder(Configuration configuration, SmartDatabase smartDatabase, Logger logger, int documentType, string documentNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getPurchaseOrder", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(documentType, documentNo));
            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement orderElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/order");
                if (orderElement != null)
                {
                    DataPurchaseLine.fromDOM(orderElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Ordern ej funnen."));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }


        public static DataSalesLine addPhysInventoryItemQty(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string itemNo, float quantity)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("addPhysInventoryItemQty", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new DocumentLine(configuration.agentId, itemNo, quantity));
            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    DataSalesLine dataSalesLine = new DataSalesLine(smartDatabase);

                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement != null)
                    {
                        dataSalesLine.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement != null)
                    {
                        dataSalesLine.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement != null)
                    {
                        dataSalesLine.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement != null)
                    {
                        dataSalesLine.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }
                    XmlElement uomElement = (XmlElement)docLineElement.SelectSingleNode("unitOfMeasureCode");
                    if (uomElement != null)
                    {
                        dataSalesLine.unitOfMeasure = uomElement.FirstChild.Value;
                    }

                    return dataSalesLine;

                }

            }

            return null;

        }
    }

}
