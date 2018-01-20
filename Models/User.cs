using System.Collections.Generic;

namespace bankAccounts.Models 
{  
    public class User: BaseEntity
    {
        public int UserId {get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }
        public string Email {get; set; } 
        public string Password {get; set; } 
        public decimal Balance {get; set; } 
        // As a consequence of our user being in the
        // tranction table, here we have a list of 
        //transactions and a user constructor
        public List<Transaction> UserTransactions {get; set; } 
        // this list must be public otherwise u wont be to access it
        public User() 
        {
            UserTransactions = new List<Transaction>();
        }  
    }
}