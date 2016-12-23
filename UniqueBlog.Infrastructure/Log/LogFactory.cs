using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Log
{
    public class LogFactory
    {
        public static ILog GetLog(Type type)
        {
            return new Log4netLog(type);
        }
    }
}
