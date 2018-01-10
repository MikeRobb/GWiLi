using System;

namespace GWiLi.EntityFramework.Extensions
{
    public static class ExceptionExtension
    {
        private const string LineSep = "\r\n\t";

        public static string GetFullExceptionMessage(this Exception e, string prefixMessage = null)
        {
            var r = string.Empty;
            if(!string.IsNullOrEmpty(prefixMessage))
            {
                r = $"{prefixMessage}{LineSep}";
            }

            r += e.Message;
            var cur = e.InnerException;
            while(cur != null)
            {
                r += $"{LineSep}{cur.Message}";
                cur = cur.InnerException;
            }

            return r;
        }
    }
}