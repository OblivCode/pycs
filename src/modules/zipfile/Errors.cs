using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pycs.modules.zipfile
{
    public class BadZipFile : Exception
    {
        public string Error() => this.Message;
        public BadZipFile(string message)
            : base(message)
        {
        }

        public BadZipFile(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
