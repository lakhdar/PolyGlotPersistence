namespace Application.BoundedConext.ERPModule
{
    using Domain.BoundedContext.ERPModule;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IOrganizationManagementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(Organization organization, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Organization>> GetAllFacilitiesAsync( CancellationToken cancellationToken = default(CancellationToken));
    }
}
