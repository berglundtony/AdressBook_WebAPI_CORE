using AdressBook_WebAPI_CORE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdressBook_WebAPI_CORE
{
    public class ContactDataStore
    {
        public static ContactDataStore Current { get; } = new ContactDataStore();
        public List<Contact> _Contacts { get; set; }

        public ContactDataStore()
        {
            _Contacts = new List<Contact>()
            {
                new Contact
                {
                    Id = 1, Firstname = "Tony", Lastname = "Berglund", Mobile = "070-4948398", DateOfBirth = "1971-03-14",
                    AdressInfo = { Id = 1, Street = "Holbergsgatan 124", PostalCode = 16845, City = "Bromma" }
                },
                new Contact
                {
                    Id = 2, Firstname = "Per-Erik", Lastname = "Centerkvist", Mobile = "070-4414697", DateOfBirth = "1963-12-22",
                    AdressInfo = { Id = 2, Street = "Travarvägen 59", PostalCode = 19391, City = "Sigtuna" }
                }
        };
        }
    }
}
