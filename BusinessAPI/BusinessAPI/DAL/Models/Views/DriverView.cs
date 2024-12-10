namespace BusinessAPI.DAL.Models.Views;
public class DriverView
{
    public string username { get; set; }
    public string LicenseID { get; set; }
    public int successful_trips { get; set; }
    public int numOfVans { get; set; }

}