using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for CAPListener.
    /// </summary>
    public interface CallClientListener
    {
        void callClient_connect(int status);
        void callClient_callUpdate(CallUpdate callUpdate);
        void callClient_profileUpdate(ProfileUpdate profileUpdate);
        void callClient_sessionUpdate(SessionUpdate sessionUpdate);
    }
}
