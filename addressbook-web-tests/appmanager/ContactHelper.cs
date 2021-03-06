using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

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

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();

            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        internal void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();

            SelectGroupInFilter(group.Name);
            SelectContact(contact.Id);
            RemoveContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void RemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroupInFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactHelper Modify(int p, ContactData newData)
        {
            manager.Navigator.GoToHomePage();

            SelectContact(p);
            InitContactModification(0);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactPage();
            return this;
        }

        public ContactHelper Modify(ContactData contact, ContactData newData)
        {
            manager.Navigator.GoToHomePage();

            SelectContact(contact.Id);
            InitContactModification(0);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactPage();
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToHomePage();

            SelectContact(contact.Id);
            RemoveContact();
            ConfirmationRemoval();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public void SelectContact(String id)
        {
            driver.FindElement(By.XPath("//input[@id='" + id + "']")).Click();
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//input[@id='" + (index + 1) + "']")).Click();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();

            SelectContact(v);
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

        public bool IsContactExists()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.HomePage);
            TypeSelected(By.Name("bday"), contact.BDay);
            TypeSelected(By.Name("bmonth"), contact.BMonth);
            Type(By.Name("byear"), contact.BYear);
            TypeSelected(By.Name("aday"), contact.ADay);
            TypeSelected(By.Name("amonth"), contact.AMonth);
            Type(By.Name("ayear"), contact.AYear);
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
                    string firstName = cells[2].Text;
                    string lastName = cells[1].Text;
                    contactCache.Add(new ContactData(cells[2].Text, cells[1].Text));
                }
            }
                return new List<ContactData>(contactCache);
            }
        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;
            

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };

        }

        public ContactData GetContactInformationFromIditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public ContactData GetContactDetailsFromForm(int index)
        {
            manager.Navigator.GoToHomePage();
            ViewContactDetails(0);

            string allInfo = driver.FindElement(By.Id("content")).Text;

            return new ContactData(allInfo)
            {
                AllInfo = allInfo
            };
        }

        public void ViewContactDetails(int index)
            {
                driver.FindElements(By.Name("entry"))[index]
                      .FindElements(By.TagName("td"))[6].
                      FindElement(By.TagName("a")).Click();
            }

        public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"))[7].
                  FindElement(By.TagName("a")).Click();
        }
        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
