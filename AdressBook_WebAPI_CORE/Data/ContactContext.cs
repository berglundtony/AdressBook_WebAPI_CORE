using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdressBook_WebAPI_CORE.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options): base(options)
        {
         
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Contact.Adress> Adresses { get; set; }
    }
}
