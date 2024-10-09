using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult Calculator(string op, double? x, double? y)
    {
        // var op = Request.Query["op"];
        // var x = double.Parse(Request.Query["x"]);
        // var y = double.Parse(Request.Query["y"]);

        if (x is null || y is null)
        {
            ViewBag.ErrorMessage = "Wrong x/y format!";
            return View("CalculatorError");
        }
        
        double? result = 0.0;

        switch (op)
        {
            case "add":
                result = x + y;
                ViewBag.Operation = "+";
                break;
            case "sub":
                result = x - y;
                ViewBag.Operation = "-";
                break;
            case "mul":
                result = x * y;
                ViewBag.Operation = "*";
                break;
            case "div":
                result = x / y;
                ViewBag.Operation = "/";
                break;
            default:
                ViewBag.ErrorMessage = "Unknown operation!";
                return View("CalculatorError");
        }
        
        ViewBag.Result = result;
        ViewBag.X = x;
        ViewBag.Y = y;
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}