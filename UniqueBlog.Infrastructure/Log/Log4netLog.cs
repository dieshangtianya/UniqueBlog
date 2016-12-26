﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace UniqueBlog.Infrastructure.Log
{
    public class Log4netLogger : ILog
    {
        private log4net.ILog log4netLogger;

        public Log4netLogger(Type type)
        {
            log4netLogger = LogManager.GetLogger(type);
        }

        public Log4netLogger(string loggerName)
        {
            log4netLogger = LogManager.GetLogger(loggerName);
        }

        #region implementation

        public void Debug(object message)
        {
            log4netLogger.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            log4netLogger.Debug(message, exception);
        }

        public void DebugFormat(string format, object arg0)
        {
            log4netLogger.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            log4netLogger.DebugFormat(format, args);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            log4netLogger.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.DebugFormat(provider, format, args);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.DebugFormat(format, arg0, arg1, arg2);
        }

        public void Error(object message)
        {
            log4netLogger.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            log4netLogger.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            log4netLogger.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            log4netLogger.ErrorFormat(format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            log4netLogger.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void Fatal(object message)
        {
            log4netLogger.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            log4netLogger.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            log4netLogger.FatalFormat(format, args);
        }

        public void FatalFormat(string format, object arg0)
        {
            log4netLogger.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            log4netLogger.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.FatalFormat(format, arg0, arg1, arg2);
        }

        public void Info(object message)
        {
            log4netLogger.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            log4netLogger.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            log4netLogger.InfoFormat(format, args);
        }

        public void InfoFormat(string format, object arg0)
        {
            log4netLogger.InfoFormat(format, arg0);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.InfoFormat(provider, format, args);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            log4netLogger.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.InfoFormat(format, arg0, arg1, arg2);
        }

        public void Warn(object message)
        {
            log4netLogger.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            log4netLogger.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            log4netLogger.WarnFormat(format, args);
        }

        public void WarnFormat(string format, object arg0)
        {
            log4netLogger.WarnFormat(format, arg0);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            log4netLogger.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.WarnFormat(format, arg0, arg1, arg2);
        }

        #endregion
    }
}
