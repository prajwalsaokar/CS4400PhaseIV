using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessAPI.DAL.Models.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeeViewModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public List<EmployeeView> Employees { get; set; }

    public EmployeeViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task OnGetAsync()
    {
        // Fetch data from the database using the service
        Employees = await _employeeService.GetEmployeeView();
    }
}