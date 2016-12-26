using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Log
{
    public class LogConfiguration
    {
        public static void ConfigLog()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
