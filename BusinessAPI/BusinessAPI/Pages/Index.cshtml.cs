using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using BusinessAPI.DAL;
namespace BusinessAPI.Pages;

public class IndexModel : PageModel
{
    private readonly CoreRepository _repository;

    public IndexModel(CoreRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public string FormId { get; set; }

    // Models for each form
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
    [BindProperty]
    public HireEmployeeModel HireEmployeeForm { get; set; }
    [BindProperty]
    public FireEmployeeModel FireEmployeeForm { get; set; }
    [BindProperty]
    public ManageServiceModel ManageServiceForm { get; set; }
    [BindProperty]
    public TakeOverVanModel TakeOverVanForm { get; set; }
    [BindProperty]
    public LoadVanModel LoadVanForm { get; set; }
    [BindProperty]
    public RefuelVanModel RefuelVanForm { get; set; }
    [BindProperty]
    public DriveVanModel DriveVanForm { get; set; }
    [BindProperty]
    public PurchaseProductModel PurchaseProductForm { get; set; }
    [BindProperty]
    public RemoveProductModel RemoveProductForm { get; set; }
    [BindProperty]
    public RemoveVanModel RemoveVanForm { get; set; }
    [BindProperty]
    public RemoveDriverRoleModel RemoveDriverRoleForm { get; set; }
    
    [BindProperty]
    public StartFundingModel StartFundingForm { get; set; } 
    [BindProperty]
    public AddLocationModel AddLocationForm { get; set; }  

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

                case "HireEmployee":
                    await _repository.HireEmployee(
                        HireEmployeeForm.Username,
                        HireEmployeeForm.ID
                    );
                    break;

                case "FireEmployee":
                    await _repository.FireEmployee(
                        FireEmployeeForm.Username,
                        FireEmployeeForm.ID
                    );
                    break;

                case "ManageService":
                    await _repository.ManageService(
                        ManageServiceForm.Username,
                        ManageServiceForm.ID
                    );
                    break;

                case "TakeOverVan":
                    await _repository.TakeOverVan(
                        TakeOverVanForm.Username,
                        TakeOverVanForm.VanID,
                        TakeOverVanForm.Tag
                    );
                    break;

                case "LoadVan":
                    await _repository.LoadVan(
                        LoadVanForm.VanID,
                        LoadVanForm.Tag,
                        LoadVanForm.Barcode,
                        LoadVanForm.Quantity,
                        LoadVanForm.Price
                    );
                    break;

                case "RefuelVan":
                    await _repository.RefuelVan(
                        RefuelVanForm.VanID,
                        RefuelVanForm.Tag,
                        RefuelVanForm.FuelAmount
                    );
                    break;

                case "DriveVan":
                    await _repository.DriveVan(
                        DriveVanForm.VanID,
                        DriveVanForm.Tag,
                        DriveVanForm.Destination
                    );
                    break;

                case "PurchaseProduct":
                    await _repository.PurchaseProduct(
                        PurchaseProductForm.LongName,
                        PurchaseProductForm.ID,
                        PurchaseProductForm.Tag,
                        PurchaseProductForm.Barcode,
                        PurchaseProductForm.Quantity
                    );
                    break;
                case "StartFunding":  
                    await _repository.StartFunding(
                        StartFundingForm.Username,
                        StartFundingForm.Invested,
                        StartFundingForm.Business,
                        StartFundingForm.InvestedDate
                    );
                    break;

                case "AddLocation": // Added AddLocation logic
                    await _repository.AddLocation(
                        AddLocationForm.Label,
                        AddLocationForm.XCoord,
                        AddLocationForm.YCoord,
                        AddLocationForm.Space
                    );
                    break;
                case "RemoveProduct":
                    await _repository.RemoveProduct(
                        RemoveProductForm.Barcode
                    );
                    break;

                case "RemoveVan":
                    await _repository.RemoveVan(
                        RemoveVanForm.VanID,
                        RemoveVanForm.Tag
                    );
                    break;

                case "RemoveDriverRole":
                    await _repository.RemoveDriverRole(
                        RemoveDriverRoleForm.Username
                    );
                    break;

                default:
                    return BadRequest("Invalid form submission.");
            }

            // Redirect to refresh the page or show success message
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            return Page();
        }
    }
}

// Individual form models
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

public class HireEmployeeModel
{
    public string Username { get; set; }
    public string ID { get; set; }
}

public class FireEmployeeModel
{
    public string Username { get; set; }
    public string ID { get; set; }
}

public class ManageServiceModel
{
    public string Username { get; set; }
    public string ID { get; set; }
}

public class TakeOverVanModel
{
    public string Username { get; set; }
    public string VanID { get; set; }
    public int Tag { get; set; }
}

public class LoadVanModel
{
    public string VanID { get; set; }
    public int Tag { get; set; }
    public string Barcode { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}

public class RefuelVanModel
{
    public string VanID { get; set; }
    public int Tag { get; set; }
    public int FuelAmount { get; set; }
}

public class DriveVanModel
{
    public string VanID { get; set; }
    public int Tag { get; set; }
    public string Destination { get; set; }
}

public class PurchaseProductModel
{
    public string LongName { get; set; }
    public string ID { get; set; }
    public int Tag { get; set; }
    public string Barcode { get; set; }
    public int Quantity { get; set; }
}

public class RemoveProductModel
{
    public string Barcode { get; set; }
}

public class RemoveVanModel
{
    public string VanID { get; set; }
    public int Tag { get; set; }
}

public class RemoveDriverRoleModel
{
    public string Username { get; set; }
}

public class StartFundingModel
{
    public string Username { get; set; }
    public int Invested { get; set; }
    public string Business { get; set; }
    public DateTime InvestedDate { get; set; }
}

public class AddLocationModel
{
    public string Label { get; set; }
    public int XCoord { get; set; }
    public int YCoord { get; set; }
    public int Space { get; set; }
}

