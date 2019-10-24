using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace SmartFrameWork.Utils
{
    /// <summary>
    /// Invokes a callback when this class is disposed.
    /// </summary>
    public sealed class CallbackOnDispose : IDisposable
    {
        System.Action callback;

        public CallbackOnDispose(System.Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");
            this.callback = callback;
        }

        public void Dispose()
        {
            System.Action action = Interlocked.Exchange(ref callback, null);
            if (action != null)
            {
                action();
                #if DEBUG
                GC.SuppressFinalize(this);
                #endif
            }
        }

        #if DEBUG
        ~CallbackOnDispose()
        {
            Debug.Fail("CallbackOnDispose was finalized without being disposed.");
        }
        #endif
    }
}
