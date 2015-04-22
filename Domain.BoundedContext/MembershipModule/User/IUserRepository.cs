namespace Domain.BoundedContext.MembershipModule
{
    using Domain.Core;

    /// <summary>
    /// User repository contract
    /// <see cref="Domain.Core.IRepository{User}"/>
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
    }
}
