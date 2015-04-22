// Decompiled with JetBrains decompiler
// Type: Application.BoundedContext.Tests.UserManagementServicesTests
// Assembly: Application.BoundedContext.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B647539-2F8A-4AE8-AC6E-7F81A390B894
// Assembly location: C:\Pedago\Solution1\Application.BoundedContext.Tests\bin\Debug\Application.BoundedContext.Tests.dll

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.BoundedContext.ERPModule;
using Domain.BoundedContext.MembershipModule;
using Infrastructure.CrossCutting.IoC;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.BoundedContext.MembershipModule;

namespace Application.BoundedContext.Tests
{
    [TestClass]
    public class UserManagementServicesTests
    {
        [TestMethod]
        public void UserManagementServices_GetByIdTest_validId()
        {
            Guid id = new Guid("448e47ad-0fc3-c5c2-584d-08d1b874916a");
            User byId = IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>().GetById(id);
            Assert.IsNotNull((object)byId);
            Assert.IsTrue(byId.Id == id);
        }

        [TestMethod]
        public void UserManagementServices_GetByIdTest_Emptyid()
        {
            Assert.IsNull((object)IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>().GetById(Guid.Empty));
        }

        [TestMethod]
        public async Task UserManagementServices_GetByIdAsyncTest_validId()
        {
            Guid id = new Guid("448e47ad-0fc3-c5c2-584d-08d1b874916a");
            IUserManagementServices manager = IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>();
            User usr = await manager.GetByIdAsync(id, new CancellationToken());
            Assert.IsNotNull((object)usr);
            Assert.IsTrue(usr.Id == id);
        }

        public async Task UserManagementServices_GetByIdAsyncTest_Emptyid()
        {
            IUserManagementServices manager = IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>();
            User usr = await manager.GetByIdAsync(Guid.Empty, new CancellationToken());
            Assert.IsNull((object)usr);
        }

        [TestMethod]
        public async Task UserManagementServices_AddAsyncTest()
        {
            IUserManagementServices manager = IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>();
            User user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "test.testdddd@test.test",
                FirstName = "test",
                LastName = "test",
                PasswordHash="test",
                BirthDate = DateTime.UtcNow,
                Address=new Address(){
                    Id = Guid.NewGuid(),
                    AddressLine1 = "test",
                    ZipCode = "1235",
                    City = "Test",
                }
            };

            await manager.AddAsync(user);
            User usr = manager.GetById(user.Id);
            Assert.IsNotNull((object)usr);
            Assert.IsTrue(usr.Id == user.Id);
            Assert.IsTrue(usr.FirstName == user.FirstName);
        }

        [TestMethod]
        public async Task UserManagementServices_GetByEmailAsyncTest_InvalidEmail()
        {
            IUserManagementServices manager = IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>();
            string email = "lakhdar.aliane@live.fr";
            User usr = await manager.GetByEmailAsync(email, new CancellationToken());
            Assert.IsNull((object)usr);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UserManagementServices_GetByEmailAsyncTest_EmptyEmail()
        {
            IUserManagementServices manager = IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>();
            string email = "";
            User usr = await manager.GetByEmailAsync(email, new CancellationToken());
            Assert.IsNull((object)usr);
        }

        [TestMethod]
        public async Task UserManagementServices_GetByEmailAsyncTest_ValidEmail()
        {
            IUserManagementServices manager = IoCFactory.Instance.CurrentContainer.Resolve<IUserManagementServices>();
            string email = "Robert@aaa.com";
            User usr = await manager.GetByEmailAsync(email, new CancellationToken());
            Assert.IsNotNull((object)usr);
        }
    }
}
