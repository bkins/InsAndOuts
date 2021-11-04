using System;
using NLog;

namespace InsAndOuts.Utilities.Interfaces
{
    public interface IMessage
    {
        
        void LongAlert(string  message);
        void ShortAlert(string message);

        void Log(LogLevel  level
               , string    message
               , Exception ex);
    }
}
