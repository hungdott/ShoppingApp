using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Model.Models;

namespace ShoppingApp.Data.Repositories
{
  public interface IErrorRepository : IRepository<Error>
  {
  }

  public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
  {
    public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
    {
    }
  }
}
