using Microsoft.EntityFrameworkCore;
 
namespace bankAccounts.Models
{
    public class BankingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BankingContext(DbContextOptions<BankingContext> options) : base(options) { }
  
        // This DbSet contains "Person" objects and is called "Users"
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
       
    }
}