namespace BusinessAPI.DAL.Models.Views;
public class EmployeeView
{
    public string Username { get; set; }
    public string TaxID { get; set; }
    public decimal Salary { get; set; }
    public DateTime Hired { get; set; }
    public int Experience { get; set; }
    public string LicenseID { get; set; }
    public string SuccessfulTrips { get; set; }
    public string Manager { get; set; }

}