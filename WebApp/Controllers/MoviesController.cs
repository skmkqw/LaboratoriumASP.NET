using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Services;

namespace WebApp.Controllers;
public class ActorListViewModel
{
    public List<PersonModel> Actors { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}

public class ActorMoviesViewModel
{
    public PersonModel Actor { get; set; }
    public List<MovieModel> Movies { get; set; }
}

public class AddMovieToActorViewModel
{
    public int ActorId { get; set; }
    public int MovieId { get; set; }
    public string CharacterName { get; set; }
    public List<MovieModel> AvailableMovies { get; set; }
}


[Authorize]
public class ActorController : Controller
{
    private readonly IActorService _actorService;
    private readonly IMovieCastService _movieCastService;

    public ActorController(IActorService actorService, IMovieCastService movieCastService)
    {
        _actorService = actorService;
        _movieCastService = movieCastService;
    }

    // Display list of actors with number of movies, and a link to their movies
    public IActionResult Index(int pageNumber = 1)
    {
        int pageSize = 20;
        int totalPages;
        var actors = _actorService.GetActorsWithMovieCounts(pageNumber, pageSize, out totalPages);

        var model = new ActorListViewModel
        {
            Actors = actors,
            CurrentPage = pageNumber,
            TotalPages = totalPages
        };

        return View("Index", model);
    }

    // Display movies for a specific actor
    public IActionResult Movies(int actorId)
    {
        var actor = _actorService.GetActorById(actorId);
        if (actor == null)
        {
            return NotFound();
        }

        var movies = _actorService.GetMoviesByActor(actorId);

        var model = new ActorMoviesViewModel
        {
            Actor = actor,
            Movies = movies
        };

        return View(model);
    }

    // Display form to add a movie to an actor
    [HttpGet]
    public IActionResult AddMovie(int actorId)
    {
        var availableMovies = _movieCastService.GetAvailableMoviesForActor(actorId);
        var model = new AddMovieToActorViewModel
        {
            ActorId = actorId,
            AvailableMovies = availableMovies
        };

        return View("AddMovieToActor", model);
    }

    // Handle the submission of the form to add a movie to an actor
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddMovie(AddMovieToActorViewModel model)
    {
        if (ModelState.IsValid)
        {
            var movieCastModel = new MovieCastModel
            {
                PersonId = model.ActorId,
                MovieId = model.MovieId,
                CharacterName = model.CharacterName
            };

            _movieCastService.AddActorToMovie(movieCastModel);

            // Redirect back to the actor's movie list after adding the movie
            return RedirectToAction("Movies", new { actorId = model.ActorId });
        }

        // If validation fails, reload available movies and show the form again
        var availableMovies = _movieCastService.GetAvailableMoviesForActor(model.ActorId);
        model.AvailableMovies = availableMovies;

        return View("AddMovieToActor", model);
    }

    // Implement pagination for the list of actors
    public IActionResult Pagination(int pageNumber, string action, int actorId)
    {
        int pageSize = 20;
        int totalPages;
        // switch (action)
        // {
        //     case "first":
        //         pageNumber = 1;
        //         break;
        //     case "last":
        //         pageNumber = totalPages;
        //         break;
        //     case "next":
        //         if (pageNumber < totalPages) pageNumber++;
        //         break;
        //     case "previous":
        //         if (pageNumber > 1) pageNumber--;
        //         break;
        // }

        var actors = _actorService.GetActorsWithMovieCounts(pageNumber, pageSize, out totalPages);
        var model = new ActorListViewModel
        {
            Actors = actors,
            CurrentPage = pageNumber,
            TotalPages = totalPages
        };

        return View("Index", model);
    }
}