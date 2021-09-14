using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactsCreationTests : TestBase
    {
        [Test]
        public void ContactsCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();
            ContactData contact = new ContactData("a");
            contact.Lastname = ("b");
            FillContactForm(contact);
            EnterContactCreation();
            ReturnToContactPage();
            Logout();
        }
    }
}
