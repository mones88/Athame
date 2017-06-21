using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.Utils;

namespace Athame.Logging
{
    public class DebugLogger : ILogger
    {
        public void Write(Level level, string moduleTag, string message)
        {
            Debug.WriteLine($"[{level}] {message}", moduleTag);
        }
    }
}
