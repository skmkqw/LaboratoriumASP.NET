using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("person")]
public class PersonEntity
{
    [Key]
    [Column("person_id")]
    public int PersonId { get; set; }
    
    [Required]
    [Column("person_name")]
    public string PersonName { get; set; }
}
