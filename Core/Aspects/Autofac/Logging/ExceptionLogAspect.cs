using System;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Logging;

public class ExceptionLogAspect : MethodInterception
{
    private LoggerServiceBase _loggerServiceBase;

    public ExceptionLogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new Exception(AspectMessages.WrongLoggerType);
        }

        _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
    }

    protected override void OnException(IInvocation invocation, Exception e)
    {
        LogDetailWithException logDetailWithException = GetLogDetail(invocation);
        logDetailWithException.ExceptionMessage = e.Message;

        _loggerServiceBase.Error(logDetailWithException);
    }

    private LogDetailWithException GetLogDetail(IInvocation invocation)
    {
        var logParameters = new List<LogParameter>();

        for (int i = 0; i < invocation.Arguments.Length; i++)
        {
            logParameters.Add(new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Value = invocation.Arguments[i],
                Type = invocation.Arguments[i].GetType().Name,
            });
        }

        var logDetail = new LogDetailWithException{
            MethodName = invocation.Method.Name,
            LogParameters = logParameters
        };

        return logDetail;
    }
}
