using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Service
{
  public interface IFooterService
  {
    Footer GetFooter(string id);
    void UpdateFooter(Footer footer);
    void Save();
  }
  public class FooterService : IFooterService
  {
    private IFooterRepository _footerRepository;
    private IUnitOfWork _unitOfWork;

    public FooterService(IFooterRepository footerRepository,IUnitOfWork unitOfWork)
    {
      this._footerRepository = footerRepository;
      this._unitOfWork = unitOfWork;
    }

    public Footer GetFooter(string id = "default")
    {
      return _footerRepository.GetSingleByCondition(X => X.ID == id);
    }
    public void UpdateFooter(Footer footer)
    {
      _footerRepository.Update(footer);
    }
    public void Save()
    {
      _unitOfWork.Commit();
    }

    
  }
}
