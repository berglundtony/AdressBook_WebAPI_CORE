using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdressBook_WebAPI_CORE.Data
{
    public class ContactForUpdate
    {
            [Required(ErrorMessage = "You should provide a firstname value")]
            [MaxLength(50)]
            public string Firstname { get; set; }
            [Required(ErrorMessage = "You should provide a lastname value")]
            [MaxLength(50)]
            public string Lastname { get; set; }
            [Required(ErrorMessage = "You should provide a mobile number")]
            [MaxLength(25)]
            public string Mobile { get; set; }
            [Required(ErrorMessage = "You should provide an date of birth")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public string DateOfBirth { get; set; }
            public AdressForUpdate AdressInfo = new AdressForUpdate();


            public class AdressForUpdate
            {
                [Required(ErrorMessage = "You should provide a streetadress")]
                [MaxLength(50)]
                public string Street { get; set; }
                [Required(ErrorMessage = "You should provide a postalcode")]
                [MaxLength(8)]
                public int PostalCode { get; set; }
                [Required(ErrorMessage = "You should provide a city")]
                [MaxLength(50)]
                public string City { get; set; }
            }
    }
}
