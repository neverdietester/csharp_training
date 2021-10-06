using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Navigator.GoToHomePage();

            if (app.Contacts.IsContactExists() != true)
            {
                ContactData contact = new ContactData("a");
                contact.Lastname = ("b");
                app.Contacts.CreateContact(contact);
                app.Contacts.SelectItem();
            }
            
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove();
            
            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);
        }
    }
}
