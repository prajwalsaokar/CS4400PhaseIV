using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessAPI.DAL;

namespace BusinessAPI.Pages;

public class ProductViewModel : PageModel
{
    private readonly CoreRepository _coreRepository;

    public List<ProductView> Products { get; set; }

    public ProductViewModel(CoreRepository coreRepository)
    {
        _coreRepository = coreRepository;
    }

    public async Task OnGetAsync()
    {
        Products = await _coreRepository.GetProductView();
    }
}