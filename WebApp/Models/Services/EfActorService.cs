namespace WebApp.Models.Services;

public class EfActorService : IActorService
{
    private readonly MoviesDbContext _dbContext;

    public EfActorService(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int GetPageCount(int pageSize)
    {
        return _dbContext.Persons.Count() / pageSize + 1;
    }

    public List<PersonModel> GetActorsWithMovieCounts(int pageNumber, int pageSize)
    {
        var actorsQuery = _dbContext.Persons
            .Select(person => new
            {
                Actor = person,
                MovieCount = _dbContext.MovieCasts.Count(mc => mc.PersonId == person.PersonId)
            });

        var actors = actorsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        List<PersonModel> actorModels = new List<PersonModel>();

        foreach (var actor in actors)
        {
            var movieRoles = _dbContext.MovieCasts
                .Where(mc => mc.PersonId == actor.Actor.PersonId)
                .OrderByDescending(mc => mc.Movie.Popularity)
                .Select(mc => new
                {
                    MovieTitle = mc.Movie.Title,
                    CharacterName = mc.CharacterName
                })
                .ToList();

            var movieRolesDictionary = movieRoles.ToDictionary(mr => mr.MovieTitle, mr => mr.CharacterName);

            var a = PersonMapper.ToModel(actor.Actor, actor.MovieCount, movieRolesDictionary);
            actorModels.Add(a);
        }

        return actorModels;
    }

    public PersonModel? GetActorById(int actorId)
    {
        var actor = _dbContext.Persons.Find(actorId);
        if (actor == null) return null;

        var movieCount = _dbContext.MovieCasts.Count(mc => mc.PersonId == actorId);
        
        var movieRoles = _dbContext.MovieCasts
            .Where(mc => mc.PersonId == actor.PersonId)
            .OrderByDescending(mc => mc.Movie.Popularity)
            .Select(mc => new
            {
                MovieTitle = mc.Movie.Title,
                CharacterName = mc.CharacterName
            })
            .ToList();

        var movieRolesDictionary = movieRoles.ToDictionary(mr => mr.MovieTitle, mr => mr.CharacterName);

        return PersonMapper.ToModel(actor, movieCount, movieRolesDictionary);
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
