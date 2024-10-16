namespace WebApp.Models;

public class Birth
{
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Name) && BirthDate.HasValue && BirthDate.Value < DateTime.Now;
    }
    
    public int CalculateAge()
    {
        if (!BirthDate.HasValue)
        {
            return -1;
        }

        var today = DateTime.Today;
        var age = today.Year - BirthDate.Value.Year;
        
        if (BirthDate.Value.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}