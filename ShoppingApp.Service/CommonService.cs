using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Common;

namespace ShoppingApp.Service
{
  public interface ICommonService
  {
    Footer GetFooter();
    IEnumerable<Slide> GetSlides();
  }
  public class CommonService : ICommonService
  {
    IFooterRepository _footerRepository;
    IUnitOfWork _unitOfWord;
    ISlideRepository _slideRepository;
    public CommonService(IFooterRepository footerRepository,IUnitOfWork unitOfWord, ISlideRepository slideRepository)
    {
      _footerRepository = footerRepository;
      _unitOfWord = unitOfWord;
      _slideRepository = slideRepository;
    }
    public Footer GetFooter()
    {
      return _footerRepository.GetSingleByCondition(x=>x.ID==CommonConstants.DefaultFooterId);
    }

    public IEnumerable<Slide> GetSlides()
    {
      return _slideRepository.GetMulti(x => x.Status == true);
    }
  }
}
