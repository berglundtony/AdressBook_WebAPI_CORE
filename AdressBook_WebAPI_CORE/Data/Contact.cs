using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdressBook_WebAPI_CORE.Data
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Firstname{ get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateOfBirth { get; set; }
        public Adress AdressInfo = new Adress();


        public class Adress
        {
            public int Id { get; set; }
            public string Street { get; set; }
            public int PostalCode { get; set; }
            public string City { get; set; }
        }
    }


}
