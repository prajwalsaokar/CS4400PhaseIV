using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessAPI.DAL;

namespace BusinessAPI.Pages;

public class EmployeeViewModel : PageModel
{
    private readonly CoreRepository _coreRepository;

    public List<EmployeeView> Employees { get; set; }

    public EmployeeViewModel(CoreRepository coreRepository)
    {
        _coreRepository = coreRepository;
    }

    public async Task OnGetAsync()
    {
        Employees = await _coreRepository.GetEmployeeView();
    }
}