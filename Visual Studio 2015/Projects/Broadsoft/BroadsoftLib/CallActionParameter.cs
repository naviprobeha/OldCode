using System;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for CallActionParameter.
    /// </summary>
    public class CallActionParameter
    {
        public string type;
        public string parameter;

        public CallActionParameter(string type, string parameter)
        {
            //
            // TODO: Add constructor logic here
            //
            this.type = type;
            this.parameter = parameter;
        }
    }
}
