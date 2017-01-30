﻿using University.Selenium.Framework.Pages;
using OpenQA.Selenium.Support.PageObjects;

namespace University.Selenium.Framework.Browser
{
    public static class Page
    {
        public static ExamplePage ExamplePage
        {
            get
            {
                var examplePage = new ExamplePage();
                PageFactory.InitElements(Driver.Browser, examplePage);
                return examplePage;
            }
        }

        public static LoginPage LoginPage
        {
            get
            {
                var loginPage = new LoginPage();
                PageFactory.InitElements(Driver.Browser, loginPage);
                return loginPage;
            }
        }
    }
}
