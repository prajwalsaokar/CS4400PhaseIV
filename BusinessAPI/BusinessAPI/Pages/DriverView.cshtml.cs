using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DriverViewModel : PageModel
{
    private readonly IDriverService _driverService;

    public List<DriverView> Drivers { get; set; }

    public DriverViewModel(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public async Task OnGetAsync()
    {
        Drivers = await _driverService.GetDriverView();
    }
}