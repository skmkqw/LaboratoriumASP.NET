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
    
    public static PersonModel ToModel(PersonEntity entity, int movieCount, List<string> characterNames)
    {
        return new PersonModel
        {
            PersonId = entity.PersonId,
            PersonName = entity.PersonName,
            MovieCount = movieCount,
            CharacterNames = characterNames
        };
    }
}
