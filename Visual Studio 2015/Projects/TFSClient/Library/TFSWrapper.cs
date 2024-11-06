using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Navipro.TFSClient.Library
{
    public class TFSWrapper
    {
        private string tfsBaseUrl { get; set; }
        private TfsTeamProjectCollection tpc;
        private VersionControlServer versionControl;
        public TFSWrapper(string baseUrl)
        {
            tfsBaseUrl = baseUrl;

            tpc = new TfsTeamProjectCollection(new Uri(tfsBaseUrl));
            versionControl = tpc.GetService<VersionControlServer>();

            versionControl.NonFatalError += VersionControl_NonFatalError;
            versionControl.Getting += VersionControl_Getting;
            versionControl.BeforeCheckinPendingChange += VersionControl_BeforeCheckinPendingChange;
            versionControl.NewPendingChange += VersionControl_NewPendingChange;
        }

        private void VersionControl_NewPendingChange(object sender, PendingChangeEventArgs e)
        {
            
        }

        private void VersionControl_BeforeCheckinPendingChange(object sender, ProcessingChangeEventArgs e)
        {
            
        }

        private void VersionControl_Getting(object sender, GettingEventArgs e)
        {
            
        }

        private void VersionControl_NonFatalError(object sender, ExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                throw new Exception("Non - fatal exception: "+e.Exception.Message);
            }
            else
            {
                throw new Exception("Non - fatal failure: "+e.Failure.Message);
            }
        }
        public void createProjekt(string customer, string projectName)
        {
            //Permissions to be allowed for particular user group for the team project created
            string[] allowPermissions = new string[2];
            allowPermissions[0] = "Read";
            allowPermissions[1] = "Write";

            //Permissions to be denied for particular user group 
            string[] denyPermissions = new string[0];

            TeamProjectFolderPermission[] permissions = new TeamProjectFolderPermission[1];
            TeamProjectFolderPermission permission = new TeamProjectFolderPermission("[DefaultCollection]\\Navipro Team", allowPermissions, denyPermissions);
            permissions[0] = permission;

            WorkingFolder[] mappings = new WorkingFolder[1];
            mappings[0] = new WorkingFolder("$/Code", "C:\\temp\\tfstemp");

            Workspace workspace = null;
            try
            {
                workspace = versionControl.GetWorkspace(customer + " - " + projectName, "[DefaultCollection]\\Navipro Team");
                workspace.Delete();
                return;
            }
            catch (WorkspaceNotFoundException)
            {
                workspace = versionControl.CreateWorkspace(customer + " - " + projectName, "[DefaultCollection]\\Navipro Team", "Hepp", mappings);
            }

            workspace.Map("$/Code", "C:\\temp\\tfstemp");
            workspace.PendAdd("C:\\temp\\tfstemp", true);
            //TeamProjectFolderOptions project = new TeamProjectFolderOptions(customer + " - " + projectName, "", "", true, permissions, null, true);
            //versionControl.CreateTeamProjectFolder(project);
        }
        
    }
}
