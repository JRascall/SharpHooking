using System;

namespace SharpHooks
{
    public class CallLog
    {
        protected string name;
        protected object[] args;
        protected DateTime on;
        protected Exception[] exceptions;

        public CallLog(string name, object[] args, Exception[] exceptions = null)
        {
            this.name = name;
            this.args = args;
            this.exceptions = exceptions;

            on = DateTime.Now;
        }
    }
}
