namespace BusinessAPI.DAL.Models.Views;
public class ProductView
{
    public string ProductName { get; set; }
    public string Location { get; set; }
    public int TotalPackages { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }

}