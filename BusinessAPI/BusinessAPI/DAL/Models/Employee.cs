namespace BusinessAPI.DAL.Models;
public class Employee
{
    public string Username { get; set; }
    public string TaxID { get; set; }
    public DateTime Hired { get; set; }
    public int Experience { get; set; }
    public int Salary { get; set; }

}