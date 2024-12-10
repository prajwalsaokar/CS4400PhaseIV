namespace BusinessAPI.DAL.Models.Views;
public class EmployeeView
{
    public string Username { get; set; }
    public string TaxID { get; set; }
    public DateTime Hired { get; set; }
    public int Experience { get; set; }
    public string? LicenseID { get; set; }
    public int? successful_trips { get; set; }
    public string Manager { get; set; }

}