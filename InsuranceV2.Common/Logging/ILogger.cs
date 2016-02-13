using System;

namespace InsuranceV2.Common.Logging
{
    public interface ILogger<T>
    {
        void Debug(object message);
        void Info(object message);
        void Warn(object message);
        void Error(object message);
        void Error(object message, Exception exception);
        void Fatal(object message);
        void Fatal(object message, Exception exception);
    }
}