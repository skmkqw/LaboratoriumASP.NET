namespace WebApp.Models.Services;

public class EfActorService : IActorService
{
    private readonly MoviesDbContext _dbContext;

    public EfActorService(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<PersonModel> GetActorsWithMovieCounts(int pageNumber, int pageSize, out int totalPages)
    {
        var query = _dbContext.Persons
            .Select(person => new
            {
                Actor = person,
                MovieCount = _dbContext.MovieCasts.Count(mc => mc.PersonId == person.PersonId),
                CharacterNames = _dbContext.MovieCasts
                    .Where(mc => mc.PersonId == person.PersonId)
                    .OrderByDescending(mc => mc.Movie.Popularity)
                    .Select(mc => mc.CharacterName)
                    .ToList()
            })
            .AsQueryable(); 

        totalPages = (int)Math.Ceiling(query.Count() / (double)pageSize);

        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(result => PersonMapper.ToModel(result.Actor, result.MovieCount, result.CharacterNames))
            .ToList();
    }

    public PersonModel? GetActorById(int actorId)
    {
        var actor = _dbContext.Persons.Find(actorId);
        if (actor == null) return null;

        var movieCount = _dbContext.MovieCasts.Count(mc => mc.PersonId == actorId);
        var characterNames = _dbContext.MovieCasts
            .Where(mc => mc.PersonId == actorId)
            .Select(mc => mc.CharacterName)
            .ToList();

        return PersonMapper.ToModel(actor, movieCount, characterNames);
    }

    public List<MovieModel> GetMoviesByActor(int actorId)
    {
        return _dbContext.MovieCasts
            .Where(mc => mc.PersonId == actorId)
            .Join(
                _dbContext.Movies,
                mc => mc.MovieId,
                m => m.MovieId,
                (mc, m) => MovieMapper.ToModel(m)
            )
            .ToList();
    }
}
