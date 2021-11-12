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

            List<GroupData> groups = GroupData.GetAll();
            GroupData group = groups[0];

            List<ContactData> oldList = ContactData.GetAll();
            ContactData contact = oldList[0];
            int numberOfGroups = groups.Count();
            for (int i = 0; i < numberOfGroups; i++)
            {
                group = groups[i];
                oldList = group.GetContacts();
                try
                {
                    contact = ContactData.GetAll().Except(oldList).First();
                    break;
                }
                catch (InvalidOperationException)
                {
                    if ((numberOfGroups - 1) == i)
                    {
                        app.Contacts.CreateContact(new ContactData("cc", "hh"));
                        contact = ContactData.GetAll()[0];
                    }
                }
            }

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }


        [Test]
        public void TestRemoveContactFromGroup()
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

            GroupData group = GroupData.GetAll()[0];

            List<ContactData> allContacts = ContactData.GetAll();
            ContactData contact = allContacts[0];
            List<GroupData> allGroups = GroupData.GetAll();

            for (int i = 0; i < allGroups.Count; i++)
            {
                if (allGroups[i].GetContacts().Count() > 0)
                {
                    group = allGroups[i];
                    allContacts = group.GetContacts();
                    contact = allContacts[0];
                    break;
                }
                else if (allGroups.Count == (i + 1))
                {
                    group = allGroups[i];
                    app.Contacts.AddContactToGroup(contact, group);
                }
            }

            app.Contacts.RemoveContactFromGroup(contact, group);
            List<ContactData> newList = group.GetContacts();
            allContacts.Remove(contact);
            newList.Sort();
            allContacts.Sort();

            Assert.AreEqual(allContacts, newList);
        }
    }
}

