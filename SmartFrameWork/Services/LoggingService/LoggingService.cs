using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork.Services
{
    /// <summary>
    /// Class for easy logging.
    /// </summary>
    public static class LoggingService
    {
        public static void Debug(object message)
        {
            ServiceManager.Instance.LoggingService.Debug(message);
        }

        public static void DebugFormatted(string format, params object[] args)
        {
            ServiceManager.Instance.LoggingService.DebugFormatted(format, args);
        }

        public static void Info(object message, bool isShowMessageBox=false)
        {
            Console.WriteLine(message.ToString());
            ServiceManager.Instance.LoggingService.Info(message);
            if (isShowMessageBox)
            {           
                ServiceManager.Instance.MessageService.ShowMessage(message.ToString(),"Info");
            }
        }
        public static void InfoFormatted(string format, params object[] args)
        {
            ServiceManager.Instance.LoggingService.InfoFormatted(format, args);
        }

        public static void Warn(object message)
        {
            Console.WriteLine(message.ToString());
            ServiceManager.Instance.LoggingService.Warn(message);
            ServiceManager.Instance.MessageService.ShowWarning(message.ToString());
        }

        public static void Warn(object message, Exception exception)
        {
            Console.WriteLine(message.ToString());
            ServiceManager.Instance.LoggingService.Warn(message, exception);
            
        }

        public static void WarnFormatted(string format, params object[] args)
        {
            ServiceManager.Instance.LoggingService.WarnFormatted(format, args);
        }

        public static void Error(object message)
        {
            Console.WriteLine(message.ToString());
            ServiceManager.Instance.LoggingService.Error(message);
            //ServiceManager.Instance.MessageService.ShowError(message.ToString());
        }

        public static void Error(object message, Exception exception)
        {
            Console.WriteLine(message.ToString());
            ServiceManager.Instance.LoggingService.Error(message, exception);
        }

        public static void ErrorFormatted(string format, params object[] args)
        {
            ServiceManager.Instance.LoggingService.ErrorFormatted(format, args);
        }

        public static void Fatal(object message)
        {
            ServiceManager.Instance.LoggingService.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            ServiceManager.Instance.LoggingService.Fatal(message, exception);
        }

        public static void FatalFormatted(string format, params object[] args)
        {
            ServiceManager.Instance.LoggingService.FatalFormatted(format, args);
        }

        public static bool IsDebugEnabled
        {
            get
            {
                return ServiceManager.Instance.LoggingService.IsDebugEnabled;
            }
        }

        public static bool IsInfoEnabled
        {
            get
            {
                return ServiceManager.Instance.LoggingService.IsInfoEnabled;
            }
        }

        public static bool IsWarnEnabled
        {
            get
            {
                return ServiceManager.Instance.LoggingService.IsWarnEnabled;
            }
        }

        public static bool IsErrorEnabled
        {
            get
            {
                return ServiceManager.Instance.LoggingService.IsErrorEnabled;
            }
        }

        public static bool IsFatalEnabled
        {
            get
            {
                return ServiceManager.Instance.LoggingService.IsFatalEnabled;
            }
        }
    }
}
