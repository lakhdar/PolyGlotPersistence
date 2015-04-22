using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.BoundedContext.ERPModule;
using Infrastructure.CrossCutting.IoC;
using System.Linq;

namespace Infrastructure.Data.BoundedContext.Tests.Repositories
{
    [TestClass]
    public class DepartmentAggregateRepositoryTests
    {

        [TestMethod]
        public void DepartmentAggregateRepository_GetByIdValidIdTest()
        {
            Guid guid = new Guid("62588897-8e83-cf15-3239-08d20141829a");
            IDepartmentAggregateRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IDepartmentAggregateRepository>();
            DepartmentAggregate dep = repo.GetElementById(guid);
            Assert.IsNotNull(dep);
            Assert.IsTrue(dep.Id == guid);
        }
        [TestMethod]
        public void DepartmentAggregateRepositoryTests_GetAllTest()
        {
            IDepartmentAggregateRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IDepartmentAggregateRepository>();
            IQueryable<DepartmentAggregate> all = repo.GetAllElements();
            Assert.IsNotNull(all);
            Assert.IsTrue(all.Any());
        }
    }
}
