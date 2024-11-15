using Microsoft.EntityFrameworkCore;
using Ticketing.Models;

namespace Ticketing.Data
{
    public class AppDBContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
