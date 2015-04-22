using Application.BoundedConext.Resources;
using Domain.BoundedContext.MembershipModule;
using Domain.Core;
using Infrastructure.CrossCutting.Core;
using Infrastructure.Data.BoundedContext.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BoundedContext.MembershipModule
{
    public class UserManagementServices : IUserManagementServices
    {
        private IUserRepository _userRepository;
        private ILogger _logger;

        public UserManagementServices(IUserRepository userRepository, ILogger logger)
        {
            if (userRepository == (IUserRepository)null)
                throw new ArgumentNullException("applicationUserRepository");
            if (logger == (ILogger)null)
                throw new ArgumentNullException("logger");
            this._userRepository = userRepository;
            this._logger = logger;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == (User)null)
                throw new ArgumentNullException("user");

            IMainBCUnitOfWork unitOfWork = this._userRepository.UnitOfWork as IMainBCUnitOfWork;
            this._userRepository.Add(user);
            await unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == (User)null)
                throw new ArgumentNullException("user");
            this._userRepository.SetModified(user);
            await this._userRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task RemoveAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == (User)null)
                throw new ArgumentNullException("user");
            this._userRepository.Remove(user);
            await this._userRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public User GetById(Guid id)
        {
            return this._userRepository.GetElementById(id);
        }

        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this._userRepository.GetElementByIdAsync(id, cancellationToken);
        }

        public Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace("userName"))
                throw new ArgumentNullException("userName");
            return this._userRepository.GetFirstOrDefaultAsync((u => u.UserName != null && u.UserName.ToLower() == userName.ToLower()), cancellationToken);
        }

        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");
            return this._userRepository.GetFirstOrDefaultAsync((x => x.Email != (object)null && x.Email.ToLower() == email.ToLower()), cancellationToken);
        }

        public async Task<User> GetByLoginAndPasswordAsync(string userName, string passWord, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");
            if (string.IsNullOrWhiteSpace(passWord))
                throw new ArgumentNullException("passWord");
            return await this._userRepository.GetFirstOrDefaultAsync((u => u.UserName != (object)null && u.UserName.ToLower() == userName.ToLower() && u.PasswordHash != null && u.PasswordHash.ToLower() == passWord.ToLower()), cancellationToken);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(Guid userid, CancellationToken cancellationToken = default(CancellationToken))
        {
            User user = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == userid), new CancellationToken(), (x => x.Roles));
            return !(user == (User)null) && user.Roles != null ? user.Roles.Select(x => x.Name) : null;
        }

        public Task<IEnumerable<Claim>> GetClaimsAsync(Guid userid, CancellationToken cancellationToken = default(CancellationToken))
        {
            User user = this._userRepository.GetElementById(userid);
            if (user == (User)null || user.Claims == null)
                return null;
            else
                return Task.FromResult<IEnumerable<Claim>>(user.Claims);
        }

        public async Task AddLoginAsync(User user, Login login, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == (User)null)
                throw new ArgumentNullException("user");
            if (login == (Login)null)
                throw new ArgumentNullException("login");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), new CancellationToken(), (x => x.Logins));

            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + user.Id)));

            if (dbUser.Logins == null)
                dbUser.Logins = new List<Login>();

            dbUser.Logins.Add(login);

            this._userRepository.SetModified(dbUser);


            await this._userRepository.UnitOfWork.CommitAsync();
        }

        public async Task RemoveLoginAsync(User user, Login login, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == (User)null)
                throw new ArgumentNullException("user");
            if (login == (Login)null)
                throw new ArgumentNullException("login");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken, (x => x.Logins));
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + user.Id)));

            if (dbUser.Logins != null)
            {
                Login log = (Login)null;
                log = (login.Id == Guid.Empty) ?
                    dbUser.Logins.FirstOrDefault(x => x.ProviderKey != null && x.ProviderKey == login.ProviderKey && x.LoginProvider != null && x.LoginProvider == login.LoginProvider) :
                    dbUser.Logins.FirstOrDefault(x => x.Id == login.Id);
                if (log == (Login)null)
                    throw new Exception(Messages.RemoveNonExistingException);
                dbUser.Logins.Remove(log);
                this._userRepository.SetModified(dbUser);

                await this._userRepository.UnitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Login>> GetLoginsAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == (User)null)
                throw new ArgumentNullException("user");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken, (x => x.Logins));
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + user.Id)));
            else
                return dbUser.Logins;
        }

        public Task<User> GetByLoginAsync(Login login, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (login == (Login)null)
                throw new ArgumentNullException("login");
            return this._userRepository.GetFirstOrDefaultAsync(x => x.Logins != null && x.Logins.Any(l => l.Id == login.Id || l.ProviderKey == login.ProviderKey), cancellationToken);
        }

        public async Task AddClaimAsync(User user, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == (User)null)
                throw new ArgumentNullException("user");
            if (claim == (Claim)null)
                throw new ArgumentNullException("claim");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken, (x => x.Claims));
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + user.Id)));
            if (dbUser.Claims == null)
                dbUser.Claims = (ICollection<Claim>)new List<Claim>();
            dbUser.Claims.Add(claim);
            this._userRepository.SetModified(dbUser);
            await this._userRepository.UnitOfWork.CommitAsync();
        }

        public async Task RemoveClaimAsync(User user, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == (User)null)
                throw new ArgumentNullException("user");
            if (claim == (Claim)null)
                throw new ArgumentNullException("claim");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken, (x => x.Claims));
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + user.Id)));
            if (dbUser.Claims != null)
            {
                Claim clm = (Claim)null;
                clm = (claim.Id == Guid.Empty) ?
                     dbUser.Claims.FirstOrDefault(x => x.ClaimType != null && x.ClaimType == claim.ClaimType && x.ClaimValue != null && x.ClaimValue == claim.ClaimValue) :
                     dbUser.Claims.FirstOrDefault(x => x.Id == claim.Id);
                if (clm == (Claim)null)
                    throw new Exception(Messages.RemoveNonExistingClaimException);
                dbUser.Claims.Remove(clm);
                this._userRepository.SetModified(dbUser);
                await this._userRepository.UnitOfWork.CommitAsync();
            }
        }

        public async Task AddRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == (User)null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken, (x => x.Roles));
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + (object)user.Id)));
            if (dbUser.Roles == null)
                dbUser.Roles = new List<Role>();

            if (dbUser.Roles.Any(x => string.Equals(x.Name, roleName, StringComparison.InvariantCultureIgnoreCase)))
                throw new Exception(string.Format(Messages.RoleAlreadyExistException, roleName));
            dbUser.Roles.Add(new Role()
            {
                Name = roleName
            });
            this._userRepository.SetModified(dbUser);
            await this._userRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == (User)null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken,  x => x.Roles);
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + user.Id)));
            else
                return dbUser.Roles != null && dbUser.Roles.Any<Role>(x => string.Equals(x.Name, roleName, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task RemoveRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == (User)null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken, (x => x.Roles));
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException, ("User id=" + user.Id)));
            if (dbUser.Roles != null)
            {
                Role role = dbUser.Roles.FirstOrDefault((x => string.Equals(x.Name, roleName, StringComparison.InvariantCultureIgnoreCase)));
                if (role == (Role)null)
                    throw new Exception(Messages.RemoveNonExistingRoleException);
                dbUser.Roles.Remove(role);
                this._userRepository.SetModified(dbUser);
                await this._userRepository.UnitOfWork.CommitAsync();
            }
        }

        private async Task<User> GetUserWithClaims(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == (User)null)
                throw new ArgumentNullException("user");
            User dbUser = await this._userRepository.GetFirstOrDefaultAsync((x => x.Id == user.Id), cancellationToken, (x => x.Claims));
            if (dbUser == (User)null)
                throw new Exception(string.Format(Messages.UserNotFoundException,("User id=" + user.Id)));
            else
                return dbUser;
        }
    }
}
