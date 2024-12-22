namespace WebApp.Models;

public static class PersonMapper
{
    public static PersonEntity ToEntity(PersonModel model)
    {
        return new PersonEntity
        {
            PersonId = model.PersonId,
            PersonName = model.PersonName
        };
    }
    
    public static PersonModel ToModel(PersonEntity entity, int movieCount, Dictionary<string, string> movieCharacterNames)
    {
        return new PersonModel
        {
            PersonId = entity.PersonId,
            PersonName = entity.PersonName,
            MovieCount = movieCount,
            MovieRoles = movieCharacterNames
        };
    }
}
