using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class Console
    {
        private static StringBuilder builder = new StringBuilder();

        public static string GetString()
        {
            lock (builder)
            {
                string message = builder.ToString();
                builder.Clear();
                return message;
            }
        }

        public static void Write(string message)
        {
            lock (builder)
            {
                builder.Append(message);
            }
        }

        public static void WriteLine(string message)
        {
            lock (builder)
            {
                builder.AppendLine(System.DateTime.Now.ToString("G")  + ": " + message);
            }
        }
    }
}
