using System;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V85.Debugger;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UITestCarsAsyncRESTv6
{
    [TestClass]
    public class UnitTestAnother
    {
        private static readonly string DriverDirectory = "C:\\webDrivers";
        private static IWebDriver _driver;

        // https://www.automatetheplanet.com/mstest-cheat-sheet/
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            //_driver = new ChromeDriver(DriverDirectory); // fast
            // if your Chrome browser was updated, you must update the driver as well ...
            //    https://chromedriver.chromium.org/downloads
            _driver = new FirefoxDriver(DriverDirectory);  // slow
            // _driver = new EdgeDriver(DriverDirectory); //  fast
            // Driver file must be renamed to MicrosoftWebDriver.exe OR msedgedriver.exe
            // depending on the version of Selenium?
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //string url = "http://localhost:3000/";
            string url = "file:///C:/andersb/javascript/carsVue3/another.html";
            _driver.Navigate().GoToUrl(url);

            string title = _driver.Title;
            Assert.AreEqual("Car Shop", title);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // decorator pattern?
            IWebElement carTable = wait.Until(d => d.FindElement(By.Id("cartable")));
            Assert.IsTrue(carTable.Text.Contains("Volvo"));

            // We already did the waiting in the previous lines, so now we can go back to using the ordinary driver
            ReadOnlyCollection<IWebElement> listElements = _driver.FindElements(By.TagName("tr"));
            Assert.AreEqual(5, listElements.Count);
            int originalCount = listElements.Count;

            Assert.IsTrue(listElements[1].Text.Contains("Volvo"));

            _driver.FindElement(By.Id("vendor")).SendKeys("NSU");
            _driver.FindElement(By.Id("model")).SendKeys("Prinz");
            _driver.FindElement(By.Id("price")).SendKeys("117");
            _driver.FindElement(By.Id("addButton")).Click();
  
            Thread.Sleep(1000); // TODO wait, on what ...?
            listElements = _driver.FindElements(By.TagName("tr"));
            Assert.AreEqual(originalCount + 1, listElements.Count);

            // XPath, an advanced option to use By.XPath(...)
            // https://www.guru99.com/handling-dynamic-selenium-webdriver.html
        }
    }
}