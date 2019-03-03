using ShoppingApp.Model.Models;
using System;
using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Service
{
  public interface IFeedbackService
  {
    Feedback Create(Feedback feedBack);
    void Save();
  }
  public class FeedbackService : IFeedbackService
  {
    private IFeedbackRepository _feedbackRepository;
    private IUnitOfWork _unitOfWork;
    public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
    {
      _feedbackRepository = feedbackRepository;
      _unitOfWork = unitOfWork;
    }
    public Feedback Create(Feedback feedBack)
    {

      return _feedbackRepository.Add(feedBack);
    }

    public void Save()
    {
      _unitOfWork.Commit();
    }
  }
}
