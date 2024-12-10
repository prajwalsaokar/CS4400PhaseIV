namespace BusinessAPI.DAL.Models.Views;
public class ServiceView
{
    public string id { get; set; }
    public string long_name { get; set; }
    public string home_base { get; set; }
    public string manager { get; set; }
    public decimal total_revenue { get; set; }
    public int unique_products { get; set; }
    public decimal total_cost { get; set; }
    public decimal total_weight { get; set; }

}