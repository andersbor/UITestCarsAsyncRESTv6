using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UITestCarsAsyncRESTv6
{
    [TestClass]
    public class UnitTest1
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
            //_driver = new FirefoxDriver(DriverDirectory);  // slow
            _driver = new EdgeDriver(DriverDirectory); //  fast
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
            //string url = "http://127.0.0.1:5502/index.htm";
            string url = "file:///C:/andersb/javascript/carsVue3/index.htm";
            _driver.Navigate().GoToUrl(url);

            string title = _driver.Title;
            Assert.AreEqual("Car Shop", title);

            IWebElement buttonElement = _driver.FindElement(By.Id("getAllButton"));
            buttonElement.Click();

            //IWebElement carList = _driver.FindElement(By.Id("carlist")); // No such element

            //pause(60); // NOT good, will always wait full 60 sec

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // decorator pattern?
            IWebElement carList = wait.Until(d => d.FindElement(By.Id("carlist")));
            Assert.IsTrue(carList.Text.Contains("Volvo"));

            // We already did the waiting in the previous lines, so now we can go back to using the ordinary driver
            ReadOnlyCollection<IWebElement> listElements = _driver.FindElements(By.TagName("li"));
            Assert.AreEqual(3, listElements.Count);

            Assert.IsTrue(listElements[0].Text.Contains("Volvo"));

            // XPath, an advanced option to use By.XPath(...)
            // https://www.guru99.com/handling-dynamic-selenium-webdriver.html
        }
    }
}