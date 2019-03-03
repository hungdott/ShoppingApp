using ShoppingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShoppingApp.Web.Models;
using ShoppingApp.Model.Models;
using AutoMapper;

namespace ShoppingApp.Web.Controllers
{
  public class ContactDetailController : Controller
  {
    IContactDetailService _contactDetailService;
    public ContactDetailController(IContactDetailService contactDetailService)
    {
      this._contactDetailService = contactDetailService;
    }
    // GET: ContactDetail
    public ActionResult Index()
    {
      var model = _contactDetailService.GetDefaultContact();
      var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
      return View(contactViewModel);
    }
  }
}