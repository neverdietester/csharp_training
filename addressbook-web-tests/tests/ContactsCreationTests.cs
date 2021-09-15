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
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.InitNewContactCreation();
            ContactData contact = new ContactData("a");
            contact.Lastname = ("b");
            app.Contacts.FillContactForm(contact);
            app.Contacts.EnterContactCreation();
            app.Contacts.ReturnToContactPage();
            app.Auth.Logout();
        }
    }
}
