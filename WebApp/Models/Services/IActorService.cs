namespace WebApp.Models.Services;

public interface IActorService
{
    List<PersonModel> GetActorsWithMovieCounts(int pageNumber, int pageSize, out int totalPages);
    PersonModel? GetActorById(int actorId);
    List<MovieModel> GetMoviesByActor(int actorId);
}
