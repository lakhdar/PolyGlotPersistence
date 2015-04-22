using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.BoundedConext.StoreModule;
using System.Collections.Generic;
using Domain.BoundedContext.StoreModule;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.IoC;

namespace Application.BoundedConext.Tests.Managers
{
    [TestClass]
    public class EquipmentAggregateManagementServiceTests
    {
        [TestMethod]
        public async Task EquipmentAggregateManagementServiceTests_GetAllTests()
        {
            IEquipmentAggregateManagementService service = IoCFactory.Instance.CurrentContainer.Resolve<IEquipmentAggregateManagementService>();
            IEnumerable<EquipmentAggregate> orgs = await service.GetAllEquipmentAsync();
            Assert.IsNotNull(orgs);
            Assert.IsTrue(orgs.Count() > 0);
        }
    }
}
