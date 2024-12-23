using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class MovieCastModel
{
    public int MovieId { get; set; }
    
    public int PersonId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string CharacterName { get; set; }
}
