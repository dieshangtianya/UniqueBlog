using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Log
{
    public class LoggerFactory
    {
        public static ILog GetLogger(Type type)
        {
            return new Log4netLogger(type);
        }

        public static ILog GetLogger(string loggerName)
        {
            return new Log4netLogger(loggerName);
        }
    }
}
