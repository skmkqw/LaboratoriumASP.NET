namespace WebApp.Models;

public class ActorListViewModel
{
    public List<PersonModel> Actors { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}