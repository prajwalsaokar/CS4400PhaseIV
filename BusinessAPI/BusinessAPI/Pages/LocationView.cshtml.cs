using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessAPI.DAL;

namespace BusinessAPI.Pages;

public class LocationViewModel : PageModel
{
    private readonly CoreRepository _coreRepository;

    public List<LocationView> Locations { get; set; }

    public LocationViewModel(CoreRepository coreRepository)
    {
        _coreRepository = coreRepository;
    }

    public async Task OnGetAsync()
    {
        Locations = await _coreRepository.GetLocationView();
    }
}