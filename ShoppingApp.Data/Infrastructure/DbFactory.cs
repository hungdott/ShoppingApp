using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Infrastructure
{
  public class DbFactory : Disposable, IDbFactory
  {
    ShoppingAppDbContext dbContext;
    public ShoppingAppDbContext Init()
    {
      return dbContext ?? (dbContext = new ShoppingAppDbContext());
    }
    protected override void DisposeCore()
    {
      if(dbContext != null)
      {
        dbContext.Dispose();
      }
    }
  }
}
