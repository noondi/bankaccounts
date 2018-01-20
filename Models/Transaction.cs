using System.ComponentModel.DataAnnotations.Schema;

namespace bankAccounts.Models 
{  
    public class Transaction: BaseEntity
    {
        public int TransactionId {get; set; }
        public decimal Amount {get; set; }
        public int UserId {get; set; } 
        // because we have a userId in Transaction we need
        // to have the user present in that table
        
        [ForeignKey("UserId")]
        public User AccountOwner {get; set; }   
    }
}