using System;
using System.IO;
using Navipro.Base.Common;
using System.Data.SqlClient;

namespace Navipro.Base.LogHandlers
{
    public class FileLogger : Logger
    {
        private TextWriter textWriter;

        public FileLogger(string fileName)
        {
            textWriter = File.AppendText(fileName);
        }

        public void close()
        {
            textWriter.Close();
        }

        #region Logger Members

        public void write(string source, int level, string message)
        {
            
            textWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + source.PadRight(30) + " " + level.ToString().PadRight(3) + " " + message);
            
        }

        #endregion
    }
}
