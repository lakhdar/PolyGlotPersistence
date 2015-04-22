using Domain.BoundedContext.ERPModule;
using Infrastructure.CrossCutting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BoundedConext.ERPModule
{
    public class OrganizationManagementService : IOrganizationManagementService
    {
         private IOrganizationRepository _organizationRepository;
        private ILogger _logger;

        public OrganizationManagementService(IOrganizationRepository organizationRepository, ILogger logger)
        {
            if (organizationRepository == (IOrganizationRepository)null)
                throw new ArgumentNullException("organizationRepository");
            if (logger == (ILogger)null)
                throw new ArgumentNullException("logger");
            this._organizationRepository = organizationRepository;
            this._logger = logger;
        }


        public Task AddAsync(Organization organization, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (organization == (Organization)null)
                throw new ArgumentNullException("organization");


              this._organizationRepository.Add(organization);
            return this._organizationRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public Task<IEnumerable<Organization>> GetAllFacilitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<IEnumerable<Organization>>(this._organizationRepository.GetAllElements());
        }
    }
}
