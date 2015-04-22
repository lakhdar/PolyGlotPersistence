namespace ASP.NET.MVC5.Client.Models
{
    using Domain.BoundedContext.ERPModule;
    using Domain.BoundedContext.MembershipModule;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    public static class MembershipMapperExtesnion
    {
        public static ApplicationUser FromDomainUser(this User model)
        {
            ApplicationUser userModel = null;
            if (model != null)
            {
                userModel = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.PasswordHash,
                    SecurityStamp = model.SecurityStamp,
                    LockoutEnabled = model.LockoutEnabled,
                    AccessFailedCount = model.AccessFailedCount,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed
                };

                if (!model.IsTransient())
                {
                    userModel.Id = model.Id.ToString();
                }
            }

            return userModel;
        }

        public static User ToDomainUser(this ApplicationUser model)
        {
            User user = null;
            if (model != null)
            {
                user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.PasswordHash,
                    SecurityStamp = model.SecurityStamp,
                    LockoutEnabled = model.LockoutEnabled,
                    LastName = "test",
                    FirstName = "test",
                    BirthDate = DateTime.Now,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    Address = new Address()
                    {
                        Id = Guid.NewGuid(),
                        AddressLine1 = "test",
                        ZipCode = "test",
                        City = "test",
                    },

                    Roles = new List<Role>()
                                    {
                                       new Role(){
                                           Id = Guid.NewGuid(),
                                           Name = "test"
                                       }
                                    },
                    Claims = new List<Claim>(),
                    Logins = new List<Login>(),
                };

                if (!string.IsNullOrEmpty(model.Id))
                {
                    user.Id = new Guid(model.Id);
                }
            }

            return user;
        }
        public static Login ToDomainLogin(this UserLoginInfo model)
        {
            Login login = (Login)null;
            if (model != null)
                login = new Login()
                {
                    LoginProvider = model.LoginProvider,
                    ProviderKey = model.ProviderKey,
                    ProviderDisplayName = model.ProviderKey
                };
            return login;
        }

        public static UserLoginInfo ToLoginInfo(this Login model)
        {
            UserLoginInfo userLoginInfo = (UserLoginInfo)null;
            if (model != (Login)null)
                userLoginInfo = new UserLoginInfo(model.LoginProvider, model.ProviderKey);
            return userLoginInfo;
        }

        public static Claim ToDomainClaim(this System.Security.Claims.Claim model)
        {
            Claim userClaim1 = (Claim)null;
            if (model != null)
            {
                userClaim1 = new Claim();
                userClaim1.ClaimType = model.Type;
                userClaim1.ClaimValue = model.Value;
                userClaim1.Description = model.ValueType;
            }

            return userClaim1;
        }

        public static System.Security.Claims.Claim ToSecurityClaim(this Claim model)
        {
            System.Security.Claims.Claim claim = null;
            if (model != null)
                claim = new System.Security.Claims.Claim(model.ClaimType, model.ClaimValue, "http://www.w3.org/2001/XMLSchema#string");
            return claim;
        }




    }
}
