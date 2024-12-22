namespace WebApp.Models.Services;

public interface IMovieCastService
{
    void AddActorToMovie(MovieCastModel movieCast);
    List<MovieModel> GetAvailableMoviesForActor(int actorId);
}
