using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ASP.NET.MVC5.Client.Tests.CodedUITests
{
    [TestClass]
    public class AccountControllerTests
    {
       // 

        private string homenUrl = "http://localhost/";
        private string logonlinkId = "logon";
        private string loginUrl = "http://localhost//account/login";

        private string loginText = "Andrew@aaa.com";
        private string loginId = "username2";
        private string passwordText = "Azerty6@";
        private string passwordId = "password";
        private string submitBtnId = "submitbtn";

        private string loginstatus = "loginstatus";
        private string logoff = "logoff";
        private string invalidemailmsg = "invalid_email_msg";
        private string requiredemailmsg = "required_email_msg";
        private string pwdemailmsg = "pwd_email_msg";
        
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
        [TestCategory("Account.CodedUiTests")]
        public void LoginValidLogon_then_Loggoff()
        {
            this.window.Navigate().GoToUrl(this.homenUrl);
            WebDriverWait webDriverWait = new WebDriverWait(this.window, TimeSpan.FromMilliseconds(1000.0));
            IWebElement loginLink = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.logonlinkId)));
            Assert.IsTrue(this.window.Title.Contains("LMS"));
            loginLink.Click();

            webDriverWait.Timeout = TimeSpan.FromMilliseconds(1000.0);
            IWebElement email = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.loginId)));
            email.SendKeys(this.loginText);

            this.window.FindElement(By.Id(this.passwordId)).SendKeys(this.passwordText);
            IWebElement sbtnSubmit = this.window.FindElement(By.Id(this.submitBtnId));
            var cssClass = sbtnSubmit.GetAttribute("class").Split(new[]{' '});
            Assert.IsFalse(cssClass.Any(c=>c.Equals("disabled",StringComparison.InvariantCultureIgnoreCase)));
            sbtnSubmit.Click();
            
            this.Logoff();
            webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.logonlinkId)));
            Assert.IsTrue(this.window.Title.Contains("LMS"));
        }


        [TestMethod]
        [TestCategory("Account.CodedUiTests")]
        public void Login_Invalid_Attempt_then_login_valid() 
        {
            string invalidmail="invalidlogin@login.com";
            this.window.Navigate().GoToUrl(this.homenUrl);
            //click login link
            WebDriverWait webDriverWait = new WebDriverWait(this.window, TimeSpan.FromMilliseconds(1000.0));
            IWebElement loginLink = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.logonlinkId)));
            Assert.IsTrue(this.window.Title.Contains("LMS"));
            loginLink.Click();

             //fill in email and pwd 
            IWebElement email = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.loginId)));
            email.SendKeys(invalidmail);
            this.window.FindElement(By.Id(this.passwordId)).SendKeys(this.passwordText);
            //be sure login button enabled and click on 
            IWebElement sbtnSubmit = this.window.FindElement(By.Id(this.submitBtnId));
            var cssClass = sbtnSubmit.GetAttribute("class").Split(new[] { ' ' });
            Assert.IsFalse(cssClass.Any(c => c.Equals("disabled", StringComparison.InvariantCultureIgnoreCase)));
            sbtnSubmit.Click();

            //wait till   validationSummary is visible
            webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("validationSummary")));
            Assert.IsTrue(this.window.Title.Contains("login"));

            //be sur login button disabled
            sbtnSubmit = this.window.FindElement(By.Id(this.submitBtnId));
            cssClass = sbtnSubmit.GetAttribute("class").Split(new[] { ' ' });
            Assert.IsTrue(cssClass.Any(c => c.Equals("disabled", StringComparison.InvariantCultureIgnoreCase)));


            //assert  email is filled by the invalid value
            email = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.loginId)));
            var value = email.GetAttribute("value");
            Assert.IsTrue(value.Equals(invalidmail,StringComparison.InvariantCultureIgnoreCase));

            //send valid values and login 
            email.Clear();
            email.SendKeys(this.loginText);

            this.window.FindElement(By.Id(this.passwordId)).SendKeys(this.passwordText);

            //be sur login button enabled
            cssClass = sbtnSubmit.GetAttribute("class").Split(new[] { ' ' });
            Assert.IsFalse(cssClass.Any(c => c.Equals("disabled", StringComparison.InvariantCultureIgnoreCase)));
            
            sbtnSubmit.Click();

            webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.loginstatus)));
            Assert.IsTrue(this.window.Title.Contains("LMS"));

        }

        [TestMethod]
        [TestCategory("Account.CodedUiTests")]
        public void Login_invalid_And_Required_Fields()
        {
            this.window.Navigate().GoToUrl(this.homenUrl);
            WebDriverWait webDriverWait = new WebDriverWait(this.window, TimeSpan.FromMilliseconds(1000.0));
            IWebElement loginLink = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.logonlinkId)));
            Assert.IsTrue(this.window.Title.Contains("LMS"));
            loginLink.Click();

            webDriverWait.Timeout = TimeSpan.FromMilliseconds(1000.0);
            IWebElement email = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.loginId)));
            email.SendKeys("invalidEmail");

            IWebElement invalidmsg = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.invalidemailmsg)));
            email.Clear();
            webDriverWait.Timeout = TimeSpan.FromMilliseconds(1000.0);
            IWebElement requiredmsg = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.requiredemailmsg)));

            IWebElement pwd = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.passwordId)));
            pwd.SendKeys("invalidpwd");
            pwd.Clear();
            IWebElement pwdrequirmsg = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.pwdemailmsg)));

            IWebElement emailParent = email.FindElement(By.XPath("../.."));

            var cssClass = emailParent.GetAttribute("class").Split(new[] { ' ' });
            Assert.IsTrue(cssClass.Contains("has-error"));

            IWebElement pwdParent = pwd.FindElement(By.XPath("../.."));

            cssClass = pwdParent.GetAttribute("class").Split(new[]{' '});
            Assert.IsTrue(cssClass.Contains("has-error"));

            IWebElement sbtnSubmit = this.window.FindElement(By.Id(this.submitBtnId));
            cssClass = sbtnSubmit.GetAttribute("class").Split(new[] { ' ' });
            Assert.IsTrue(cssClass.Contains("disabled"));
        }




        private bool Shoouldlogoff()
        {
            var loginStatus = this.window.FindElement(By.Id(this.loginstatus));
           return loginStatus == null;
        }

        private void Logoff()
        {
            IWebElement loginStatus = this.window.FindElement(By.Id(this.loginstatus));
            loginStatus.Click();
            WebDriverWait webDriverWait = new WebDriverWait(this.window, TimeSpan.FromMilliseconds(100.0));
            IWebElement logoff = webDriverWait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(this.logoff)));
            logoff.Click();

            webDriverWait.Timeout = TimeSpan.FromMilliseconds(1000.0);
           
        }
    }
}
