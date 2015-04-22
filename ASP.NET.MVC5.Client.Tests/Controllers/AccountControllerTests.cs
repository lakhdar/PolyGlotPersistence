using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASP.NET.MVC5.Client.Controllers;
using Infrastructure.CrossCutting.IoC;
using System.Threading.Tasks;
using ASP.NET.MVC5.Client.Models;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASP.NET.MVC5.Client.Resources;

namespace ASP.NET.MVC5.Client.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
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
        public async Task AccountController_RegisterTest()
        {
            AccountController controller = IoCFactory.Instance.CurrentContainer.Resolve<AccountController>();
            RegisterViewModel model = new RegisterViewModel()
            {
                Email = "lakhdar.aliane@live.fr",
                Password = "Azerty6@",
                ConfirmPassword = "Azerty6@"
            };

            RedirectToRouteResult result = await controller.Register(model) as RedirectToRouteResult;
            Assert.IsNotNull((object)result);
            Assert.AreEqual((object)"Index", result.RouteValues["action"]);
            Assert.AreEqual((object)"Home", result.RouteValues["controller"]);


        }

        [TestMethod]
        public async Task AccountController_RegisterTest_EmptyPasswd()
        {
            //Arrange
            AccountController controller = IoCFactory.Instance.CurrentContainer.Resolve<AccountController>();
            RegisterViewModel model = new RegisterViewModel()
            {
                Email = "test.test@live.fr",
                Password = "",
                ConfirmPassword = ""
            };

            //Act
            ViewResult result = await controller.Register(model) as ViewResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(controller.ModelState.Count == 1);
            string errors = string.Join("; ", controller.ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            Assert.IsFalse(string.IsNullOrEmpty(errors));

        }

        [TestMethod]
        public async Task AccountController_RegisterTest_InvalidEmailFormat()
        {
            //Arrange
            AccountController controller = IoCFactory.Instance.CurrentContainer.Resolve<AccountController>();
            RegisterViewModel model = new RegisterViewModel()
            {
                Email = "test.test",
                Password = "xxxxxx",
                ConfirmPassword = "xxxxxx"
            };

            //Act
            ViewResult result = await controller.Register(model) as ViewResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(controller.ModelState.Count == 1);
            string errors = string.Join("; ", controller.ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            Assert.IsFalse(string.IsNullOrEmpty(errors));
        }

        [TestMethod]
        public async Task AccountController_LoginValidTest()
        {
            //Arrange
            AccountController controller = IoCFactory.Instance.CurrentContainer.Resolve<AccountController>();
            LoginViewModel model = new LoginViewModel()
            {
                Email = "Andrew@aaa.com",
                Password = "Azerty6@",
                RememberMe=true
            };
            controller.Url =  new AccountControllerTests.TestUriHelper();
            //Act
            RedirectToRouteResult result = await controller.Login(model,"/home/index") as RedirectToRouteResult;
            //Assert
            Assert.IsNotNull((object)result);
            Assert.AreEqual((object)"Index", result.RouteValues["action"]);
            Assert.AreEqual((object)"Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public async Task AccountController_LoginInValidPwdTest()
        {
            //Arrange
            AccountController controller = IoCFactory.Instance.CurrentContainer.Resolve<AccountController>();
            LoginViewModel model = new LoginViewModel()
            {
                Email = "test@test.com",
                Password = "A123Azerty",
                RememberMe = true
            };
            controller.Url = new AccountControllerTests.TestUriHelper();
            //Act
            ViewResult result = await controller.Login(model, "/home/index") as ViewResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(controller.ModelState.Count == 1);
            string errors = string.Join("; ", controller.ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            Assert.AreEqual(errors, Resources.Messages.InvalidLoginAttempt,true);
        }

        internal class TestUriHelper : UrlHelper
        {
            public override bool IsLocalUrl(string url)
            {
                return false;
            }
        }

    }
   
}
