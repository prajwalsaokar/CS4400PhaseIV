using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessAPI.DAL;

public class OwnerViewModel : PageModel
{
    private readonly CoreRepository _coreRepository;

    public List<OwnerView> Owners { get; set; }

    public OwnerViewModel(CoreRepository coreRepository)
    {
        _coreRepository = coreRepository;
    }

    public async Task OnGetAsync()
    {
        Owners = await _coreRepository.GetOwnerView();
    }
}