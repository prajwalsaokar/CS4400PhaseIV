using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OwnerViewModel : PageModel
{
    private readonly IOwnerService _ownerService;

    public List<OwnerView> Owners { get; set; }

    public OwnerViewModel(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    public async Task OnGetAsync()
    {
        // Fetch data from the database using the service
        Owners = await _ownerService.GetOwnerView();
    }
}