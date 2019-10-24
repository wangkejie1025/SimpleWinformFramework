// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmartFrameWork.Services
{
    /// <summary>
    /// Maintains a list of services that can be shutdown in the reverse order of their initialization.
    /// Maintains references to the core service implementations.
    /// </summary>
    public abstract class ServiceManager : IServiceProvider
    {
        volatile static ServiceManager instance = new DefaultServiceManager();

        /// <summary>
        /// Gets the static ServiceManager instance.
        /// </summary>
        public static ServiceManager Instance
        {
            get { return instance; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                instance = value;
            }
        }

        /// <summary>
        /// Gets a service. Returns null if service is not found.
        /// </summary>
        public abstract object GetService(Type serviceType);

        /// <summary>
        /// Gets a service. Returns null if service is not found.
        /// </summary>
        public T GetService<T>() where T : class
        {
            return GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Gets a service. Throws an exception if service is not found.
        /// </summary>
        public object GetRequiredService(Type serviceType)
        {
            object service = GetService(serviceType);
            if (service == null)
                throw new ServiceNotFoundException();
            return service;
        }

        /// <summary>
        /// Gets a service. Throws an exception if service is not found.
        /// </summary>
        public T GetRequiredService<T>() where T : class
        {
            return (T)GetRequiredService(typeof(T));
        }

        /// <summary>
        /// Gets the logging service.
        /// </summary>
        public virtual ILoggingService LoggingService
        {
            get { return (ILoggingService)GetRequiredService(typeof(ILoggingService)); }
        }

        /// <summary>
        /// Gets the message service.
        /// </summary>
        public virtual IMessageService MessageService
        {
            get { return (IMessageService)GetRequiredService(typeof(IMessageService)); }
        }
    }
    //每一个services都定义了一个对外提供服务的Service类，使用ServcieManager中的instance示例对象访问具体的服务
    //提供了一个defaultManager类，实现了默认的服务，向外提供服务的依然是MessageService、LoggingServie这些实现了各自
    //接口的服务类
    sealed class DefaultServiceManager : ServiceManager
    {
#if DEBUG
        static ILoggingService loggingService = new TextWriterLoggingService(new DebugTextWriter());    
        //static IMessageService messageService = new TextWriterMessageService(System.Console.Out);  
#else 
        static ILoggingService loggingService = new Log4NetService();
        //static IMessageService messageService = new MessageBoxMessageService();
#endif
        static IMessageService messageService = new MessageBoxMessageService();
        public override ILoggingService LoggingService
        {
            get { return loggingService; }
        }

        public override IMessageService MessageService
        {
            get { return messageService; }
        }

        public override object GetService(Type serviceType)
        {
            if (serviceType == typeof(ILoggingService))
                return loggingService;
            else if (serviceType == typeof(IMessageService))
                return messageService;
            else
                return null;
        }
    }
}
