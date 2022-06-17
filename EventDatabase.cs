using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace CICA
{
    class EventDatabase : DbContext // class responsible for interacting with the database
    {
        public DbSet<Event> Events { get; set; } //database session where instances of the entities can be queried and saved
    }
}