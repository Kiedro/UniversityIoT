﻿using System;

namespace University.Selenium.Framework.Utilities
{
    public static class Settings
    {
        public static String[] arguments = Environment.GetCommandLineArgs();
        public static string driver { get {return arguments[0] ;}   }

        public static string baseUrl { get { return "http://google.com"; } }

        public static string Login { get { return "exampleLogin"; } }
        public static string Password { get { return "examplePassword"; } }
        
        public static  string loginPageLink { get { return "http://localhost:3000/#!/login"; } }

        public static TimeSpan implicitWaitTimeout { get { return TimeSpan.FromSeconds(30); } }

    }
}
