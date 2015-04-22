using Application.BoundedContext.MembershipModule;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET.MVC5.Client.Models
{
    public class ApplicationUserValidator : UserValidator<ApplicationUser, string>
  {
        public ApplicationUserValidator(ApplicationUserManager manager)
            : base(manager)
    {
    }
  }
}