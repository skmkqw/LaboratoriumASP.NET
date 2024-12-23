using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Services;

namespace WebApp.Controllers;

public class ActorController : Controller
{
    private readonly IActorService _actorService;
    private readonly IMovieCastService _movieCastService;

    public ActorController(IActorService actorService, IMovieCastService movieCastService)
    {
        _actorService = actorService;
        _movieCastService = movieCastService;
    }

    public IActionResult Index(int pageNumber = 1)
    {
        int pageSize = 20;
        
        int totalPages = _actorService.GetPageCount(pageSize);
        
        var actors = _actorService.GetActorsWithMovieCounts(pageNumber, pageSize);

        var model = new ActorListViewModel
        {
            Actors = actors,
            CurrentPage = pageNumber,
            TotalPages = totalPages
        };

        return View("Index", model);
    }

    public IActionResult Movies(int actorId)
    {
        var actor = _actorService.GetActorById(actorId);
        if (actor == null)
        {
            return NotFound();
        }

        var movies = _actorService.GetMoviesByActor(actorId)
            .OrderByDescending(m => m.Popularity)
            .ToList();

        var model = new ActorMoviesViewModel
        {
            Actor = actor,
            Movies = movies
        };

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public IActionResult AddMovie(int actorId)
    {
        var actor = _actorService.GetActorById(actorId);
        if (actor == null)
        {
            return NotFound();
        }
        
        var availableMovies = _movieCastService.GetAvailableMoviesForActor(actorId);
        
        var model = new AddMovieToActorViewModel
        {
            ActorId = actorId,
            ActorFullName = actor.PersonName,
            AvailableMovies = availableMovies
        };

        return View("AddMovieToActor", model);
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddMovie(int actorId, int movieId, string characterName)
    {
        if (actorId <= 0 || movieId <= 0 || string.IsNullOrWhiteSpace(characterName))
        {
            ModelState.AddModelError(string.Empty, "All fields are required.");
        
            var availableMovies = _movieCastService.GetAvailableMoviesForActor(actorId);
            var model = new AddMovieToActorViewModel
            {
                ActorId = actorId,
                ActorFullName = _actorService.GetActorById(actorId)?.PersonName ?? "Unknown",
                MovieId = movieId,
                CharacterName = characterName,
                AvailableMovies = availableMovies
            };
        
            return View("AddMovieToActor", model);
        }

        var movieCastModel = new MovieCastModel
        {
            PersonId = actorId,
            MovieId = movieId,
            CharacterName = characterName
        };

        _movieCastService.AddActorToMovie(movieCastModel);

        return RedirectToAction("Movies", new { actorId = actorId });
    }

    public IActionResult Pagination(int pageNumber)
    {
        int pageSize = 20;
        int totalPages = _actorService.GetPageCount(pageSize);

        var actors = _actorService.GetActorsWithMovieCounts(pageNumber, pageSize);
        
        var model = new ActorListViewModel
        {
            Actors = actors,
            CurrentPage = pageNumber,
            TotalPages = totalPages
        };

        return View("Index", model);
    }
}