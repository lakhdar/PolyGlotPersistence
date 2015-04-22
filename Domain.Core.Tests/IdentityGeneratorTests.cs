using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Core.Test
{
    [TestClass]
    public class IdentityGeneratorTests
    {
        [TestMethod]
        public void SequentialGuidTest()
        {
            //Arrange,Act
            var id = IdentityGenerator.SequentialGuid();

            //Assert
            Assert.IsNotNull(id);
            Assert.IsTrue(id!=Guid.Empty);

        }
    }
}
