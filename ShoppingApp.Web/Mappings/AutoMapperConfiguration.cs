﻿using AutoMapper;
using ShoppingApp.Model.Models;
using ShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApp.Web.Mappings
{
  public class AutoMapperConfiguration
  {
    public static void Configure()
    {
      Mapper.CreateMap<Post, PostViewModel>();
      Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
      Mapper.CreateMap<Tag, TagViewModel>();

      Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
      Mapper.CreateMap<Product, ProductViewModel>();
      Mapper.CreateMap<ProductTag, ProductTagViewModel>();
      Mapper.CreateMap<Footer, FooterViewModel>();
      Mapper.CreateMap<Slide, SlideViewModel>();
      Mapper.CreateMap<Page, PageViewModel>();
      Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();

      Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
      Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
      Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
    }
  }
}