using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using University.Selenium.Framework.Browser;
using University.Selenium.Framework.Utilities;
using OpenQA.Selenium.Support.Events;


namespace University.Selenium.Tests
{
    [TestClass]
    public class ViessmannTests
    {
        IWebDriver driver;
        string url = "http://localhost:3000/#!/login";

        [TestInitialize]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
        }

        [TestMethod]
        public void ChechIfLoginLabelExists()
        {
            //act
            var login = driver.FindElement(By.Id("login"));

            //assert
            Assert.IsNotNull(login);
        }

        //[TestMethod]
        //public void ChechIfLoginLabelExists()
        //{
        //    //act
        //    var login = driver.FindElement(By.Id("login"));

        //    //assert
        //    Assert.IsNotNull(login);
        //}

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
