using System.ComponentModel.DataAnnotations;

namespace ProjectDb.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; } 

       
        [StringLength(100)]
        public required string UserName { get; set; } 

        
        [StringLength(100)]
        public required string Password { get; set; } 

        

       

    }
}