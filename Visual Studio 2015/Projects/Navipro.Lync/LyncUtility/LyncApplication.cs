using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Lync.LyncUtilities
{
    public interface LyncApplication
    {
        void fireIncomingAlertEvent(string uri, string responseGroupUri);
        void fireIncomingConversationEvent(string uri, string responseGroupUri);
        void fireDisconnectedConversationEvent(string uri, string responseGroupUri);
    }
}
