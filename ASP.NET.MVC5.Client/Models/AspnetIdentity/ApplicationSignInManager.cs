namespace ASP.NET.MVC5.Client.Models
{
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System.Security.Claims;
    using System.Threading.Tasks;
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
    }
}