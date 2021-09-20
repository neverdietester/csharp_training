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
            ContactData contact = new ContactData("a");
            contact.Lastname = ("b");

            app.Contacts.CreateContact(contact);
        }
    }
}
