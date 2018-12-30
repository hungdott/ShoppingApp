using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Infrastructure
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly IDbFactory dbFactory;
    private ShoppingAppDbContext dbContext;
    public UnitOfWork(IDbFactory dbFactory)
    {
      this.dbFactory = dbFactory;
    }
    public ShoppingAppDbContext DbContext
    {
      get{ return dbContext ?? (dbContext = dbFactory.Init()); }
    }
    public void Commit()
    {
      DbContext.SaveChanges();
    }
  }
}
