using System;
using System.Collections.Generic;
using System.Text;

namespace Ludwig.Core.AppModels
{
    public enum LogLevel
    {
        CRITICAL = 50,
        FATAL = CRITICAL,
        ERROR = 40,
        WARNING = 30,
        WARN = WARNING,
        INFO = 20,
        DEBUG = 10,
        NOTSET = 0,
    }
}
