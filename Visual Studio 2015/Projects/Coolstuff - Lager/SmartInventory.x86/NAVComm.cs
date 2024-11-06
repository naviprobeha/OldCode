using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Navipro.SmartInventory
{
    public class NAVComm
    {

        
        public static DataInventoryItem addPhysInventoryItemQty(Configuration configuration, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("addPhysInventoryItemQty", configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
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

                    //System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    ErrorMessage.show(serviceResponse.status.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem();


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

        public static DataInventoryItem getPhysInventoryItemQty(Configuration configuration, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("getPhysInventoryItemQty", configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
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

                    //System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    ErrorMessage.show(serviceResponse.status.description);

                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem();


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



        public static DataInventoryItem setPhysInventoryItemQty(Configuration configuration, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("setPhysInventoryItemQty", configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);
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

                    //System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    ErrorMessage.show(serviceResponse.status.description);

                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem();


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


    }

}
