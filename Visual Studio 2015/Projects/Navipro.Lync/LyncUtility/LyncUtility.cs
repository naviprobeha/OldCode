using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Lync.Model;

namespace Navipro.Lync.LyncUtilities
{
    public class LyncUtility
    {
        private LyncClient lyncClient;
        private LyncApplication lyncApp;
        

        public LyncUtility(LyncApplication lyncApp)
        {
            this.lyncApp = lyncApp;

            lyncClient = LyncClient.GetClient();
            lyncClient.ConversationManager.ConversationAdded += new EventHandler<Microsoft.Lync.Model.Conversation.ConversationManagerEventArgs>(ConversationManager_ConversationAdded);
            
        }

        private void ConversationManager_ConversationAdded(object sender, Microsoft.Lync.Model.Conversation.ConversationManagerEventArgs e)
        {
            if (e.Conversation.State == Microsoft.Lync.Model.Conversation.ConversationState.Inactive) return;

            e.Conversation.Modalities[Microsoft.Lync.Model.Conversation.ModalityTypes.AudioVideo].ModalityStateChanged += new EventHandler<Microsoft.Lync.Model.Conversation.ModalityStateChangedEventArgs>(LyncUtility_ModalityStateChanged);


            string remoteParticipant = ((Contact)e.Conversation.Properties[Microsoft.Lync.Model.Conversation.ConversationProperty.Inviter]).Uri;


            string responseGroup = "";
            if (((RepresentationInfo)e.Conversation.Properties[Microsoft.Lync.Model.Conversation.ConversationProperty.RepresentedBy]) != null)
            {
                responseGroup = ((RepresentationInfo)e.Conversation.Properties[Microsoft.Lync.Model.Conversation.ConversationProperty.RepresentedBy]).Uri.Substring(4);
            }

            lyncApp.fireIncomingAlertEvent(remoteParticipant.Substring(4), responseGroup);
        }

        void LyncUtility_ModalityStateChanged(object sender, Microsoft.Lync.Model.Conversation.ModalityStateChangedEventArgs e)
        {
            Microsoft.Lync.Model.Conversation.Conversation conversation = ((Microsoft.Lync.Model.Conversation.AudioVideo.AVModality)sender).Conversation;

            string remoteParticipant = ((Contact)conversation.Properties[Microsoft.Lync.Model.Conversation.ConversationProperty.Inviter]).Uri;

            //Console.WriteLine("DEBUG: " + ((Microsoft.Lync.Model.Conversation.AudioVideo.AVModality)sender).Conversation.Properties[Microsoft.Lync.Model.Conversation.ConversationProperty.Inviter]).Uri);

            string responseGroup = "";
            if (((RepresentationInfo)conversation.Properties[Microsoft.Lync.Model.Conversation.ConversationProperty.RepresentedBy]) != null)
            {
                responseGroup = ((RepresentationInfo)conversation.Properties[Microsoft.Lync.Model.Conversation.ConversationProperty.RepresentedBy]).Uri.Substring(4);
            }

            if (e.NewState == Microsoft.Lync.Model.Conversation.ModalityState.Connected)
            {

                lyncApp.fireIncomingConversationEvent(remoteParticipant.Substring(4), responseGroup);
            }

            if (e.NewState == Microsoft.Lync.Model.Conversation.ModalityState.Disconnected)
            {
                lyncApp.fireDisconnectedConversationEvent(remoteParticipant.Substring(4), responseGroup);
            }

        }

    }
}
