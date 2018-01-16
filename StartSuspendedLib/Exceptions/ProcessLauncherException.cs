using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartSuspendedLib.Exceptions
{
    public class ProcessLauncherException : ApplicationException
    {
        public ProcessLauncherException(string message) : base(message)
        {
        }
    }
}
