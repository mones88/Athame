using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.Logging
{
    public enum Level
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public interface ILogger
    {
        void Write(Level level, string moduleTag, string message);
    }
}
