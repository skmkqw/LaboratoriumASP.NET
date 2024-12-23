namespace WebApp.Models;

public class AddMovieToActorViewModel
{
    public int ActorId { get; set; }

    public string ActorFullName { get; set; }
    public int MovieId { get; set; }
    public string CharacterName { get; set; }
    public List<MovieModel> AvailableMovies { get; set; }
}