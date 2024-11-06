using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navipro.Lync.LyncUtilities;

namespace Navipro.Lync.LyncCallNotifier
{
    public class CallNotifier : Navipro.Lync.LyncUtilities.LyncApplication
    {
        private LyncUtility lyncUtil;

        public CallNotifier()
        {
            lyncUtil = new LyncUtility(this);
            Console.WriteLine("Lync Call Notification started.");
            Console.ReadLine();
        }

        #region LyncApplication Members

        void LyncUtilities.LyncApplication.fireIncomingConversationEvent(string uri, string responseGroupUri)
        {
            Console.WriteLine("Incoming conversation: " + uri);
        }

        #endregion

        #region LyncApplication Members


        public void fireDisconnectedConversationEvent(string uri, string responseGroupUri)
        {
            Console.WriteLine("Conversation disconnected: " + uri);
        }

        #endregion

        #region LyncApplication Members

        public void fireIncomingAlertEvent(string uri, string responseGroupUri)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
