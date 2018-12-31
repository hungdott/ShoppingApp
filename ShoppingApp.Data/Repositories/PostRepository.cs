using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Model.Models;

namespace ShoppingApp.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}