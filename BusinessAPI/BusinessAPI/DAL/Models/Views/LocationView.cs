namespace BusinessAPI.DAL.Models.Views;
public class LocationView
{
    public string label { get; set; }
    public string long_name { get; set; }
    public int x_coord { get; set; }
    public int y_coord { get; set; }
    public int space { get; set; }
    public int num_vans { get; set; }
    public string van_ids { get; set; }
    public int remaining_capacity { get; set; }


}