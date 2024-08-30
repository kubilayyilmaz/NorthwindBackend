using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors.Autofac;

public abstract class MethodInterception : MethodInterceptionBaseAttribute
{
    //Yani bu benim bu kodu nasıl yorumlayacağımı anlatacak, yani bir methodun arasına girecek ama nerede nasıl yorumlayacak onu anlatacağız
    //invocation -> bizim methodumuz, çalıştırılmaya çalışan methodumuz
    //OnBefore -> Methodun önünde, yani method çalışmadan sen çalış
    protected virtual void OnBefore(IInvocation invocation)
    {

    }

    //OnAfter -> Method çalıştıktan sonra sen çalış
    protected virtual void OnAfter(IInvocation invocation)
    {

    }

    //OnException -> Method hata verdiğinde sen çalış
    protected virtual void OnException(IInvocation invocation, Exception exception)
    {

    }

    //OnSuccess -> Method başarılıysa sen çalış
    protected virtual void OnSuccess(IInvocation invocation)
    {

    }

    public override void Intercept(IInvocation invocation)
    {
        var isSuccess = true;

        OnBefore(invocation);

        try
        {
            invocation.Proceed();
        }
        catch (Exception e)
        {
            isSuccess = false;

            OnException(invocation, e);

            throw;
        }
        finally
        {
            if (isSuccess)
            {
                OnSuccess(invocation);
            }
        }

        OnAfter(invocation);
    }
}