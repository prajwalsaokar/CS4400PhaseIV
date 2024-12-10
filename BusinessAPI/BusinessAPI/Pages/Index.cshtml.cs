using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using BusinessAPI.DAL;
namespace BusinessAPI.Pages;
public class Index : PageModel
{
    private readonly CoreRepository _repository;

    public Index(CoreRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public string FormId { get; set; }

    [BindProperty]
    public AddOwnerModel AddOwnerForm { get; set; }

    [BindProperty]
    public AddEmployeeModel AddEmployeeForm { get; set; }

    [BindProperty]
    public AddDriverRoleModel AddDriverRoleForm { get; set; }

    [BindProperty]
    public AddProductModel AddProductForm { get; set; }

    [BindProperty]
    public AddVanModel AddVanForm { get; set; }

    [BindProperty]
    public AddBusinessModel AddBusinessForm { get; set; }

    // Add more models for other forms as needed...

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            switch (FormId)
            {
                case "AddOwner":
                    await _repository.AddOwner(
                        AddOwnerForm.Username,
                        AddOwnerForm.FirstName,
                        AddOwnerForm.LastName,
                        AddOwnerForm.Address,
                        AddOwnerForm.Birthdate
                    );
                    break;

                case "AddEmployee":
                    await _repository.AddEmployee(
                        AddEmployeeForm.Username,
                        AddEmployeeForm.FirstName,
                        AddEmployeeForm.LastName,
                        AddEmployeeForm.Address,
                        AddEmployeeForm.Birthdate,
                        AddEmployeeForm.TaxID,
                        AddEmployeeForm.HiredDate,
                        AddEmployeeForm.Experience,
                        AddEmployeeForm.Salary
                    );
                    break;

                case "AddDriverRole":
                    await _repository.AddDriverRole(
                        AddDriverRoleForm.Username,
                        AddDriverRoleForm.LicenseID,
                        AddDriverRoleForm.LicenseType,
                        AddDriverRoleForm.DriverExperience
                    );
                    break;

                case "AddProduct":
                    await _repository.AddProduct(
                        AddProductForm.Barcode,
                        AddProductForm.Name,
                        AddProductForm.Weight
                    );
                    break;

                case "AddVan":
                    await _repository.AddVan(
                        AddVanForm.VanID,
                        AddVanForm.Tag,
                        AddVanForm.Fuel,
                        AddVanForm.Capacity,
                        AddVanForm.Sales,
                        AddVanForm.DrivenBy
                    );
                    break;

                case "AddBusiness":
                    await _repository.AddBusiness(
                        AddBusinessForm.LongName,
                        AddBusinessForm.Rating,
                        AddBusinessForm.Spent,
                        AddBusinessForm.Location
                    );
                    break;

                // Add more cases for other forms...

                default:
                    return BadRequest("Invalid form submission.");
            }

            // Redirect to refresh or show success message
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            return Page();
        }
    }
}

// Models for each form
public class AddOwnerModel
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime Birthdate { get; set; }
}

public class AddEmployeeModel
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime Birthdate { get; set; }
    public string TaxID { get; set; }
    public DateTime HiredDate { get; set; }
    public int Experience { get; set; }
    public int Salary { get; set; }
}

public class AddDriverRoleModel
{
    public string Username { get; set; }
    public string LicenseID { get; set; }
    public string LicenseType { get; set; }
    public int DriverExperience { get; set; }
}

public class AddProductModel
{
    public string Barcode { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
}

public class AddVanModel
{
    public string VanID { get; set; }
    public int Tag { get; set; }
    public int Fuel { get; set; }
    public int Capacity { get; set; }
    public int Sales { get; set; }
    public string DrivenBy { get; set; }
}

public class AddBusinessModel
{
    public string LongName { get; set; }
    public int Rating { get; set; }
    public int Spent { get; set; }
    public string Location { get; set; }
}
