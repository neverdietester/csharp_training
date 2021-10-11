using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Navigator.GoToHomePage();

            if (app.Contacts.IsContactExists() != true)
            {
                ContactData contact = new ContactData("a");
                contact.Lastname = ("b");
                app.Contacts.CreateContact(contact);
                app.Contacts.SelectItem();
            }

            ContactData newData = new ContactData("lol");
            newData.Lastname = null;
            newData.Bday = null;

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Modify(newData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[3].Firstname = newData.Firstname;
            oldContacts[2].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}