using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using LinqToDB;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]

    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {

        public GroupData(string name)
        {
            Name = name;
        }

        public GroupData()
        {

        }
        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name + "\nheader= " + Header + "\nfooter =" + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        [Column(Name = "group_name")]
        public string Name { get; set; }


        [Column(Name = "group_header")]
        public string Header { get; set; }


        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]

        public string Id { get; set; }

        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == this.Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }
       public static List<GroupData> GetNonEmptyGroups()
        {
            return GetAll().Where(g => g.GetContacts().Count > 0).ToList();
             
        }

        public static List<GroupData> GetEmptyGroups()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return GetAll().Where(g => g.GetContacts().Count == 0).ToList();
            }
        }


        public static List<ContactData> GetContactsInGroup()

        {
            using (AddressBookDB db = new AddressBookDB())
            {
                
                var list = (from c in db.Contacts join gc in db.GCR on c.Id equals gc.ContactId select c).ToList();
                return list;
            }
        }
    }
}
