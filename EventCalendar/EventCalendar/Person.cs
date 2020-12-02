using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCalendar
{
    public class Person
    {
        public Person(string firstName, string lastName, long oib, long phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            OIB = oib;
            PhoneNumber = phoneNumber;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long OIB { get; set; }
        public long PhoneNumber { get; set; }
    }
}
