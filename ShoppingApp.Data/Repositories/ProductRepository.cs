using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Model.Models;

namespace ShoppingApp.Data.Repositories
{
  public interface IProductRepository
  {

  }
  public class ProductRepository:RepositoryBase<Product>,IProductRepository
  {
    public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
    {

    }
  }
}
