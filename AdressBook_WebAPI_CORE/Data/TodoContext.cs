using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdressBook_WebAPI_CORE.Data;

namespace AdressBook_WebAPI_CORE.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        public DbSet<Todo>Todos{ get; set; }

        public DbSet<AdressBook_WebAPI_CORE.Data.Contact> Contact { get; set; }
}
}
