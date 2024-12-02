namespace BusinessAPI.DAL.Models.Views;
public class LocationView
{
    public string LocationLabel { get; set; }
    public string BusinessName { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int TotalSpace { get; set; }
    public int TotalVans { get; set; }
    public string VanIdentifiers { get; set; }
    public int AvailableCapacity { get; set; }

}