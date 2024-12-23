namespace WebApp.Models.Services;

public class EfMovieCastService : IMovieCastService
{
    private readonly MoviesDbContext _dbContext;

    public EfMovieCastService(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddActorToMovie(MovieCastModel movieCast)
    {
        var movieCastEntity = MovieCastMapper.ToEntity(movieCast);
        _dbContext.MovieCasts.Add(movieCastEntity);
        _dbContext.SaveChanges();
    }

    public List<MovieModel> GetAvailableMoviesForActor(int actorId)
    {
        var existingMovieIds = _dbContext.MovieCasts
            .Where(mc => mc.PersonId == actorId)
            .Select(mc => mc.MovieId)
            .ToHashSet();

        return _dbContext.Movies
            .Where(m => !existingMovieIds.Contains(m.MovieId))
            .Select(m => MovieMapper.ToModel(m))
            .ToList();
    }
}
