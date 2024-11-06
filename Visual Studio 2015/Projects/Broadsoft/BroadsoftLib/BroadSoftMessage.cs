using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for BroadWorksMessage.
    /// </summary>
    public interface BroadSoftMessage
    {
        XmlDocument toDOM();
    }
}
