using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Model.Models;

namespace ShoppingApp.UnitTest.RepositoryTest
{
  [TestClass]
  public class PostCategoryRepositoryTest
  {
    IDbFactory dbFactory;
    IPostCategoryRepository objRepository;
    IUnitOfWork unitOfWork;

    [TestInitialize]
    public void Initialize()
    {
      dbFactory = new DbFactory();
      objRepository = new PostCategoryRepository(dbFactory);
      unitOfWork = new UnitOfWork(dbFactory);
    }
    [TestMethod]
    public void PostCategory_Repository_GetAll()
    {
      var list = objRepository.GetAll().ToList();
      Assert.AreEqual(3, list.Count);
    }
    [TestMethod]
    public void PostCategory_Repository_Create()
    {
      PostCategory category = new PostCategory();
      category.Name = "test category";
      category.Alias = "test - category";
      category.Status =true;
      var result = objRepository.Add(category);
      unitOfWork.Commit();
      Assert.IsNotNull(result);
      Assert.AreEqual(2, result.ID);
    }
  }
}