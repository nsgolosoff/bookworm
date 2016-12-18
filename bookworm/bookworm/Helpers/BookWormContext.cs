using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookworm.Entities;

namespace bookworm.Helpers
{
    public class BookWormContext2 : DbContext
    {
        public BookWormContext2() : base("BookWorm1") { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}