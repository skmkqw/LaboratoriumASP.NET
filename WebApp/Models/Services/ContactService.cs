namespace WebApp.Models.Services;

public class ContactService : IContactService
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
                BirthDate = new DateOnly(2006, 4 ,1),
                Category = Category.Family
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
                BirthDate = new DateOnly(2000,  11,21),
                Category = Category.Friends
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
                BirthDate = new DateOnly(2002, 3 ,12),
                Category = Category.Business
            }
        },
    };

    private static int _currentId = 3;
    
    public void Add(ContactModel contact)
    {
        contact.Id = ++_currentId;
        
        _contacts.Add(contact.Id, contact);
    }

    public void Update(ContactModel contact)
    {
        if (_contacts.ContainsKey(contact.Id))
        {
            _contacts[contact.Id] = contact;
        }
    }

    public void Delete(int id)
    {
        _contacts.Remove(id);
    }

    public List<ContactModel> GetAll()
    {
        return _contacts.Values.ToList();
    }

    public ContactModel? GetById(int id)
    {
        return _contacts[id];
    }
}