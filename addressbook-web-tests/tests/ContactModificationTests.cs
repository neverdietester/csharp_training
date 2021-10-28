using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Navigator.GoToHomePage();

            if (app.Contacts.IsContactExists() != true)
            {
                ContactData contact = new ContactData("a");
                contact.LastName = ("b");
                app.Contacts.CreateContact(contact);
                app.Contacts.SelectItem();
            }

            ContactData newData = new ContactData("lol");
            newData.LastName = null;
            newData.BDay = null;

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModify = oldContacts[0];

            //app.Contacts.Modify(newData);
            app.Contacts.Modify(toBeModify, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = newData.FirstName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                //if (group.Id == oldData.Id)
                if (contact.Id == toBeModify.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                }
            }
        }
    }
}