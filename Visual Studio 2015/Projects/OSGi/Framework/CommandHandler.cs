using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.OSGi.Framework
{
    public class CommandHandler
    {
        private FrameworkFactory _loader;
        private ConsoleConnection _console;

        public CommandHandler(FrameworkFactory loader, ConsoleConnection console)
        {
            _loader = loader;
            _console = console;
        }

        public void executeCommand(string command)
        {
            string[] commandArray = command.Split(' ');
            if (commandArray.Length > 0)
            {
                string mainCommand = commandArray[0];
                string[] parameterArray = getParameters(commandArray);
                if (mainCommand.ToUpper() == "LOAD") command_Load(parameterArray);
                if (mainCommand.ToUpper() == "START") command_Start(parameterArray);
                if (mainCommand.ToUpper() == "STOP") command_Stop(parameterArray);
                if (mainCommand.ToUpper() == "UNLOAD") command_Unload(parameterArray);
                if (mainCommand.ToUpper() == "RELOAD") command_Reload(parameterArray);
                if (mainCommand.ToUpper() == "LIST") command_List(parameterArray);
                if (mainCommand.ToUpper() == "HELP") command_Help(parameterArray);
            }
        }

        private void command_Load(string[] parameterArray)
        {
            if (parameterArray.Length < 3)
            {
                showHelp("LOAD");
                return;
            }

            try
            {
                _loader.moduleHandler.loadModule(parameterArray[2], parameterArray[0], parameterArray[1]);
            }
            catch (Exception e)
            {
                _console.write("Error: " + e.Message);
            }
        }

        private void command_Start(string[] parameterArray)
        {
            if (parameterArray.Length < 1)
            {
                showHelp("START");
                return;
            }

            try
            {
                _loader.moduleHandler.startModule(parameterArray[0]);
            }
            catch (Exception e)
            {
                _console.write("Error: " + e.Message);
            }
        }

        private void command_Stop(string[] parameterArray)
        {
            if (parameterArray.Length < 1)
            {
                showHelp("STOP");
                return;
            }

            try
            {
                _loader.moduleHandler.stopModule(parameterArray[0]);
            }
            catch (Exception e)
            {
                _console.write("Error: " + e.Message);
            }
        }

        private void command_Unload(string[] parameterArray)
        {
            if (parameterArray.Length < 1)
            {
                showHelp("UNLOAD");
                return;
            }

            try
            {
                _loader.moduleHandler.unloadModule(parameterArray[0]);
            }
            catch (Exception e)
            {
                _console.write("Error: " + e.Message);
            }
        }

        private void command_Reload(string[] parameterArray)
        {
            if (parameterArray.Length < 1)
            {
                showHelp("RELOAD");
                return;
            }

            try
            {
                _loader.moduleHandler.reloadModule(parameterArray[0]);
            }
            catch (Exception e)
            {
                _console.write("Error: " + e.Message);
            }
        }

        private void command_List(string[] parameterArray)
        {

            try
            {
                _console.write("Name                 Status       Heartbeat  Class");
                _console.write("=====================================================================================");
                ModuleCollection moduleCollection = _loader.moduleHandler.getModuleCollection();
                int i = 0;
                while (i < moduleCollection.Count)
                {
                    _console.write(moduleCollection[i].name.PadRight(20) + " " + moduleCollection[i].getStatus().PadRight(12)+" "+moduleCollection[i].heartBeat.ToString("HH:mm:ss").PadRight(10)+" "+moduleCollection[i].className);

                    i++;
                }
            }
            catch (Exception e)
            {
                _console.write("Error: " + e.Message);
            }
        }


        private void command_Help(string[] parameterArray)
        {
            if (parameterArray.Length < 1)
            {
                _console.write("Command   Description");
                _console.write("=============================================================");
                _console.write("LOAD      Loads an assembly module into memory for execution.");
                _console.write("START     Starts the execution of an assembly module.");
                _console.write("STOP      Stops the execution of an assembly module.");
                _console.write("UNLOAD    Unloads an assembly module from memory.");
                _console.write("RELOAD    Reloads an assembly module into memory.");
                _console.write("LIST      Lists all defined modules.");
                return;
            }

            showHelp(parameterArray[0]);
        }

        private void showHelp(string command)
        {
            if (command == "LOAD") _console.write("Syntax: LOAD [Assembly path] [Class name] [Module name]");
            if (command == "START") _console.write("Syntax: START [Module name]");
            if (command == "STOP") _console.write("Syntax: STOP [Module name]");
            if (command == "UNLOAD") _console.write("Syntax: UNLOAD [Module name]");
            if (command == "RELOAD") _console.write("Syntax: RELOAD [Module name]");
            if (command == "HELP") _console.write("Syntax: HELP [Module name]");
        }

        private string[] getParameters(string[] commandArray)
        {
            string[] parameterArray = new string[commandArray.Length - 1];

            if (commandArray.Length > 1)
            {
                for (int i = 0, j = 0; i < parameterArray.Length; i++, j++)
                {
                    if (i == 0)
                    {
                        j++;
                    }

                    parameterArray[i] = commandArray[j];
                }

            }
            return parameterArray;


        }
    }
}
