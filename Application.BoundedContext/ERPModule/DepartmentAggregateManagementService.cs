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
    public class DepartmentAggregateManagementService : IDepartmentAggregateManagementService
    {

        #region Ctor

        private IDepartmentAggregateRepository _departmentAggregateRepository;
        private ILogger _logger;

        public DepartmentAggregateManagementService(IDepartmentAggregateRepository departmentAggregateRepository, ILogger logger)
        {
            if (departmentAggregateRepository == (IDepartmentAggregateRepository)null)
                throw new ArgumentNullException("departmentAggregateRepository");
            if (logger == (ILogger)null)
                throw new ArgumentNullException("logger");
            this._departmentAggregateRepository = departmentAggregateRepository;
            this._logger = logger;
        }


        #endregion

        #region IDepartmentAggregateManagementService
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddDepartmentAsync(DepartmentAggregate department, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (department == (DepartmentAggregate)null)
                throw new ArgumentNullException("department");
            _departmentAggregateRepository.Add(department);
            return Task.Run(() =>
            {
                _departmentAggregateRepository.UnitOfWork.Commit();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentAggregate>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<IEnumerable<DepartmentAggregate>>(this._departmentAggregateRepository.GetAllElements());
        }
    }
        #endregion

}
