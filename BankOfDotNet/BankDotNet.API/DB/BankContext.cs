using BankDotNet.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDotNet.API.DB
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options): base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
