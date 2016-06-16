using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;

namespace InsuranceV2.Infrastructure.Database
{
    public class EfCommandInterceptor : IDbCommandInterceptor
    {
        private static Stopwatch _stopwatch = new Stopwatch();
        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Restart();
            Log($"NonQueryExecuting: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log($"NonQueryExecuted after {_stopwatch.Elapsed}: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            _stopwatch.Restart();
            Log($"ReaderExecuting: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log($"ReaderExecuted after {_stopwatch.Elapsed}: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Restart();
            Log($"ScalarExecuting: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Log($"ScalarExecuted after {_stopwatch.Elapsed}: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext interceptionContext)
        {
            _stopwatch.Restart();
            Log($"ReaderExecuting: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext interceptionContext)
        {
            Log($"ReaderExecuted after {_stopwatch.Elapsed}: IsAsync: {interceptionContext.IsAsync}, Command Text: {command.CommandText}");
        }

        public static void Log(string command)
        {
            Console.WriteLine($"Intercepted: {command}");
        }
    }
}