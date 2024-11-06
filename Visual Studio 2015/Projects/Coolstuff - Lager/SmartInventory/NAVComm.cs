using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Navipro.SmartInventory
{
    public class NAVComm
    {

        public static DataPickConfig updatePickConfig(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPickConfig dataPickConfig)
        {


            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            if (dataPickConfig == null)
            {
                dataPickConfig = new DataPickConfig();
            }

            Service service = new Service("updatePickConfig", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPickConfig);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    XmlElement pickConfigElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickConfig");
                    if (pickConfigElement != null)
                    {
                        dataPickConfig = DataPickConfig.fromDOM(pickConfigElement);
                        return dataPickConfig;
                    }


                    return dataPickConfig;
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return dataPickConfig;


        }

        public static bool getPickLists(Configuration configuration, SmartDatabase smartDatabase, Logger logger)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getPickLists", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setAgentId(configuration.agentId);
            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement activityElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLists");
                    if (activityElement != null)
                    {
                        DataPickHeader.fromDOM(activityElement, smartDatabase);
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


        public static bool getFirstPickLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string documentNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getFirstPickLine", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(0, documentNo));
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLines");
                if (pickLineElement != null)
                {
                    DataPickLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Plocklistan ej funnen."));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool getFirstUndoLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string documentNo, string orderNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Document document = new Document(0, documentNo);
            document.orderNo = orderNo;

            Service service = new Service("getFirstUndoLine", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(document);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLines");
                if (pickLineElement != null)
                {
                    DataPickLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Plocklistan ej funnen."));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool reportPickedLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPickLine dataPickLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("reportPickedLine", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPickLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLines");
                if (pickLineElement != null)
                {
                    XmlAttribute doneAttribute = pickLineElement.GetAttributeNode("done");
                    if (doneAttribute != null)
                    {
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader plockade!"));
                    }

                    DataPickLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Kommunikationsfel. Försöka igen?"), "Fel", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        return NAVComm.reportPickedLine(configuration, smartDatabase, logger, dataPickLine);
                    }
                    else
                    {
                        return false;
                    }
                }


                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool reportUnPickableLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPickLine dataPickLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("reportUnPickableLine", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPickLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLines");
                if (pickLineElement != null)
                {
                    DataPickLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    XmlAttribute doneAttribute = pickLineElement.GetAttributeNode("done");
                    if (doneAttribute != null)
                    {
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader plockade!"));
                    }
                    else
                    {
                        if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Kommunikationsfel. Försöka igen?"), "Fel", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {
                            return NAVComm.reportPickedLine(configuration, smartDatabase, logger, dataPickLine);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool reportUndoLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPickLine dataPickLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("reportUndoLine", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPickLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLines");
                if (pickLineElement != null)
                {
                    DataPickLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader är återförda!"));
                }


                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool reportPickedLines(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string documentNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataPickHeader dataPickHeader = DataPickHeader.getPickHeader(smartDatabase, documentNo);

            Service service = new Service("reportPickedLines", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPickHeader);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        /*

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
        */

        public static DataInventoryItem addPhysInventoryItemQty(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("addPhysInventoryItemQty", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);


                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement brandElement = (XmlElement)docLineElement.SelectSingleNode("brand");
                    if (brandElement.FirstChild != null)
                    {
                        dataInventoryItem.brand = brandElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }

            return null;

        }

        public static DataInventoryItem getPhysInventoryItemQty(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("getPhysInventoryItemQty", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);


                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement brandElement = (XmlElement)docLineElement.SelectSingleNode("brand");
                    if (brandElement.FirstChild != null)
                    {
                        dataInventoryItem.brand = brandElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }

            return null;

        }



        public static DataInventoryItem setPhysInventoryItemQty(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem, bool adjustPickLines)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            string serviceName = "setPhysInventoryItemQty";
            if (adjustPickLines) serviceName = "setPhysInventoryItemQtyAdjustPickLines";

            Service service = new Service(serviceName, smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);


                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement brandElement = (XmlElement)docLineElement.SelectSingleNode("brand");
                    if (brandElement.FirstChild != null)
                    {
                        dataInventoryItem.brand = brandElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }

            return null;

        }

        public static DataInventoryItem takeOutItem(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            string serviceName = "takeOutItem";

            Service service = new Service(serviceName, smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);


                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement brandElement = (XmlElement)docLineElement.SelectSingleNode("brand");
                    if (brandElement.FirstChild != null)
                    {
                        dataInventoryItem.brand = brandElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }

            return null;

        }

        public static DataInventoryItem getTakeOutItemQty(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            string serviceName = "getTakeOutItemQty";

            Service service = new Service(serviceName, smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);

                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement brandElement = (XmlElement)docLineElement.SelectSingleNode("brand");
                    if (brandElement.FirstChild != null)
                    {
                        dataInventoryItem.brand = brandElement.FirstChild.Value;
                    }
                    XmlElement wagonCodeElement = (XmlElement)docLineElement.SelectSingleNode("wagonCode");
                    if (wagonCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.wagonCode = wagonCodeElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }

            return null;

        }

        public static DataInventoryItem storeItem(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            string serviceName = "storeItem";

            Service service = new Service(serviceName, smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);


                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement brandElement = (XmlElement)docLineElement.SelectSingleNode("brand");
                    if (brandElement.FirstChild != null)
                    {
                        dataInventoryItem.brand = brandElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }


            return null;

        }

        public static DataInventoryItem getStoreItemQty(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            string serviceName = "getStoreItemQty";

            Service service = new Service(serviceName, smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);


                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement variantCodeElement = (XmlElement)docLineElement.SelectSingleNode("variantCode");
                    if (variantCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.variantCode = variantCodeElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement brandElement = (XmlElement)docLineElement.SelectSingleNode("brand");
                    if (brandElement.FirstChild != null)
                    {
                        dataInventoryItem.brand = brandElement.FirstChild.Value;
                    }
                    XmlElement wagonCodeElement = (XmlElement)docLineElement.SelectSingleNode("wagonCode");
                    if (wagonCodeElement.FirstChild != null)
                    {
                        dataInventoryItem.wagonCode = wagonCodeElement.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }


            return null;

        }

        public static void logPhysInventory(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("logPhysInventory", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }


        }

        public static bool getStoreLists(Configuration configuration, SmartDatabase smartDatabase, Logger logger)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getStoreLists", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement activityElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/storeLists");
                    if (activityElement != null)
                    {
                        DataStoreHeader.fromDOM(activityElement, smartDatabase);
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


        public static bool getReceiptLists(Configuration configuration, SmartDatabase smartDatabase, Logger logger)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getReceiptLists", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement activityElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/receiptLists");
                    if (activityElement != null)
                    {
                        DataReceiptHeader.fromDOM(activityElement, smartDatabase);

                    }


                    XmlElement usersElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/users");
                    if (usersElement != null)
                    {
                        DataUserCollection userCollection = new DataUserCollection();
                        XmlNodeList usersNodeList = usersElement.SelectNodes("user");
                        int i = 0;
                        while (i < usersNodeList.Count)
                        {
                            XmlElement userElement = (XmlElement)usersNodeList[i];

                            if (userElement.FirstChild != null)
                            {
                                DataUser dataUser = new DataUser(userElement.GetAttribute("code"), userElement.FirstChild.Value);
                                userCollection.Add(dataUser);
                            }
                            i++;
                        }
                        configuration.userCollection = userCollection;
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


        public static bool getRetailShipmentList(Configuration configuration, SmartDatabase smartDatabase, Logger logger)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getRetailShipmentList", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement activityElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/shipmentLists");
                    if (activityElement != null)
                    {
                        DataShipmentHeader.fromDOM(activityElement, smartDatabase);
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

        public static DataPickConfig createRetailPickList(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPickConfig dataPickConfig)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("createRetailPickList", smartDatabase, configuration);
            service.serviceRequest.setServiceArgument(dataPickConfig);
            service.setLogger(logger);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    XmlElement pickConfigElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickConfig");
                    if (pickConfigElement != null)
                    {
                        dataPickConfig = DataPickConfig.fromDOM(pickConfigElement);
                        return dataPickConfig;
                    }


                    return null;

                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return null;


        }

        public static bool getStoreLines(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string documentNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getStoreLines", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(0, documentNo));
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/storeLines");
                if (pickLineElement != null)
                {
                    DataStoreLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Inlagringsuppdraget ej funnen."));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool getReceiptLines(Configuration configuration, SmartDatabase smartDatabase, Logger logger, Document document)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getReceiptLines", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(document);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement receiptLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/receiptLines");
                if (receiptLineElement != null)
                {
                    DataReceiptLine.fromDOM(receiptLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Inleveransuppdraget ej funnet."));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }


        public static bool reportStoredLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataStoreLine dataStoreLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("reportStoredLine2", smartDatabase, configuration);

            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataStoreLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                XmlElement storeLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/storeLines");
                if (storeLineElement != null)
                {
                    dataStoreLine.delete();
                    //DataStoreLine.deleteAll(smartDatabase, dataStoreLine.documentNo);
                    DataStoreLine.fromDOM(storeLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader inlagrade!"));
                }


                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool getMoveStoreLines(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string wagonNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getMoveStoreLines", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(0, wagonNo));
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/storeLines");
                if (pickLineElement != null)
                {
                    DataStoreLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Felaktig vagn."));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static string getPickSalesOrderComments(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string documentNo, int lineNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getPickSalesOrderComments", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(0, documentNo, lineNo));
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            string comments = "";

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlNodeList nodeList = serviceResponse.responseDocument.SelectNodes("serviceResponse/docLine");
                int i = 0;
                while (i < nodeList.Count)
                {
                    XmlElement docLineElement = (XmlElement)nodeList.Item(i);

                    XmlElement xmlElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (comments != "") comments = comments + " ";
                    comments = comments + xmlElement.FirstChild.Value;
                    i++;
                }

            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return comments;

        }

        public static bool reportPickedLineDiff(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPickLine dataPickLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("reportPickedLineDiff", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPickLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLines");
                if (pickLineElement != null)
                {
                    XmlAttribute doneAttribute = pickLineElement.GetAttributeNode("done");
                    if (doneAttribute != null)
                    {
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader plockade!"));
                    }

                    DataPickLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Kommunikationsfel. Försöka igen?"), "Fel", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        return NAVComm.reportPickedLineDiff(configuration, smartDatabase, logger, dataPickLine);
                    }
                    else
                    {
                        return false;
                    }
                }


                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool getItemBinContentList(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string documentNo, int lineNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getItemBinContentList", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(0, documentNo, lineNo));
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement activityElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/binContentLists");
                    if (activityElement != null)
                    {
                        DataItemBinContent.fromDOM(activityElement, smartDatabase);
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

        public static bool updateBinPickLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPickLine dataPickLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("updateBinPickLine", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPickLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickLines");
                if (pickLineElement != null)
                {
                    XmlAttribute doneAttribute = pickLineElement.GetAttributeNode("done");
                    if (doneAttribute != null)
                    {
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader plockade!"));
                    }

                    DataPickLine.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Kommunikationsfel. Försöka igen?"), "Fel", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        return NAVComm.updateBinPickLine(configuration, smartDatabase, logger, dataPickLine);
                    }
                    else
                    {
                        return false;
                    }
                }


                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool getPickItemList(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string documentNo)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("getPickItemList", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(new Document(0, documentNo));
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement pickLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/pickListLines");
                if (pickLineElement != null)
                {
                    DataPickItemLines.fromDOM(pickLineElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    return true;
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }

        public static bool checkPickWagon(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataStoreLine dataStoreLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            string serviceName = "checkPickWagon";

            Service service = new Service(serviceName, smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataStoreLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                return true;

            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }


        public static bool reportReceiptLine(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataReceiptLine dataReceiptLine)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("reportReceiptLine", smartDatabase, configuration);

            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataReceiptLine);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    return false;
                }

                return true;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;

        }


        public static DataPurchaseOrder getPurchaseOrders(Configuration configuration, SmartDatabase smartDatabase, Logger logger, string eanCode)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataItemCrossReference itemCrossRef = new DataItemCrossReference(smartDatabase);
            itemCrossRef.crossReferenceCode = eanCode;

            Service service = new Service("getPurchaseOrders", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(itemCrossRef);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement purchaseOrdersElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/purchaseOrders");
                if (purchaseOrdersElement != null)
                {
                    DataPurchaseOrder.deleteAll(smartDatabase);

                    DataPurchaseOrder firstPurchaseOrder = DataPurchaseOrder.fromDOM(purchaseOrdersElement, smartDatabase);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return firstPurchaseOrder;
                }

            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return null;

        }




        public static string addPurchaseOrderToReceiptWorksheet(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataPurchaseOrder dataPurchaseOrder)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("addPurchaseOrderToReceiptWorksheet", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataPurchaseOrder);
            service.serviceRequest.setAgentId(configuration.agentId);

            ServiceResponse serviceResponse = service.performService();

            string receiptListNo = "";

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                }

                XmlElement receiptListElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/receiptList");
                if (receiptListElement != null)
                {
                    receiptListNo = receiptListElement.GetAttribute("no");

                }

            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return receiptListNo;


        }

    }


}
