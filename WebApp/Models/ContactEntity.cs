using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("Contacts")]
public class ContactEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    public string Email { get; set; }
    
    [Column("phone")]
    public string PhoneNumber { get; set; }
    
    public DateOnly BirthDate { get; set; }
    
    public Category Category { get; set; }

    public DateTime CreatedAt { get; set; }
}