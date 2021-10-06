using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.GoToGroupPage();

            if (app.Groups.IsGroupExists() != true)
            {
                GroupData group = new GroupData("aaa");
                group.Header = "";
                group.Footer = "";
                app.Groups.Create(group);
            }
            
            app.Groups.Remove(1);
        }
    }
}
