using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ASP.NET.MVC5.Client.Tests.CodedUITests
{
    [TestClass]
    public class FacilityControllerTests
    {

        private string homenUrl = "http://localhost/PolyglotPersistence/";
        private IWebDriver window;

        [TestInitialize]
        public void MyTestInitialize()
        {
            this.window = BrowserSelect.GetConfigBrowser();

        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            this.window.Close();
        }

        [TestMethod]
        public void faciltyListCreateTestDemo()
        {
            this.window.Navigate().GoToUrl(this.homenUrl);
            WebDriverWait webDriverWait = new WebDriverWait(this.window, TimeSpan.FromMilliseconds(1000.0));
            IWebElement loginLink = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("Facility")));
            loginLink.Click();
            IWebElement Link = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("facilityCreate")));
            Link.Click();
            

        }
    }
}
