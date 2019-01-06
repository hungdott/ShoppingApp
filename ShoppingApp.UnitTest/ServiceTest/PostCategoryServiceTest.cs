using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Service;
using ShoppingApp.Model.Models;

namespace ShoppingApp.UnitTest.ServiceTest
{
  [TestClass]
  public class PostCategoryServiceTest
  {
    private Mock<IPostCategoryRepository> _mockRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private IPostCategoryService _categoryService;
    private List<PostCategory> _listCategory;

    [TestInitialize]
    public void Initialize()
    {
      _mockRepository = new Mock<IPostCategoryRepository>();
      _mockUnitOfWork = new Mock<IUnitOfWork>();
      _categoryService = new PostCategoryService(_mockRepository.Object,_mockUnitOfWork.Object);
      _listCategory = new List<PostCategory>()
      {
        new PostCategory() {ID=1,Name="DM1",Status=true },
        new PostCategory() {ID=2,Name="DM2",Status=true },
        new PostCategory() {ID=3,Name="DM3",Status=true }
      };
    }
    [TestMethod]
    public void PostCategoryService_GetAll()
    {
      //setup method
      _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);
      //call action
      var result = _categoryService.GetAll() as List<PostCategory>;
      //compare
      Assert.IsNotNull(result);
      Assert.AreEqual(3, result.Count);
    }
    [TestMethod]
    public void PostCategoryService_Create()
    {
      PostCategory category = new PostCategory();
      int id = 1;
      category.Name = "Test";
      category.Alias = "test";
      category.Status = true;
      _mockRepository.Setup(m => m.Add(category)).Returns((PostCategory p) =>
        {
          p.ID = 1;
          return p; 
        });
      var result = _categoryService.Add(category);
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.ID);
    }
  }
}
