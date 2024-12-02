namespace BusinessAPI.DAL.Models.Views;
public class OwnerView
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public int NumBusinesses { get; set; }
    public int DiffPlaces { get; set; }
    public int HighRatings { get; set; }
    public int LowRatings { get; set; }
    public decimal TotalDebt { get; set; }

}