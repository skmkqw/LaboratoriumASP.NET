using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class MovieModel
{
    public int MovieId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    
    public int? Budget { get; set; }
    
    public float? Popularity { get; set; }
    
    [Url]
    public string Homepage { get; set; }
}
