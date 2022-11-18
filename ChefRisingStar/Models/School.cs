
using System;
using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class School : IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public int ContactId { get; set; }

        public School()
        {

        }
        public School(int id, string name, string address, string phone, string city, int contactId)
        {
            Id = id;
            Name = name;
            Address = address;
            Phone = phone;
            City = city;
            ContactId = contactId;
        }

        public override string ToString()
        {
            return $"{Name} - {City}";
        }

        public int CompareTo(object obj)
        {
            School school = obj as School;

            if (school == null)
                return -1;

            return school.Name.CompareTo(Name);
        }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {Name}";
        }
    }
}
