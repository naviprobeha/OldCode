using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Navipro.SmartInventory
{
    public class NAVComm
    {

        public static DataUserCollection getUsers(Configuration configuration, SmartDatabase smartDatabase, Logger logger)
        {
            DataUserCollection dataUserCollection = new DataUserCollection();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("getUsers", smartDatabase, configuration);
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

                    XmlNodeList usersNodeList = serviceResponse.responseDocument.SelectNodes("serviceResponse/users/user");
                    int i = 0;
                    while (i < usersNodeList.Count)
                    {
                        XmlElement userElement = (XmlElement)usersNodeList[i];
                        DataUser dataUser = new DataUser(userElement.GetAttribute("code"), userElement.GetAttribute("name"));
                        dataUserCollection.Add(dataUser);

                        i++;
                    }

                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return dataUserCollection;

        }

        public static bool verifyUser(Configuration configuration, SmartDatabase smartDatabase, Logger logger, DataUser dataUser)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();


            Service service = new Service("verifyUser", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setAgentId(configuration.agentId);
            service.serviceRequest.setServiceArgument(dataUser);

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


                    return true;
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return false;


        }




        public static bool reportItem(Configuration configuration, SmartDatabase smartDatabase, Logger logger, ref DataReportItem dataReportItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("reportItem", smartDatabase, configuration);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataReportItem);
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

                XmlElement reportItemElement = (XmlElement)serviceResponse.responseDocument.SelectSingleNode("serviceResponse/reportItem");
                if (reportItemElement != null)
                {

                    dataReportItem = DataReportItem.fromDOM(reportItemElement);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                    return true;
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Kommunikationsfel. Försöka igen?"), "Fel", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        return NAVComm.reportItem(configuration, smartDatabase, logger, ref dataReportItem);
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

    }


}
