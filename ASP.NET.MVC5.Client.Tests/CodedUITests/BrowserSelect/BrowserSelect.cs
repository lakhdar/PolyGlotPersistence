using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace ASP.NET.MVC5.Client.Tests.CodedUITests
{
    public static class BrowserSelect
    {
        public static IWebDriver GetConfigBrowser(string browserName)
        {
            string AppRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string strDriverPath = null;
            IWebDriver webDriver = null;
            if (browserName.Equals("Firefox", StringComparison.InvariantCultureIgnoreCase))
            {
                webDriver = new FirefoxDriver();
            }
            else if (browserName.Equals("chrome", StringComparison.InvariantCultureIgnoreCase))
            {
                strDriverPath = Path.Combine(AppRoot, @"CodedUITests\Drivers\chromedriver_win32");
                webDriver = new ChromeDriver(strDriverPath);

            }
            else if (browserName.Equals("IE", StringComparison.InvariantCultureIgnoreCase))
            {
                InternetExplorerOptions options = new InternetExplorerOptions()
                {
                    EnableNativeEvents = true,
                    IgnoreZoomLevel = true
                };
                strDriverPath = Path.Combine(AppRoot, @"CodedUITests\Drivers\IEDriverServer_Win32_2.44.0");
                webDriver = new InternetExplorerDriver(strDriverPath, options, TimeSpan.FromMinutes(3.0));
            }
            if (webDriver == null)
                throw new Exception("must configure browsername in aap.config");
            else
                return webDriver;
        }

        public static IWebDriver GetConfigBrowser()
        {
            return BrowserSelect.GetConfigBrowser(ConfigurationManager.AppSettings["browserName"]);
        }
    }
}
