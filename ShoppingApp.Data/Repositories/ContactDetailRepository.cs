using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Repositories
{
  public interface IContactDetailRepository:IRepository<ContactDetail>
  {

  }
  public class ContactDetailRepository : RepositoryBase<ContactDetail>,IContactDetailRepository
  {
    public ContactDetailRepository(IDbFactory dbFactory) : base(dbFactory)
    {

    }
  }
}
