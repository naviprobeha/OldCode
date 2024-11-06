using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navipro.Base.Common;

namespace Navipro.Base.LogHandlers
{
    public class CoreLogHandler : Logger
    {
        public CoreLogHandler()
        {


        }
 

        #region Logger Members

        public void write(string source, int level, string message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
