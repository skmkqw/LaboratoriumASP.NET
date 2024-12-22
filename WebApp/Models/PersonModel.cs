using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class PersonModel
{
    public int PersonId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string PersonName { get; set; }
    
    public int MovieCount { get; set; }
    
    public List<string> CharacterNames { get; set; }
}