namespace Application.BoundedContext.MembershipModule
{

    using Domain.BoundedContext.MembershipModule;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserManagementServices
    {
        Task AddAsync(User user, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync(User user, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveAsync(User user, CancellationToken cancellationToken = default(CancellationToken));

        User GetById(Guid id);

        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> GetByLoginAndPasswordAsync(string userName, string passWord, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<string>> GetRolesAsync(Guid userid, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Claim>> GetClaimsAsync(Guid userid, CancellationToken cancellationToken = default(CancellationToken));

        Task AddLoginAsync(User user, Login login, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveLoginAsync(User user, Login login, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Login>> GetLoginsAsync(User user, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> GetByLoginAsync(Login login, CancellationToken cancellationToken = default(CancellationToken));

        Task AddClaimAsync(User user, Claim claim, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveClaimAsync(User user, Claim claim, CancellationToken cancellationToken = default(CancellationToken));

        Task AddRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken));
    }
}
