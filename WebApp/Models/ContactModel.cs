using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    [MinLength(2, ErrorMessage = "First name must have at least 2 characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    [MinLength(2, ErrorMessage = "Last name must have at least 2 characters")]
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    [RegularExpression("\\d{3} \\d{3} \\d{3}", ErrorMessage = "Wrong phone number format (should be xxx-xxx-xxx)")]
    public string PhoneNumber { get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
}