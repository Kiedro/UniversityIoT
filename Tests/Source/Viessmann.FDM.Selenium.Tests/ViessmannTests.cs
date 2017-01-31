using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using University.Selenium.Framework.Browser;
using University.Selenium.Framework.Utilities;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;


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

        [TestMethod]
        public void TryLogin()
        {
            //arrange
            var loginInput = driver.FindElement(By.Id("login"));
            var passInput = driver.FindElement(By.Id("password"));
            var loginButton = driver.FindElement(By.Id("login-button"));
            
            //act
            loginInput.SendKeys("Joan");
            passInput.SendKeys("pass");
            loginButton.Submit();

            //assert
            Assert.IsTrue(IsDialogPresent());
        }

        bool IsDialogPresent()
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0,0,10));
            try
            {
                wait.Until(ExpectedConditions.AlertIsPresent());
                IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                return (alert != null);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
