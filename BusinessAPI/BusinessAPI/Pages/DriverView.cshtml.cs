using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessAPI.DAL;

namespace BusinessAPI.Pages;
public class DriverViewModel : PageModel
{
    private readonly CoreRepository _coreRepository;

    public List<DriverView> Drivers { get; set; }

    public DriverViewModel(CoreRepository coreRepository)
    {
        _coreRepository = coreRepository;
    }

    public async Task OnGetAsync()
    {
        Drivers = await _coreRepository.GetDriverView();
    }
}