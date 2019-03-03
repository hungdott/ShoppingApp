﻿using ShoppingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingApp.Web.Models;
using ShoppingApp.Model.Models;
using AutoMapper;
using ShoppingApp.Web.Infrastructure.Extensions;
using BotDetect.Web.Mvc;
using System.Text;
using ShoppingApp.Common;

namespace ShoppingApp.Web.Controllers
{
  public class ContactDetailController : Controller
  {
    IContactDetailService _contactDetailService;
    IFeedbackService _feedbackService;
    public ContactDetailController(IContactDetailService contactDetailService,IFeedbackService feedbackService)
    {
      this._contactDetailService = contactDetailService;
      this._feedbackService = feedbackService;
    }
    // GET: ContactDetail
    public ActionResult Index()
    {
      
      FeedbackViewModel viewModel = new FeedbackViewModel();
      viewModel.ContactDetail = GetDetail();

      return View(viewModel);
    }
    [HttpPost]
    [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
    public ActionResult SendFeeback(FeedbackViewModel feedbackViewModel)
    {
      if (ModelState.IsValid)
      {
        Feedback newFeedback = new Feedback();
        newFeedback.UpdateFeedback(feedbackViewModel);
        _feedbackService.Create(newFeedback);
        _feedbackService.Save();
        ViewData["SuccessMsg"] = "Gửi phản hồi thành công";

        string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
        content = content.Replace("{{Name}}", feedbackViewModel.Name);
        content = content.Replace("{{Email}}", feedbackViewModel.Email);
        content = content.Replace("{{Message}}", feedbackViewModel.Message);
        var adminEmail = ConfigHelper.GetByKey("AdminEmail");
        MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);
        feedbackViewModel.Name = "";
        feedbackViewModel.Message = "";
        feedbackViewModel.Email = "";
      }
      feedbackViewModel.ContactDetail = GetDetail();
      return View("Index", feedbackViewModel);

    }

    private ContactDetailViewModel GetDetail() {
      var model = _contactDetailService.GetDefaultContact();
      var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
      return contactViewModel;
    }
  }
}