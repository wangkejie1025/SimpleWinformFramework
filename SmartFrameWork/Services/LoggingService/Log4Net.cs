using System;
using log4net;
using log4net.Config;

namespace SmartFrameWork.Services
{
    /// <summary>
    /// 使用Log4net插件的log日志对象
    /// </summary>
    internal class Log4Net:ILoggingService
    {
        private ILog log;
        public Log4Net()
        {
            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            log = LogManager.GetLogger(typeof(Log4Net));
        }
        public bool IsFatalEnabled { get{return true;}}
        public bool IsErrorEnabled { get { return true; } }
        public bool IsWarnEnabled { get { return true; } }
        public bool IsInfoEnabled { get { return true; } }
        public bool IsDebugEnabled { get { return true; } }
        public void Debug(object message)
        {
            log.Debug(message);
        }

        public void DebugFormatted(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            log.Info(message);
        }

        public void InfoFormatted(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public void WarnFormatted(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public void ErrorFormatted(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        public void FatalFormatted(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }
    }
}
