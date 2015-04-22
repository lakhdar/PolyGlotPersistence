using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.BoundedContext.ERPModule;
using Infrastructure.CrossCutting.IoC;
using System.Linq;

namespace Infrastructure.Data.BoundedContext.Tests.Repositories
{
    [TestClass]
    public class OrganizationRepositoryTests
    {
        [TestMethod]
        public void OrganizationRepository_GetByIdValidIdTest()
        {
            Guid id = new Guid("32f6c66a-d762-c890-3b5a-08d1ffaa17c7");
            IOrganizationRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IOrganizationRepository>();
           Organization org=     repo.GetElementById(id);
            Assert.IsNotNull((object)org);
            Assert.IsTrue(org.Id == id);
        }

        [TestMethod]
        public void OrganizationRepository_GetAllTest()
        {
            IOrganizationRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IOrganizationRepository>();
            IQueryable<Organization> all = repo.GetAllElements();
            Assert.IsNotNull(all);
            Assert.IsTrue( all.Count() > 0);
        }

        [TestMethod]
        public void UserRepository_AddTest_ValidUser_NewAddress()
        {
            IOrganizationRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IOrganizationRepository>();
            Organization org = new Organization()
            {
                Id = new Guid("514c5873-bee8-cc78-3de8-08d1b9e806d0"),
                Name = "test2.test@test2.test",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressLine1 = "test2",
                    ZipCode = "12352",
                    City = "Test2"
                }
            };

            repo.Add(org);
            repo.UnitOfWork.Commit();
            Organization byId = repo.GetElementById(org.Id);
            Assert.IsNotNull(byId);
            Assert.IsTrue(byId.Id == org.Id);
        }
    }
}
