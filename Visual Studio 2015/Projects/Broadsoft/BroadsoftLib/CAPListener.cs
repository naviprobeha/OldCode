using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for CAPListener.
    /// </summary>
    public interface CAPListener
    {
        void notify(string docType, XmlDocument xmlDocument);
    }
}
