using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            app.Navigator.GoToHomePage();

            if (app.Contacts.IsContactExists() != true)
            {
                ContactData newcontact = new ContactData("a");
                newcontact.LastName = ("b");
                app.Contacts.CreateContact(newcontact);
            }
            app.Navigator.GoToGroupPage();

            if (app.Groups.IsGroupExists() != true)
            {
                GroupData newgroup = new GroupData("aaa");
                newgroup.Header = "";
                newgroup.Footer = "";
                app.Groups.Create(newgroup);
            }


            /*GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();*/

            foreach (ContactData contact in ContactData.GetAll())
            { 
                if (contact)
            }


            app.Contacts.AddContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestRemoveContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().First();

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
