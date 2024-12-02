namespace BusinessAPI.DAL.Models.Views;
public class DriverView
{
    public string Username { get; set; }
    public string LicenseID { get; set; }
    public int SuccessfulTrips { get; set; }
    public int VanCount { get; set; }

}