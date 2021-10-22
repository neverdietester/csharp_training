using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;


namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2];
            string type = args[3];

            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }

            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(10))
                {
                    LastName = TestBase.GenerateRandomString(30),
                    MiddleName = TestBase.GenerateRandomString(30),
                    NickName = TestBase.GenerateRandomString(50),
                    Title = TestBase.GenerateRandomString(100),
                    Company = TestBase.GenerateRandomString(100),
                    Address = TestBase.GenerateRandomString(100),
                    HomePhone = TestBase.GenerateRandomString(20),
                    MobilePhone = TestBase.GenerateRandomString(20),
                    WorkPhone = TestBase.GenerateRandomString(20),
                    Fax = TestBase.GenerateRandomString(20),
                    Email = TestBase.GenerateRandomString(30),
                    Email2 = TestBase.GenerateRandomString(30),
                    Email3 = TestBase.GenerateRandomString(30),
                    HomePage = TestBase.GenerateRandomString(30),
                    Address2 = TestBase.GenerateRandomString(100),
                    Phone2 = TestBase.GenerateRandomString(20),
                    Notes = TestBase.GenerateRandomString(100),
                });
            }
            StreamWriter writer = new StreamWriter(filename);
            if (format == "json" & type == "groups")
            {
                writeGroupsToJsonFile(groups, writer);
            }
            else if (format == "xml" & type == "groups")
            {
                writeGroupsToXmlFile(groups, writer);
            }
            else if (format == "xml" & type == "contacts")
            {
                writeContactsToXmlFile(contacts, writer);
            }
            else if (format == "json" & type == "contacts")
            {
                writeContactsToJsonFile(contacts, writer);
            }
            else 
            {
                System.Console.Out.Write("Unrecognized format " + format);
            }

            writer.Close();
        }

        private static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), filename));

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write (JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        private static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        private static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        private static void writeContactsToExcelFile(List<ContactData> contacts, string filename)
        {

            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.FirstName;
                sheet.Cells[row, 2] = contact.LastName;
                sheet.Cells[row, 3] = contact.MiddleName;
                sheet.Cells[row, 4] = contact.NickName;
                sheet.Cells[row, 5] = contact.Title;
                sheet.Cells[row, 6] = contact.Company;
                sheet.Cells[row, 7] = contact.Address;
                sheet.Cells[row, 8] = contact.HomePhone;
                sheet.Cells[row, 9] = contact.MobilePhone;
                sheet.Cells[row, 10] = contact.WorkPhone;
                sheet.Cells[row, 11] = contact.Fax;
                sheet.Cells[row, 12] = contact.Email;
                sheet.Cells[row, 13] = contact.Email2;
                sheet.Cells[row, 14] = contact.Email3;
                sheet.Cells[row, 15] = contact.HomePage;
                sheet.Cells[row, 16] = contact.Address2;
                sheet.Cells[row, 17] = contact.Phone2;
                sheet.Cells[row, 18] = contact.Notes;

                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), filename));

            wb.Close();
            app.Visible = false;
            app.Quit();
        }
    }
}