using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using LinqToDB;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]

    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string allPhones;
        public string allEmails;

        public ContactData(string firstname)
        {
            FirstName = firstname;
        }
        public ContactData()
        {
           
        }
        public ContactData(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            {
                return FirstName == other.FirstName && LastName == other.LastName;
            }
        }

        public override int GetHashCode()
            {
            return FirstName.GetHashCode();
            }

        public override string ToString()
        {
            return "firstname" + FirstName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return FirstName.CompareTo(other.FirstName);
        }

        private string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ )(-]", "") + "\r\n";

        }

        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return (email) + "\r\n";

        }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastName")]
        public string LastName { get; set; }

        [Column(Name = "middleName")]
        public string MiddleName { get; set; }

        [Column(Name = "nickName")]
        public string NickName { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "homePage")]
        public string HomePage { get; set; }

        [Column(Name = "bday")]
        public string BDay { get; set; }

        [Column(Name = "bmonth")]
        public string BMonth { get; set; }

        [Column(Name = "byear")]
        public string BYear { get; set; }

        [Column(Name = "aday")]
        public string ADay { get; set; }

        [Column(Name = "amonth")]
        public string AMonth { get; set; }

        [Column(Name = "ayear")]
        public string AYear { get; set; }

        [Column(Name = "address2")]
        public string Address2 { get; set; }

        [Column(Name = "phone2")]
        public string Phone2 { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity, NotNull]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhone(HomePhone) + CleanUpPhone(MobilePhone) + CleanUpPhone(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
       {
           get
           {
               if (allEmails != null)
                {
                  return allEmails;
               }
              else
              {
                   return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
              }
            }
           set
            {
                allEmails = value;
            }
       }

        public string AllEditInfo
        {
            get
            {
                return CleanUpFirstName(FirstName) + CleanUpLastName(LastName) + CleanUpLastAddress(Address)
                    + CleanUpHPhone(HomePhone) + CleanUpMPhone(MobilePhone) + CleanUpWPhone(WorkPhone)
                    + AllEmails;
            }
            set
            {
                AllEditInfo = value;
            }
        }

        private string CleanUpHPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return ("H: " + phone + "\r\n");

        }

        private string CleanUpMPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return ("M: " + phone + "\r\n");

        }

        private string CleanUpWPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return ("W: " + phone + "\r\n\r\n");

        }
        private string CleanUpFirstName(string firstname)
        {
            if (firstname == null || firstname == "")
            {
                return "";
            }
            return (firstname) + " ";

        }

        private string CleanUpLastName(string lastname)
        {
            if (lastname == null || lastname == "")
            {
                return "";
            }
            return (lastname) + "\r\n";

        }
        private string CleanUpLastAddress(string address)
        {
            if (address == null || address == "")
            {
                return "";
            }
            return (address) + "\r\n\r\n";

        }


        public string AllInfo { get; set; }

    }
}
