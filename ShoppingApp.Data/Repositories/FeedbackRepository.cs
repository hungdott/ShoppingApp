using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Repositories
{
  public interface IFeedbackRepository: IRepository<Feedback>
  {

  }
  public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
  {
    public FeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
    {
    }
  }
}
