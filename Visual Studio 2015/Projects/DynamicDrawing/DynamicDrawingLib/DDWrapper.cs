using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace Navipro.DynamicDrawing
{

    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F1-123456781020";
        public const string intfguid = "D030D214-C984-496a-87E7-123456781021";
        public const string eventguid = "D030D214-C984-496a-87E7-123456781022";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IDDWrapper
    {
        [DispId(1)]
        void setDrawingFile(string fileName);

        [DispId(2)]
        void setArgument(string key, string value);

        [DispId(3)]
        void exportDrawing(string fileName);

        [DispId(4)]
        void init(int width, int height);

    }



    [Guid(Guids.coclsguid), ProgId("Navipro.DynamicDrawing"), ClassInterface(ClassInterfaceType.None)]
    public class DDWrapper : IDDWrapper
    {
        private DDHandler ddHandler;

        public DDWrapper()
        {
            ddHandler = new DDHandler();
        }

        public void setDrawingFile(string fileName)
        {
            ddHandler.setDrawingFile(fileName);
        }

        public void setArgument(string key, string value)
        {
            ddHandler.setArgument(key, value);
        }

        public void exportDrawing(string fileName)
        {
            ddHandler.exportDrawing(fileName);
        }

        public void init(int width, int height)
        {
            ddHandler.init(width, height);
        }
    }
}
