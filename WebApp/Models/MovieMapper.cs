namespace WebApp.Models;

public static class MovieMapper
{
    public static MovieEntity ToEntity(MovieModel model)
    {
        return new MovieEntity
        {
            MovieId = model.MovieId,
            Title = model.Title,
            Budget = model.Budget,
            Popularity = model.Popularity,
            Homepage = model.Homepage
        };
    }
    
    public static MovieModel ToModel(MovieEntity entity)
    {
        return new MovieModel
        {
            MovieId = entity.MovieId,
            Title = entity.Title,
            Budget = entity.Budget,
            Popularity = entity.Popularity,
            Homepage = entity.Homepage
        };
    }
}
