using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1,
            new ContactModel()
            {
                Id = 1,
                FirstName = "Timofei",
                LastName = "Korsakov",
                Email = "tkorsakov77@gmail.com",
                PhoneNumber = "123 456 789",
                BirthDate = new DateOnly(2006, 4 ,1)
            }
        },
        {
            2,
            new ContactModel()
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                Email = "j.doe@gmail.com",
                PhoneNumber = "221 359 126",
                BirthDate = new DateOnly(2000,  11,21)
            }
        },
        {
            3,
            new ContactModel()
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "j.smith@gmail.com",
                PhoneNumber = "580 051 582",
                BirthDate = new DateOnly(2002, 3 ,12)
            }
        },
    };

    private static int _currentId = 3;
    
    // List of contacts
    public IActionResult Index()
    {
        return View(_contacts);
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

        contact.Id = ++_currentId;
        
        _contacts.Add(contact.Id, contact);

        return View("Index", _contacts);
    }
    
    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }

    public IActionResult Info(int id)
    {
        return View(_contacts[id]);
    }

    public IActionResult Edit(int id)
    {
        throw new NotImplementedException();
    }
}