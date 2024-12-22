namespace WebApp.Models.Services;

public interface IActorService
{
    int GetPageCount(int pageSize);
    List<PersonModel> GetActorsWithMovieCounts(int pageNumber, int pageSize);
    PersonModel? GetActorById(int actorId);
    List<MovieModel> GetMoviesByActor(int actorId);
}
