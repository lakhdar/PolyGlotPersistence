using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Collections.Generic;
using System.IO;
using ASP.NET.MVC5.Client.Controllers;
using System.Web.Mvc;
using Infrastructure.CrossCutting.IoC;
using System.Threading.Tasks;

namespace ASP.NET.MVC5.Client.Tests.Controllers
{
    [TestClass]
    public class EquipmentControllerTests
    {
        [TestInitialize]
        public void Setup()
        {
            HttpContext.Current = new HttpContext(new HttpRequest("", "http://tempuri.org", ""), new HttpResponse((TextWriter)new StringWriter()));
            HttpContext.Current.Items.Add("owin.Environment", new Dictionary<string, object>()
              {
                {
                  "owin.RequestBody",
                  null
                }
              });

            BootStrapper.Start();
        }



        [TestMethod]
        public async Task EquipmentController_Index_Test()
        {
            // Arrange
            EquipmentController controller = IoCFactory.Instance.CurrentContainer.Resolve<EquipmentController>(); ;

            // Act
            ViewResult result =await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
