using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarnaTest
{
    public class Class1
    {
        public Class1()
        {

            Klarna.Rest.Client client = new Klarna.Rest.Client("", "", new Uri(""));

            Klarna.Rest.OrderManagement.ICapture capture = client.NewCapture(new Uri("11223"));

            Klarna.Rest.Models.CaptureData capData = new Klarna.Rest.Models.CaptureData();
            //capData.

            //Klarna.Rest.Models.OrderData orderdata = order.Fetch();
            //orderdata.
        }

    }
}
