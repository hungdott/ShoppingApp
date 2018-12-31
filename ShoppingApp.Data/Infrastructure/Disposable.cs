using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Infrastructure
{
  public class Disposable : IDisposable
  {
    private bool isDisposed;
    ~Disposable()
    {
      Dispose(false);
    }
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    private void Dispose(bool disposing)
    {
      if(!isDisposed && disposing)
      {
        DisposeCore();
      }
      isDisposed = true;
    }
    //override this to dispose custom obj
    protected virtual void DisposeCore()
    {

    }
  }
}
