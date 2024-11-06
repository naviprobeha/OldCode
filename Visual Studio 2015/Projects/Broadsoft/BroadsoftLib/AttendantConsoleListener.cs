using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for CAPListener.
    /// </summary>
    public interface AttendantConsoleListener
    {
        void attendantConsole_connect(int status);
        void attendantConsole_callUpdate(CallUpdate callUpdate);
        void attendantConsole_profileUpdate(ProfileUpdate profileUpdate);
        void attendantConsole_sessionUpdate(SessionUpdate sessionUpdate);
    }
}
