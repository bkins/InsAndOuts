using Android.App;
using Android.Widget;
using System;
using System.Threading.Tasks;
using InsAndOuts.Droid.Utilities;
using InsAndOuts.Utilities.Interfaces;
using NLog;
using Logger = NLog.Logger;
using LogManager = NLog.LogManager;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
[assembly: Xamarin.Forms.Dependency(typeof(ILogger))]
namespace InsAndOuts.Droid.Utilities
{
    public class MessageAndroid : IMessage, ILogger
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long)
                ?.Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short)
                ?.Show();
        }


        //BENDO: Continue to implement logging to LogCat (not sure if this is the best place/way.  Probably need to learn more about it.
        public void Log(LogLevel  level
                       , string    message
                       , Exception ex)
        {
            Logger catLog = LogManager.GetCurrentClassLogger();
            catLog.SetProperty("Tag", "~~~~~~~~MyApp~~~~~~~~");
            if (ex is null)
            {
                catLog.Info(message);
            }
            else
            {
                catLog.Error(ex, message);
            }
        }
        
        public void Log(LogLevel level,
                        object   value)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        object          value)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        object   arg1,
                        object   arg2)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        object   arg1,
                        object   arg2,
                        object   arg3)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        bool            argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        bool     argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        char            argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        char     argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        byte            argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        byte     argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        string          argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        string   argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        int             argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        int      argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        long            argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        long     argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        float           argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        float    argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        double          argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        double   argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        decimal         argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        decimal  argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        object          argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        object   argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        sbyte           argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        sbyte    argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        uint            argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        uint     argument)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        ulong           argument)
        {
            
        }

        public void Log(LogLevel level,
                        string   message,
                        ulong    argument)
        {
            
        }

        public bool IsEnabled(LogLevel level)
        {
            return false;
        }

        public void Log(LogEventInfo   logEvent)
        {
            
        }

        public void Log(Type         wrapperType,
                        LogEventInfo logEvent)
        {
            
        }

        public void Log<T>(LogLevel level,
                           T        value)
        {
            
        }

        public void Log<T>(LogLevel        level,
                           IFormatProvider formatProvider,
                           T               value)
        {
            
        }

        public void Log(LogLevel            level,
                        LogMessageGenerator messageFunc)
        {
            
        }

        public void LogException(LogLevel  level,
                                 string    message,
                                 Exception exception)
        {
            
        }

        public void Log(LogLevel        level,
                        Exception       exception,
                        string          message,
                        params object[] args)
        {
            
        }

        public void Log(LogLevel        level,
                        Exception       exception,
                        IFormatProvider formatProvider,
                        string          message,
                        params object[] args)
        {
            
        }

        public void Log(LogLevel        level,
                        IFormatProvider formatProvider,
                        string          message,
                        params object[] args)
        {
            
        }

        public void Log(LogLevel level
                      , string   message)
        {
            
        }

        public void Log(LogLevel        level,
                        string          message,
                        params object[] args)
        {
            
        }
        
        public void Log<TArgument>(LogLevel        level,
                                   IFormatProvider formatProvider,
                                   string          message,
                                   TArgument       argument)
        {
            
        }

        public void Log<TArgument>(LogLevel  level,
                                   string    message,
                                   TArgument argument)
        {
            
        }

        public void Log<TArgument1, TArgument2>(LogLevel        level,
                                                IFormatProvider formatProvider,
                                                string          message,
                                                TArgument1      argument1,
                                                TArgument2      argument2)
        {
            
        }

        public void Log<TArgument1, TArgument2>(LogLevel   level,
                                                string     message,
                                                TArgument1 argument1,
                                                TArgument2 argument2)
        {
            
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel        level,
                                                            IFormatProvider formatProvider,
                                                            string          message,
                                                            TArgument1      argument1,
                                                            TArgument2      argument2,
                                                            TArgument3      argument3)
        {
            
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel   level,
                                                            string     message,
                                                            TArgument1 argument1,
                                                            TArgument2 argument2,
                                                            TArgument3 argument3)
        {
            
        }

        public string Name { get; }

        public LogFactory Factory { get; }

        public event EventHandler<EventArgs> LoggerReconfigured;

        public void Swallow(Action  action)
        {
            
        }

        public T Swallow<T>(Func<T> func)
        {
            return default;
        }

        public T Swallow<T>(Func<T> func
                          , T       fallback)
        {
            return default;
        }

        public void          Swallow(Task                              task) { }
        public Task          SwallowAsync(Task                         task)
        {
            return null;
        }

        public Task          SwallowAsync(Func<Task>                   asyncAction)
        {
            return null;
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            return null;
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc
                                                 , TResult             fallback)
        {
            return null;
        }

        public void Trace(object value)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          object          value)
        {
            
        }

        public void Trace(string message,
                          object arg1,
                          object arg2)
        {
            
        }

        public void Trace(string message,
                          object arg1,
                          object arg2,
                          object arg3)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          bool            argument)
        {
            
        }

        public void Trace(string message,
                          bool   argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          char            argument)
        {
            
        }

        public void Trace(string message,
                          char   argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          byte            argument)
        {
            
        }

        public void Trace(string message,
                          byte   argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          string          argument)
        {
            
        }

        public void Trace(string message,
                          string argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          int             argument)
        {
            
        }

        public void Trace(string message,
                          int    argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          long            argument)
        {
            
        }

        public void Trace(string message,
                          long   argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          float           argument)
        {
            
        }

        public void Trace(string message,
                          float  argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          double          argument)
        {
            
        }

        public void Trace(string message,
                          double argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          decimal         argument)
        {
            
        }

        public void Trace(string  message,
                          decimal argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          object          argument)
        {
            
        }

        public void Trace(string message,
                          object argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          sbyte           argument)
        {
            
        }

        public void Trace(string message,
                          sbyte  argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          uint            argument)
        {
            
        }

        public void Trace(string message,
                          uint   argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          ulong           argument)
        {
            
        }

        public void Trace(string message,
                          ulong  argument)
        {
            
        }

        public void Debug(object value)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          object          value)
        {
            
        }

        public void Debug(string message,
                          object arg1,
                          object arg2)
        {
            
        }

        public void Debug(string message,
                          object arg1,
                          object arg2,
                          object arg3)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          bool            argument)
        {
            
        }

        public void Debug(string message,
                          bool   argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          char            argument)
        {
            
        }

        public void Debug(string message,
                          char   argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          byte            argument)
        {
            
        }

        public void Debug(string message,
                          byte   argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          string          argument)
        {
            
        }

        public void Debug(string message,
                          string argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          int             argument)
        {
            
        }

        public void Debug(string message,
                          int    argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          long            argument)
        {
            
        }

        public void Debug(string message,
                          long   argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          float           argument)
        {
            
        }

        public void Debug(string message,
                          float  argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          double          argument)
        {
            
        }

        public void Debug(string message,
                          double argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          decimal         argument)
        {
            
        }

        public void Debug(string  message,
                          decimal argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          object          argument)
        {
            
        }

        public void Debug(string message,
                          object argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          sbyte           argument)
        {
            
        }

        public void Debug(string message,
                          sbyte  argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          uint            argument)
        {
            
        }

        public void Debug(string message,
                          uint   argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          ulong           argument)
        {
            
        }

        public void Debug(string message,
                          ulong  argument)
        {
            
        }

        public void Info(object value)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         object          value)
        {
            
        }

        public void Info(string message,
                         object arg1,
                         object arg2)
        {
            
        }

        public void Info(string message,
                         object arg1,
                         object arg2,
                         object arg3)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         bool            argument)
        {
            
        }

        public void Info(string message,
                         bool   argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         char            argument)
        {
            
        }

        public void Info(string message,
                         char   argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         byte            argument)
        {
            
        }

        public void Info(string message,
                         byte   argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         string          argument)
        {
            
        }

        public void Info(string message,
                         string argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         int             argument)
        {
            
        }

        public void Info(string message,
                         int    argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         long            argument)
        {
            
        }

        public void Info(string message,
                         long   argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         float           argument)
        {
            
        }

        public void Info(string message,
                         float  argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         double          argument)
        {
            
        }

        public void Info(string message,
                         double argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         decimal         argument)
        {
            
        }

        public void Info(string  message,
                         decimal argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         object          argument)
        {
            
        }

        public void Info(string message,
                         object argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         sbyte           argument)
        {
            
        }

        public void Info(string message,
                         sbyte  argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         uint            argument)
        {
            
        }

        public void Info(string message,
                         uint   argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         ulong           argument)
        {
            
        }

        public void Info(string message,
                         ulong  argument)
        {
            
        }

        public void Warn(object value)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         object          value)
        {
            
        }

        public void Warn(string message,
                         object arg1,
                         object arg2)
        {
            
        }

        public void Warn(string message,
                         object arg1,
                         object arg2,
                         object arg3)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         bool            argument)
        {
            
        }

        public void Warn(string message,
                         bool   argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         char            argument)
        {
            
        }

        public void Warn(string message,
                         char   argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         byte            argument)
        {
            
        }

        public void Warn(string message,
                         byte   argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         string          argument)
        {
            
        }

        public void Warn(string message,
                         string argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         int             argument)
        {
            
        }

        public void Warn(string message,
                         int    argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         long            argument)
        {
            
        }

        public void Warn(string message,
                         long   argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         float           argument)
        {
            
        }

        public void Warn(string message,
                         float  argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         double          argument)
        {
            
        }

        public void Warn(string message,
                         double argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         decimal         argument)
        {
            
        }

        public void Warn(string  message,
                         decimal argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         object          argument)
        {
            
        }

        public void Warn(string message,
                         object argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         sbyte           argument)
        {
            
        }

        public void Warn(string message,
                         sbyte  argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         uint            argument)
        {
            
        }

        public void Warn(string message,
                         uint   argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         ulong           argument)
        {
            
        }

        public void Warn(string message,
                         ulong  argument)
        {
            
        }

        public void Error(object value)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          object          value)
        {
            
        }

        public void Error(string message,
                          object arg1,
                          object arg2)
        {
            
        }

        public void Error(string message,
                          object arg1,
                          object arg2,
                          object arg3)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          bool            argument)
        {
            
        }

        public void Error(string message,
                          bool   argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          char            argument)
        {
            
        }

        public void Error(string message,
                          char   argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          byte            argument)
        {
            
        }

        public void Error(string message,
                          byte   argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          string          argument)
        {
            
        }

        public void Error(string message,
                          string argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          int             argument)
        {
            
        }

        public void Error(string message,
                          int    argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          long            argument)
        {
            
        }

        public void Error(string message,
                          long   argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          float           argument)
        {
            
        }

        public void Error(string message,
                          float  argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          double          argument)
        {
            
        }

        public void Error(string message,
                          double argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          decimal         argument)
        {
            
        }

        public void Error(string  message,
                          decimal argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          object          argument)
        {
            
        }

        public void Error(string message,
                          object argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          sbyte           argument)
        {
            
        }

        public void Error(string message,
                          sbyte  argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          uint            argument)
        {
            
        }

        public void Error(string message,
                          uint   argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          ulong           argument)
        {
            
        }

        public void Error(string message,
                          ulong  argument)
        {
            
        }

        public void Fatal(object value)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          object          value)
        {
            
        }

        public void Fatal(string message,
                          object arg1,
                          object arg2)
        {
            
        }

        public void Fatal(string message,
                          object arg1,
                          object arg2,
                          object arg3)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          bool            argument)
        {
            
        }

        public void Fatal(string message,
                          bool   argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          char            argument)
        {
            
        }

        public void Fatal(string message,
                          char   argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          byte            argument)
        {
            
        }

        public void Fatal(string message,
                          byte   argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          string          argument)
        {
            
        }

        public void Fatal(string message,
                          string argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          int             argument)
        {
            
        }

        public void Fatal(string message,
                          int    argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          long            argument)
        {
            
        }

        public void Fatal(string message,
                          long   argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          float           argument)
        {
            
        }

        public void Fatal(string message,
                          float  argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          double          argument)
        {
            
        }

        public void Fatal(string message,
                          double argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          decimal         argument)
        {
            
        }

        public void Fatal(string  message,
                          decimal argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          object          argument)
        {
            
        }

        public void Fatal(string message,
                          object argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          sbyte           argument)
        {
            
        }

        public void Fatal(string message,
                          sbyte  argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          uint            argument)
        {
            
        }

        public void Fatal(string message,
                          uint   argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          ulong           argument)
        {
            
        }

        public void Fatal(string message,
                          ulong  argument)
        {
            
        }

        public void Trace<T>(T value)
        {
            
        }

        public void Trace<T>(IFormatProvider formatProvider,
                             T               value)
        {
            
        }

        public void Trace(LogMessageGenerator messageFunc)
        {
            
        }

        public void TraceException(string    message,
                                   Exception exception)
        {
            
        }

        public void Trace(Exception exception,
                          string    message)
        {
            
        }

        public void Trace(Exception       exception,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Trace(Exception       exception,
                          IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Trace(IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Trace(string message)
        {
            
        }

        public void Trace(string          message,
                          params object[] args)
        {
            
        }

        public void Trace(string    message,
                          Exception exception)
        {
            
        }

        public void Trace<TArgument>(IFormatProvider formatProvider,
                                     string          message,
                                     TArgument       argument)
        {
            
        }

        public void Trace<TArgument>(string    message,
                                     TArgument argument)
        {
            
        }

        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider,
                                                  string          message,
                                                  TArgument1      argument1,
                                                  TArgument2      argument2)
        {
            
        }

        public void Trace<TArgument1, TArgument2>(string     message,
                                                  TArgument1 argument1,
                                                  TArgument2 argument2)
        {
            
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
                                                              string          message,
                                                              TArgument1      argument1,
                                                              TArgument2      argument2,
                                                              TArgument3      argument3)
        {
            
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(string     message,
                                                              TArgument1 argument1,
                                                              TArgument2 argument2,
                                                              TArgument3 argument3)
        {
            
        }

        public void Debug<T>(T value)
        {
            
        }

        public void Debug<T>(IFormatProvider formatProvider,
                             T               value)
        {
            
        }

        public void Debug(LogMessageGenerator messageFunc)
        {
            
        }

        public void DebugException(string    message,
                                   Exception exception)
        {
            
        }

        public void Debug(Exception exception,
                          string    message)
        {
            
        }

        public void Debug(Exception       exception,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Debug(Exception       exception,
                          IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Debug(IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Debug(string message)
        {
            
        }

        public void Debug(string          message,
                          params object[] args)
        {
            
        }

        public void Debug(string    message,
                          Exception exception)
        {
            
        }

        public void Debug<TArgument>(IFormatProvider formatProvider,
                                     string          message,
                                     TArgument       argument)
        {
            
        }

        public void Debug<TArgument>(string    message,
                                     TArgument argument)
        {
            
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider,
                                                  string          message,
                                                  TArgument1      argument1,
                                                  TArgument2      argument2)
        {
            
        }

        public void Debug<TArgument1, TArgument2>(string     message,
                                                  TArgument1 argument1,
                                                  TArgument2 argument2)
        {
            
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
                                                              string          message,
                                                              TArgument1      argument1,
                                                              TArgument2      argument2,
                                                              TArgument3      argument3)
        {
            
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(string     message,
                                                              TArgument1 argument1,
                                                              TArgument2 argument2,
                                                              TArgument3 argument3)
        {
            
        }

        public void Info<T>(T value)
        {
            
        }

        public void Info<T>(IFormatProvider formatProvider,
                            T               value)
        {
            
        }

        public void Info(LogMessageGenerator messageFunc)
        {
            
        }

        public void InfoException(string    message,
                                  Exception exception)
        {
            
        }

        public void Info(Exception exception,
                         string    message)
        {
            
        }

        public void Info(Exception       exception,
                         string          message,
                         params object[] args)
        {
            
        }

        public void Info(Exception       exception,
                         IFormatProvider formatProvider,
                         string          message,
                         params object[] args)
        {
            
        }

        public void Info(IFormatProvider formatProvider,
                         string          message,
                         params object[] args)
        {
            
        }

        public void Info(string message)
        {
            
        }

        public void Info(string          message,
                         params object[] args)
        {
            
        }

        public void Info(string    message,
                         Exception exception)
        {
            
        }

        public void Info<TArgument>(IFormatProvider formatProvider,
                                    string          message,
                                    TArgument       argument)
        {
            
        }

        public void Info<TArgument>(string    message,
                                    TArgument argument)
        {
            
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider,
                                                 string          message,
                                                 TArgument1      argument1,
                                                 TArgument2      argument2)
        {
            
        }

        public void Info<TArgument1, TArgument2>(string     message,
                                                 TArgument1 argument1,
                                                 TArgument2 argument2)
        {
            
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
                                                             string          message,
                                                             TArgument1      argument1,
                                                             TArgument2      argument2,
                                                             TArgument3      argument3)
        {
            
        }

        public void Info<TArgument1, TArgument2, TArgument3>(string     message,
                                                             TArgument1 argument1,
                                                             TArgument2 argument2,
                                                             TArgument3 argument3)
        {
            
        }

        public void Warn<T>(T value)
        {
            
        }

        public void Warn<T>(IFormatProvider formatProvider,
                            T               value)
        {
            
        }

        public void Warn(LogMessageGenerator messageFunc)
        {
            
        }

        public void WarnException(string    message,
                                  Exception exception)
        {
            
        }

        public void Warn(Exception exception,
                         string    message)
        {
            
        }

        public void Warn(Exception       exception,
                         string          message,
                         params object[] args)
        {
            
        }

        public void Warn(Exception       exception,
                         IFormatProvider formatProvider,
                         string          message,
                         params object[] args)
        {
            
        }

        public void Warn(IFormatProvider formatProvider,
                         string          message,
                         params object[] args)
        {
            
        }

        public void Warn(string message)
        {
            
        }

        public void Warn(string          message,
                         params object[] args)
        {
            
        }

        public void Warn(string    message,
                         Exception exception)
        {
            
        }

        public void Warn<TArgument>(IFormatProvider formatProvider,
                                    string          message,
                                    TArgument       argument)
        {
            
        }

        public void Warn<TArgument>(string    message,
                                    TArgument argument)
        {
            
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider,
                                                 string          message,
                                                 TArgument1      argument1,
                                                 TArgument2      argument2)
        {
            
        }

        public void Warn<TArgument1, TArgument2>(string     message,
                                                 TArgument1 argument1,
                                                 TArgument2 argument2)
        {
            
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
                                                             string          message,
                                                             TArgument1      argument1,
                                                             TArgument2      argument2,
                                                             TArgument3      argument3)
        {
            
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(string     message,
                                                             TArgument1 argument1,
                                                             TArgument2 argument2,
                                                             TArgument3 argument3)
        {
            
        }

        public void Error<T>(T value)
        {
            
        }

        public void Error<T>(IFormatProvider formatProvider,
                             T               value)
        {
            
        }

        public void Error(LogMessageGenerator messageFunc)
        {
            
        }

        public void ErrorException(string    message,
                                   Exception exception)
        {
            
        }

        public void Error(Exception exception,
                          string    message)
        {
            
        }

        public void Error(Exception       exception,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Error(Exception       exception,
                          IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Error(IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Error(string message)
        {
            
        }

        public void Error(string          message,
                          params object[] args)
        {
            
        }

        public void Error(string    message,
                          Exception exception)
        {
            
        }

        public void Error<TArgument>(IFormatProvider formatProvider,
                                     string          message,
                                     TArgument       argument)
        {
            
        }

        public void Error<TArgument>(string    message,
                                     TArgument argument)
        {
            
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider,
                                                  string          message,
                                                  TArgument1      argument1,
                                                  TArgument2      argument2)
        {
            
        }

        public void Error<TArgument1, TArgument2>(string     message,
                                                  TArgument1 argument1,
                                                  TArgument2 argument2)
        {
            
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
                                                              string          message,
                                                              TArgument1      argument1,
                                                              TArgument2      argument2,
                                                              TArgument3      argument3)
        {
            
        }

        public void Error<TArgument1, TArgument2, TArgument3>(string     message,
                                                              TArgument1 argument1,
                                                              TArgument2 argument2,
                                                              TArgument3 argument3)
        {
            
        }

        public void Fatal<T>(T value)
        {
            
        }

        public void Fatal<T>(IFormatProvider formatProvider,
                             T               value)
        {
            
        }

        public void Fatal(LogMessageGenerator messageFunc)
        {
            
        }

        public void FatalException(string    message,
                                   Exception exception)
        {
            
        }

        public void Fatal(Exception exception,
                          string    message)
        {
            
        }

        public void Fatal(Exception       exception,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Fatal(Exception       exception,
                          IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider,
                          string          message,
                          params object[] args)
        {
            
        }

        public void Fatal(string message)
        {
            
        }

        public void Fatal(string          message,
                          params object[] args)
        {
            
        }

        public void Fatal(string    message,
                          Exception exception)
        {
            
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider,
                                     string          message,
                                     TArgument       argument)
        {
            
        }

        public void Fatal<TArgument>(string    message,
                                     TArgument argument)
        {
            
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider,
                                                  string          message,
                                                  TArgument1      argument1,
                                                  TArgument2      argument2)
        {
            
        }

        public void Fatal<TArgument1, TArgument2>(string     message,
                                                  TArgument1 argument1,
                                                  TArgument2 argument2)
        {
            
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
                                                              string          message,
                                                              TArgument1      argument1,
                                                              TArgument2      argument2,
                                                              TArgument3      argument3)
        {
            
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(string     message,
                                                              TArgument1 argument1,
                                                              TArgument2 argument2,
                                                              TArgument3 argument3)
        {
            
        }

        public bool IsTraceEnabled { get; }

        public bool IsDebugEnabled { get; }

        public bool IsInfoEnabled { get; }

        public bool IsWarnEnabled { get; }

        public bool IsErrorEnabled { get; }

        public bool IsFatalEnabled { get; }
    }
}