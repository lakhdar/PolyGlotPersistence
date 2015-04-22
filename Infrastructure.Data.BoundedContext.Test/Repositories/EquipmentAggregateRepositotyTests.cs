using Domain.BoundedContext.StoreModule;
using Infrastructure.CrossCutting.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Infrastructure.Data.BoundedContext.tests.Membership
{
    [TestClass]
    public class EquipmentAggregateRepositotyTests
    {
        [TestMethod]
        public void EquipmentAggregateRepositoty_GetByIdValidIdTest()
        {
            Guid guid = new Guid("602D9F0C-40DA-4FCE-8C4D-F22748D5FB2D");
            IEquipmentAggregateRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IEquipmentAggregateRepository>();
            EquipmentAggregate equipment= repo.GetElementById(guid);
            Assert.IsNotNull(equipment);
            Assert.IsTrue(equipment.Id == guid);
        }


        [TestMethod]
        public void EquipmentAggregateRepositoty_GetAllTest()
        {
            IQueryable<EquipmentAggregate> all = IoCFactory.Instance.CurrentContainer.Resolve<IEquipmentAggregateRepository>().GetAllElements();
            Assert.IsNotNull(all);
            Assert.IsTrue(all.Any());
        }

        [TestMethod]
        public void EquipmentAggregateRepositoty_AddTest()
        {
            IEquipmentAggregateRepository repo = IoCFactory.Instance.CurrentContainer.Resolve<IEquipmentAggregateRepository>();
            EquipmentAggregate equipment = new EquipmentAggregate()
                                                          {
                                                              Id = Guid.NewGuid(),
                                                              DepartmentId = Guid.NewGuid(),
                                                              DepartmentName="test facility",
                                                              Model = "Type of equipment",
                                                              Name = "test equipment"
                                                          };
            repo.Add(equipment);
            repo.UnitOfWork.Commit();
            var all = repo.GetAllElements();
            Assert.IsNotNull(all);
            Assert.IsTrue(all.Count()>1);
        }
    }
}
