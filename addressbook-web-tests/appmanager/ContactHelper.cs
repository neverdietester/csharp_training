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
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public ContactHelper CreateContact(ContactData contact)
        {
            manager.Navigator.InitNewContactCreation();

            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactPage();
            return this;
        }

        public ContactHelper Modify(ContactData newData)
        {
            manager.Navigator.GoToHomePage();

            SelectContact();
            EditContact();
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactPage();
            return this;
        }
                
        public ContactHelper Remove()
        {
            manager.Navigator.GoToHomePage();

            SelectContact();
            RemoveContact();
            ConfirmationRemoval();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper ConfirmationRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            driver.FindElement(By.CssSelector("div.msgbox"));
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact()
        {
            SelectItem();
            return this;
        }

        public void SelectItem()
        {
            driver.FindElement(By.Name("selected[]")).Click();
        }

        public bool IsContactExists()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            TypeSelected(By.Name("bday"), contact.Bday);
            TypeSelected(By.Name("bmonth"), contact.Bmonth);
            Type(By.Name("byear"), contact.Byear);
            TypeSelected(By.Name("aday"), contact.Aday);
            TypeSelected(By.Name("amonth"), contact.Amonth);
            Type(By.Name("ayear"), contact.Ayear);
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);

            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper ReturnToContactPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper EditContact()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }


        private List<ContactData> contactCache = null;
        
        
        public List<ContactData> GetContactList()
            {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));

                foreach (IWebElement element in elements)
                {

                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    string firstname = cells[2].Text;
                    string lastname = cells[1].Text;
                    contactCache.Add(new ContactData(cells[2].Text, cells[1].Text));
                }
            }
                return new List<ContactData>(contactCache);
            }
        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }
    }
}
