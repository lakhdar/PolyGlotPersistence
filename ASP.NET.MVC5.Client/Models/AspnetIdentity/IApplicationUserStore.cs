namespace ASP.NET.MVC5.Client.Models
{
    using Application.BoundedContext.MembershipModule;
    using Microsoft.AspNet.Identity;
    using System;
    public interface IApplicationUserStore : IUserLoginStore<ApplicationUser, string>,
        IUserClaimStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>,
        IQueryableUserStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserPhoneNumberStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser, string>,
        IUserLockoutStore<ApplicationUser, string>,
        IUserStore<ApplicationUser>, 
        IDisposable
    {
    }
}
