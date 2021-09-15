using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests

{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        protected void SetupTest()
        {
            app = new ApplicationManager();
        }

        [TearDown]
        protected void TeardownTest()
        {
            app.Stop();
        }

    }

}
