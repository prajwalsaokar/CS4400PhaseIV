namespace BusinessAPI.DAL.Models.Views;
public class ServiceView
{
    public string Id { get; set; }
    public string LongName { get; set; }
    public string HomeBase { get; set; }
    public string Manager { get; set; }
    public decimal TotalSales { get; set; }
    public int UniqueProducts { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TotalWeight { get; set; }

}