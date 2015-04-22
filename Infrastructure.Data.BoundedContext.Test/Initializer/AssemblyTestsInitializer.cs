using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infrastructure.Data.BoundedContext
{
    [TestClass]
    public class AssemblyTestsInitialize
    {
        [AssemblyInitialize]
        public static void RebuildUnitOfWork(TestContext context)
        {
        }
    }
}
