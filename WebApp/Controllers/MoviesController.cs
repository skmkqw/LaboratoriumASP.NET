using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Services;

namespace WebApp.Controllers;

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

            return RedirectToAction("Movies", new { actorId = model.ActorId });
        }

        var availableMovies = _movieCastService.GetAvailableMoviesForActor(model.ActorId);
        model.AvailableMovies = availableMovies;

        return View("AddMovieToActor", model);
    }

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