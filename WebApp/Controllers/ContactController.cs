using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Services;

namespace WebApp.Controllers;

[Authorize(Roles = "admin user")]
public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // List of contact
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View(_contactService.GetAll());
    }
    
    // AddContact form
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    // Create new contact from form
    [HttpPost]
    public IActionResult Add([FromForm] ContactModel contact)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _contactService.Add(contact);
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Delete(int id)
    {
        _contactService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Info(int id)
    {
        return View(_contactService.GetById(id));
    }

    public IActionResult Edit(int id)
    {
        return View(_contactService.GetById(id));
    }

    [HttpPost]
    public IActionResult Edit(ContactModel contact)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _contactService.Update(contact);
        return RedirectToAction(nameof(Index));
    }
}