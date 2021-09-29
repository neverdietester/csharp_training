using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests

{
    public class NavigationHelper : HelperBase
    { 
        public string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) 
            : base (manager)
        {
        this.baseURL = baseURL;
        }

        public void GoToHomePage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.Navigate().GoToUrl("http://localhost/addressbook/");
        }
        public void GoToGroupPage()
        {
            if (driver.Url == baseURL + "/group.php"
                && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();
        }
        public void InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }
    }
}
