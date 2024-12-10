namespace BusinessAPI.DAL.Models.Views;
public class OwnerView
{
    public string Username { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string address { get; set; }
    public int num_businesses { get; set; }
    public int diff_places { get; set; }
    public int high_ratings { get; set; }
    public int low_ratings { get; set; }
    public decimal total_debt { get; set; }

}