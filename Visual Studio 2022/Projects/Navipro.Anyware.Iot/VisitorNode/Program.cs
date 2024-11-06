using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;

namespace VisitorNode
{
    public class Program : Application
    {

        public static void Main()
        {
            var taskRegistered = false;
            var exampleTaskName = "VisitorMonitor";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == exampleTaskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            var builder = new BackgroundTaskBuilder();

            builder.Name = exampleTaskName;
            builder.TaskEntryPoint = "StartupTask";
            builder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange, false));

            BackgroundTaskRegistration registeredTask = builder.Register();
        }
    }
}
