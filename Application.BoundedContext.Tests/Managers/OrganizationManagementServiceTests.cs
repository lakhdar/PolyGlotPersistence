using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.BoundedConext.ERPModule;
using Infrastructure.CrossCutting.IoC;
using System.Collections.Generic;
using Domain.BoundedContext.ERPModule;
using System.Threading.Tasks;

namespace Application.BoundedConext.Tests.Managers
{
    [TestClass]
    public class OrganizationManagementServiceTests
    {
        [TestMethod]
        public async Task OrganizationManagementServiceTests_GetAllFacilitiesAsync_Tests()
        {
            IOrganizationManagementService service = IoCFactory.Instance.CurrentContainer.Resolve<IOrganizationManagementService>();
            IEnumerable<Organization> orgs =await  service.GetAllFacilitiesAsync();
            Assert.IsNotNull(orgs);
            Assert.IsTrue(orgs.Count() > 0);
        }
    }
}
