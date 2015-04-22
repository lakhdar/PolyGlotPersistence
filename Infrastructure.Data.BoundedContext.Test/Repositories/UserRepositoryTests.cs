// Decompiled with JetBrains decompiler
// Type: Infrastructure.Data.BoundedContext.tests.Membership.UserRepositoryTests
// Assembly: Infrastructure.Data.BoundedContext.tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC499FDF-797A-4D00-802C-5AFEB6A1772B
// Assembly location: C:\Pedago\Solution1\Infrastructure.Data.BoundedContext.tests\bin\Debug\Infrastructure.Data.BoundedContext.tests.dll

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.BoundedContext.ERPModule;
using Domain.BoundedContext.MembershipModule;
using Infrastructure.CrossCutting.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.BoundedContext.tests.Membership
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void UserRepository_GetByIdValidIdTest()
        {
            Guid id = new Guid("448e47ad-0fc3-c5c2-584d-08d1b874916a");
            User byId = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>().GetElementById(id);
            Assert.IsNotNull((object)byId);
            Assert.IsTrue(byId.Id == id);
        }

        [TestMethod]
        public async Task UserRepository_GetElementByIdAsyncIdTest()
        {
            Guid id = new Guid("448e47ad-0fc3-c5c2-584d-08d1b874916a");
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            User user = await repo.GetElementByIdAsync(id, new CancellationToken());
            Assert.IsNotNull((object)user);
            Assert.IsTrue(user.Id == id);
        }

        [TestMethod]
        public void UserRepository_GetAllTest()
        {
            IQueryable<User> all = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>().GetAllElements();
            Assert.IsNotNull((object)all);
            Assert.IsTrue(Queryable.Count<User>(all) > 0);
        }

        [TestMethod]
        public void UserRepository_AddTest_ValidUser_NewAddress()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            IoCFactory.Instance.CurrentContainer.Resolve<IAddressRepository>();
            User user1 = new User()
            {
                Id = new Guid("514c5873-bee8-cc78-3de8-08d1b9e806d0"),
                UserName = "test2.test@test2.test",
                FirstName = "test2",
                LastName = "test2",
                PasswordHash = "test2",
                BirthDate = DateTime.UtcNow,
                LastActivityDate = DateTime.UtcNow,
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressLine1 = "test2",
                    ZipCode = "12352",
                    City = "Test2"
                }
            };
           
            userRepository.Add(user1);
            userRepository.UnitOfWork.Commit();
            User byId = userRepository.GetElementById(user1.Id);
            Assert.IsNotNull((object)byId);
            Assert.IsTrue(byId.Id == user1.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UserRepository_AddTest_InValidUser()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            IoCFactory.Instance.CurrentContainer.Resolve<IAddressRepository>();
            User user1 = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                BirthDate = DateTime.UtcNow,
                LastActivityDate = DateTime.UtcNow,
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressLine1 = "test",
                    ZipCode = "1235",
                    City = "Test"
                }
            };

            userRepository.Add(user1);
            userRepository.UnitOfWork.CommitAndRefreshChanges();
            userRepository.GetElementById(user1.Id);
        }

        [TestMethod]
        public void UserRepository_RemoveTest()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            Guid id = new Guid("f3f3a7be-1a99-c4e1-c830-08d1c03f3913");
            User byId = userRepository.GetElementById(id);
            Assert.IsNotNull((object)byId);
            userRepository.Remove(byId);
            userRepository.UnitOfWork.Commit();
            Assert.IsNull((object)userRepository.GetElementById(byId.Id));
        }

        [TestMethod]
        public void UserRepository_ModifyTest()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            Guid id = new Guid("448e47ad-0fc3-c5c2-584d-08d1b874916a");
            User byId1 = userRepository.GetElementById(id);
            Assert.IsNotNull((object)byId1);
            byId1.FirstName = "Modified";
            userRepository.SetModified(byId1);
            userRepository.UnitOfWork.Commit();
            User byId2 = userRepository.GetElementById(byId1.Id);
            Assert.IsNotNull((object)byId2);
            Assert.IsTrue(byId2.FirstName == "Modified");
        }

        [TestMethod]
        public void UserRepository_GetFilteredElementsTest()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            Guid id = new Guid("448e47ad-0fc3-c5c2-584d-08d1b874916a");
            IEnumerable<User> filteredElements = userRepository.GetFilteredElements((x => x.Id == id));
            Assert.IsNotNull((object)filteredElements);
            Assert.IsTrue(Enumerable.Count<User>(filteredElements) == 1);
            Assert.IsTrue(Enumerable.First<User>(filteredElements).Id == id);
        }

        [TestMethod]
        public void UserRepository_GetFilteredElementsTest_nonexisting()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaaxxxx@aaa.com";
            IEnumerable<User> filteredElements = userRepository.GetFilteredElements((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()));
            Assert.IsNotNull((object)filteredElements);
            Assert.IsTrue(Enumerable.Count<User>(filteredElements) == 0);
        }

        [TestMethod]
        public void UserRepository_GetFilteredElementsTest_existing()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaa@aaa.com";
            IEnumerable<User> filteredElements = userRepository.GetFilteredElements((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()));
            Assert.IsNotNull((object)filteredElements);
            Assert.IsTrue(Enumerable.Count<User>(filteredElements) == 1);
            Assert.IsTrue(Enumerable.First<User>(filteredElements).UserName == uName);
        }

        [TestMethod]
        public void UserRepository_GetFilteredElementsTest_Includes()
        {
            IUserRepository userRepository = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaa@aaa.com";
            IEnumerable<User> filteredElements = userRepository.GetFilteredElements((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()), (Expression<Func<User, object>>)(x => x.Roles));
            Assert.IsNotNull((object)filteredElements);
            Assert.IsTrue(Enumerable.Count<User>(filteredElements) == 1);
            Assert.IsTrue(Enumerable.First<User>(filteredElements).UserName == uName);
            Assert.IsNotNull((object)Enumerable.First<User>(filteredElements).Roles);
            Assert.IsTrue(Enumerable.Count<Role>((IEnumerable<Role>)Enumerable.First<User>(filteredElements).Roles) > 0);
        }

        [TestMethod]
        public async Task UserRepository_GetFilteredElementsAsyncTest_nonexisting()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaaxxxx@aaa.com";
            IEnumerable<User> posistions = await repo.GetFilteredElementsAsync((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()), new CancellationToken());
            Assert.IsNotNull((object)posistions);
            Assert.IsTrue(Enumerable.Count<User>(posistions) == 0);
        }

        [TestMethod]
        public async Task UserRepository_GetFilteredElementsAsyncTest_existing()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaa@aaa.com";
            IEnumerable<User> posistions = await repo.GetFilteredElementsAsync((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()), new CancellationToken());
            Assert.IsNotNull((object)posistions);
            Assert.IsTrue(Enumerable.Count<User>(posistions) == 1);
            Assert.IsTrue(Enumerable.First<User>(posistions).UserName == uName);
        }

        [TestMethod]
        public async Task UserRepository_GetFilteredElementsAsyncTest_Includes()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaa@aaa.com";
            IEnumerable<User> posistions = await repo.GetFilteredElementsAsync((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()), new CancellationToken(), (Expression<Func<User, object>>)(x => x.Roles));
            Assert.IsNotNull((object)posistions);
            Assert.IsTrue(Enumerable.Count<User>(posistions) == 1);
            Assert.IsTrue(Enumerable.First<User>(posistions).UserName == uName);
            Assert.IsNotNull((object)Enumerable.First<User>(posistions).Roles);
            Assert.IsTrue(Enumerable.Count<Role>((IEnumerable<Role>)Enumerable.First<User>(posistions).Roles) > 0);
        }

        [TestMethod]
        public void UserRepository_GetPagedTest()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            IEnumerable<User> paged = repo.GetPagedElements<string>(0, 3, x => x.FirstName, true);
            Assert.IsNotNull((object)paged);
            Assert.IsTrue(paged.Count() <= 3);
        }

        [TestMethod]
        public async Task UserRepository_GetFirstOrDefaultAsyncTest_ExistingUser()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaa@aaa.com";
            User user = await repo.GetFirstOrDefaultAsync((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()), new CancellationToken(), (Expression<Func<User, object>>)(x => x.Roles));
            Assert.IsNotNull((object)user);
            Assert.IsNotNull((object)user.Roles);
            Assert.IsTrue(user.UserName.Equals(uName, StringComparison.InvariantCultureIgnoreCase));
        }

       
        [TestMethod]
        public async Task UserRepository_GetFirstOrDefaultAsyncTest_NullFilter()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            User user = await repo.GetFirstOrDefaultAsync(null, new CancellationToken(), (x => x.Roles));
            Assert.IsNotNull((object)user);
        }

        [TestMethod]
        public async Task UserRepository_GetFirstOrDefaultAsyncTest_InvalidUser()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string uName = "aaaaxxxx@aaa.com";
            User user = await repo.GetFirstOrDefaultAsync((u => u.UserName != (object)null && u.UserName.ToLower() == uName.ToLower()), new CancellationToken());
            Assert.IsNull((object)user);
        }

        [TestMethod]
        public async Task UserRepository_GetFirstOrDefaultAsyncTest_NotSingleUser()
        {
            IUserRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IUserRepository>();
            string passwordHash = "ADlfX1+zP7nNsHgdei/CbMQHM6g0fIYu2YAlrqIA6ULALmWBIDHebhWYLo8f//P4+g==";
            User user = await repo.GetFirstOrDefaultAsync((u => u.PasswordHash != (object)null && u.PasswordHash.ToLower() == passwordHash.ToLower()), new CancellationToken());
            Assert.IsNotNull((object)user);
            Assert.IsTrue(user.PasswordHash.Equals(passwordHash, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
