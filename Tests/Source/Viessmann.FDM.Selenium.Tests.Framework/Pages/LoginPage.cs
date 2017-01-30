using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using University.Selenium.Framework.Browser;
using University.Selenium.Framework.Utilities;

namespace University.Selenium.Framework.Pages
{
    public class LoginPage
    {
        [FindsBy(How = How.Id, Using = "login")]
        private IWebElement loginInput;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordInput;

        [FindsBy(How = How.ClassName, Using = "button")]
        private IWebElement loginButton;

        public void LogIn()
        {
            loginInput.SendKeys(Settings.Login);
            passwordInput.SendKeys(Settings.Password);

            loginButton.Click();
        }

        public bool LoginFailed()
        {
            WebDriverWait wait = new WebDriverWait(Driver.webDriver, new TimeSpan(0,0,5));
            try
            {
                wait.Until(ExpectedConditions.AlertIsPresent());

                var alert = Driver.webDriver.SwitchTo().Alert();

                return alert.Text == "dupa";
            }
            catch
            {
                return false;
            }
        }
    }
}
