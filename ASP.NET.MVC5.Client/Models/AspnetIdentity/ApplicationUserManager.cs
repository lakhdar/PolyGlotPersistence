

namespace ASP.NET.MVC5.Client.Models 
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.DataProtection;
    using System;

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IApplicationUserStore store)
            : base((IUserStore<ApplicationUser>)store)
        {

              this.UserValidator =    new ApplicationUserValidator(this)
              {
                  AllowOnlyAlphanumericUserNames = false,
                 RequireUniqueEmail = true
              };
           
            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Phone Code", (IUserTokenProvider<ApplicationUser, string>)new PhoneNumberModelTokenManagementServiceProvider()
            {
                MessageFormat = "Your security code is {0}"
            });

            this.RegisterTwoFactorProvider("Email Code", (IUserTokenProvider<ApplicationUser, string>)new EmailModelTokenManegementServiceProvider()
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            this.EmailService = new EmailManagementServices();
            this.SmsService = new SmsManagementServices();

            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");

                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector);
            }
        }

    }
}