using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessAPI.DAL;

namespace BusinessAPI.Pages;

public class ServiceViewModel : PageModel
{
    private readonly CoreRepository _coreRepository;

    public List<ServiceView> Services { get; set; }

    public ServiceViewModel(CoreRepository coreRepository)
    {
        _coreRepository = coreRepository;
    }

    public async Task OnGetAsync()
    {
        Services = await _coreRepository.GetServiceView();
    }
}