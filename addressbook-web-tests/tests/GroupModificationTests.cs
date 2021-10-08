using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
    
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
            [Test]
            public void GroupModificationTest()
            {
                app.Navigator.GoToGroupPage();

                if (app.Groups.IsGroupExists() != true)
                {
                GroupData group = new GroupData("aaa");
                group.Header = "";
                group.Footer = "";
                app.Groups.Create(group);
                }

                List<GroupData> oldGroups = app.Groups.GetGroupList();

                GroupData newData = new GroupData("zzz");
                newData.Header = null;
                newData.Footer = null;
                app.Groups.Modify(0, newData);

                List<GroupData> newGroups = app.Groups.GetGroupList();
                oldGroups[0].Name = newData.Name;
                oldGroups.Sort();
                newGroups.Sort();
                Assert.AreEqual(oldGroups, newGroups);
        }
    }

}
