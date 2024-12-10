namespace BusinessAPI.DAL.Models.Views;
public class ProductView
{
    public string product_name { get; set; }
    public string location { get; set; }
    public int total_quantity { get; set; }
    public decimal min_price { get; set; }
    public decimal max_price { get; set; }

}