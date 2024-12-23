namespace WebApp.Models;

public static class MovieCastMapper
{
    public static MovieCastEntity ToEntity(MovieCastModel model)
    {
        return new MovieCastEntity
        {
            MovieId = model.MovieId,
            PersonId = model.PersonId,
            CharacterName = model.CharacterName
        };
    }
    
    public static MovieCastModel ToModel(MovieCastEntity entity)
    {
        return new MovieCastModel
        {
            MovieId = entity.MovieId,
            PersonId = entity.PersonId,
            CharacterName = entity.CharacterName
        };
    }
}
