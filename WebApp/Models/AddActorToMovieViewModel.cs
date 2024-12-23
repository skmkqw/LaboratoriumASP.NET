using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AddMovieToActorViewModel
{
    public int ActorId { get; set; }
    
    public string ActorFullName { get; set; }
    
    [Required(ErrorMessage = "Please select a movie")]
    public int MovieId { get; set; }
    
    [Required(ErrorMessage = "Please enter a character name")]
    [MaxLength(200, ErrorMessage = "Character name cannot be longer than 200 characters")]
    public string CharacterName { get; set; }
    public List<MovieModel> AvailableMovies { get; set; }
}