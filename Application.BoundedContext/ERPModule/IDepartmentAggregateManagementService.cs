using Domain.BoundedContext.ERPModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BoundedConext.ERPModule
{
   public interface IDepartmentAggregateManagementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       Task AddDepartmentAsync(DepartmentAggregate department, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       Task<IEnumerable<DepartmentAggregate>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
