using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankAccounts.Models 
{
    
    public abstract class BaseEntity
    {
        // This db generate is because in the db 
        //these values were set to default
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt {get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get; set; }
    
    }
}
