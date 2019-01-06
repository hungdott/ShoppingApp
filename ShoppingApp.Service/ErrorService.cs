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
  public interface IErrorService
  {
    Error Create(Error err);
    void Save();
  }
  public class ErrorService : IErrorService
  {
    IErrorRepository _errorRepository;
    IUnitOfWork _unitOFwork;
    public ErrorService(IErrorRepository errorRepository,IUnitOfWork unitOFwork) {
      this._errorRepository = errorRepository;
      this._unitOFwork = unitOFwork;
    }
    public Error Create(Error err)
    {
      return _errorRepository.Add(err);
    }

    public void Save()
    {
      _unitOFwork.Commit();
    }
  }
}
