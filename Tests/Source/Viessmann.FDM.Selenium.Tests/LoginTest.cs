using Microsoft.VisualStudio.TestTools.UnitTesting;
using University.Selenium.Framework.Browser;

namespace University.Selenium.Tests
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void LoginShouldFail()
        {
            Driver.goToLoginPAge();
            Driver.implicitWait();

            Page.LoginPage.LogIn();

            bool loginFailed = Page.LoginPage.LoginFailed();
            Assert.IsTrue(loginFailed);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.exit();
        }
    }
}