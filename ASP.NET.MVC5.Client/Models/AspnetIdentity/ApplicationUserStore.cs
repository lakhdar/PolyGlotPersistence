namespace ASP.NET.MVC5.Client.Models
{
    using Application.BoundedContext.MembershipModule;
    using Domain.BoundedContext.MembershipModule;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class ApplicationUserStore : IApplicationUserStore
    {
        private IUserManagementServices _userManagementService;
        private bool _disposed;

        public IQueryable<ApplicationUser> Users
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ApplicationUserStore(IUserManagementServices userManagementService)
        {

            if (userManagementService == null)
                throw new ArgumentNullException("userManagementService");
            this._userManagementService = userManagementService;
        }


        public async virtual Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            this.ThrowIfDisposed();
            await this._userManagementService.AddLoginAsync(user.ToDomainUser(), login.ToDomainLogin(), new CancellationToken());
        }

        public async virtual   Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            this.ThrowIfDisposed();
            User user = await this._userManagementService.GetByLoginAsync(login.ToDomainLogin(), new CancellationToken());
             return  user.FromDomainUser() ;
        }

        public async virtual Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();
            IEnumerable<Login> list = await this._userManagementService.GetLoginsAsync(user.ToDomainUser(), new CancellationToken());
            if (list != null)
            {
                return list.Select(x => x.ToLoginInfo()).ToList();
            }

            return null;
        }

        public async virtual Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            this.ThrowIfDisposed();
            await this._userManagementService.RemoveLoginAsync(user.ToDomainUser(), login.ToDomainLogin(), new CancellationToken());
        }

        public async virtual Task CreateAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();
            await this._userManagementService.AddAsync(user.ToDomainUser(),  new CancellationToken());
        }

        public async virtual Task DeleteAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();
            await this._userManagementService.RemoveAsync(user.ToDomainUser(), new CancellationToken());
        }

        public async virtual Task<ApplicationUser> FindByIdAsync(string userId)
        {
            this.ThrowIfDisposed();
            User user = await this._userManagementService.GetByIdAsync(new Guid(userId), new CancellationToken());
            return user.FromDomainUser();
        }

        public async virtual Task<ApplicationUser> FindByNameAsync(string userName)
        {
            this.ThrowIfDisposed();
            User user = await this._userManagementService.GetByUserNameAsync(userName, new CancellationToken());
            return user.FromDomainUser();
        }

        public async virtual Task UpdateAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();
            await this._userManagementService.UpdateAsync(user.ToDomainUser(), new CancellationToken());
        }

        public async  Task AddClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            this.ThrowIfDisposed();
            await this._userManagementService.AddClaimAsync(user.ToDomainUser(), claim.ToDomainClaim(), new CancellationToken());
        }

        public async Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();

            Guid userid = new Guid(user.Id);
            IEnumerable<Claim> userClaims = await this._userManagementService.GetClaimsAsync(userid, new CancellationToken());
            if (userClaims != null)
            {
                return userClaims.Select(x => new System.Security.Claims.Claim(x.ClaimType, x.ClaimValue, "http://www.w3.org/2001/XMLSchema#string")).ToList();
            }
            return null;

        }

        public async Task RemoveClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            this.ThrowIfDisposed();

            await this._userManagementService.RemoveClaimAsync(user.ToDomainUser(), claim.ToDomainClaim(), new CancellationToken());
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            this.ThrowIfDisposed();
            await this._userManagementService.AddRoleAsync(user.ToDomainUser(), roleName, new CancellationToken());
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();

            IEnumerable<string> list = await this._userManagementService.GetRolesAsync(new Guid(user.Id), new CancellationToken());
            if (list != null)
            {
                return list.ToList();
            }

            return null;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            this.ThrowIfDisposed();
            return await this._userManagementService.IsInRoleAsync(user.ToDomainUser(), roleName, new CancellationToken());
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            this.ThrowIfDisposed();

            await this._userManagementService.RemoveRoleAsync(user.ToDomainUser(), roleName, new CancellationToken());
        }
        /// <summary>
        ///     Get the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        ///     Returns true if the user has a password set
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> HasPasswordAsync(ApplicationUser user )
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        /// <summary>
        ///     Set the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the security stamp for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.SecurityStamp);
        }

        /// <summary>
        ///     Set the security stamp for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
             this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Find an user by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async virtual Task<ApplicationUser> FindByEmailAsync(string email)
        {
            this.ThrowIfDisposed();
            User user = await this._userManagementService.GetByEmailAsync(email, new CancellationToken());
            return user.FromDomainUser();
        }

        /// <summary>
        ///     Get the user's email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> GetEmailAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.Email);
        }

        /// <summary>
        ///     Returns whether the user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        ///     Set the user email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetEmailAsync(ApplicationUser user, string email)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Email = email;
            return Task.FromResult(0);
        }


        /// <summary>
        ///     Set IsConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get a user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        ///     Returns whether the user phoneNumber is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        ///     Set the user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Set PhoneNumberConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Gets whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>
        ///     Set whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Returns the current number of failed access attempts.  This number usually will be reset whenever the password is
        ///     verified or the account is locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        ///     Returns whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        ///     Returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should be considered
        ///     not locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            DateTime? lockoutEndDateUtc = user.LockoutEndDateUtc;
            DateTimeOffset result;
            if (!lockoutEndDateUtc.HasValue)
            {
                result = new DateTimeOffset();
            }
            else
            {
                lockoutEndDateUtc = user.LockoutEndDateUtc;
                result = new DateTimeOffset(DateTime.SpecifyKind(lockoutEndDateUtc.Value, DateTimeKind.Utc));
            }
            return Task.FromResult<DateTimeOffset>(result);
        }

        /// <summary>
        ///     Used to record when an attempt to access the user has failed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        ///     Used to reset the account access count, typically after the account is successfully accessed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Sets whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        // <summary>
        ///     Locks a user out until the specified end date (set to a past date, to unlock a user)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            this.ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : new DateTime?(lockoutEnd.UtcDateTime);
            return Task.FromResult(0);
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        public void Dispose()
        {
            this._disposed = true;
        }

    }
}
