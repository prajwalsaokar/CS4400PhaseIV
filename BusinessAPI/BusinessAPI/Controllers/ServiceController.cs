using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessAPI.DAL;
using BusinessAPI.DAL.Models;
using BusinessAPI.DAL.Models.Views;

namespace BusinessAPI.Controllers
{

    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly CoreRepository _coreRepository;

        public BusinessController(CoreRepository coreRepository)
        {
            _coreRepository = coreRepository;
        }

        [HttpPost("add-owner")]
        public async Task<IActionResult> AddOwner([FromForm] AddOwnerModel request)
        {
            await _coreRepository.AddOwner(request.username, request.first_name, request.last_name, request.address,
                request.birthdate);
            return Ok("Owner added successfully.");
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeModel request)
        {
            await _coreRepository.AddEmployee(request.username, request.first_name, request.last_name, request.address,
                request.birthdate,
                request.tax_id, request.hired_date, request.experience, request.salary);
            return Ok("Employee added successfully.");
        }

        [HttpPost("add-driver-role")]
        public async Task<IActionResult> AddDriverRole([FromForm] AddDriverRoleModel request)
        {
            await _coreRepository.AddDriverRole(request.username, request.license_id, request.license_type,
                request.driver_experience);
            return Ok("Driver role added successfully.");
        }

        [HttpPost("add-worker-role")]
        public async Task<IActionResult> AddWorkerRole([FromForm] string username)
        {
            await _coreRepository.AddWorkerRole(username);
            return Ok("Worker role added successfully.");
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductModel request)
        {
            await _coreRepository.AddProduct(request.barcode, request.name, request.weight);
            return Ok("Product added successfully.");
        }

        [HttpPost("add-van")]
        public async Task<IActionResult> AddVan([FromForm] AddVanModel request)
        {
            await _coreRepository.AddVan(request.van_id, request.tag, request.fuel, request.capacity, request.sales,
                request.driven_by);
            return Ok("Van added successfully.");
        }

        [HttpPost("add-business")]
        public async Task<IActionResult> AddBusiness([FromForm] AddBusinessModel request)
        {
            await _coreRepository.AddBusiness(request.long_name, request.rating, request.spent, request.location);
            return Ok("Business added successfully.");
        }
        [HttpPost("add-service")]
        public async Task<IActionResult> AddService([FromForm] AddServiceModel request)
        {
            await _coreRepository.AddService(request.id, request.long_name, request.home_base, request.manager);
            return Ok("Business added successfully.");
        }


        [HttpPost("add-location")]
        public async Task<IActionResult> AddLocation([FromForm] AddLocationModel request)
        {
            await _coreRepository.AddLocation(request.label, request.xCoord, request.yCoord, request.space);
            return Ok("Location added successfully.");
        }

        [HttpPost("start-funding")]
        public async Task<IActionResult> StartFunding([FromForm] StartFundingModel request)
        {
            await _coreRepository.StartFunding(request.username, request.invested, request.business,
                request.invested_date);
            return Ok("Funding started successfully.");
        }

        [HttpPost("hire-employee")]
        public async Task<IActionResult> HireEmployee([FromForm] HireEmployeeModel request)
        {
            await _coreRepository.HireEmployee(request.username, request.id);
            return Ok("Employee hired successfully.");
        }

        [HttpPost("fire-employee")]
        public async Task<IActionResult> FireEmployee([FromForm] FireEmployeeModel request)
        {
            await _coreRepository.FireEmployee(request.username, request.id);
            return Ok("Employee fired successfully.");
        }

        [HttpPost("manage-service")]
        public async Task<IActionResult> ManageService([FromForm] ManageServiceModel request)
        {
            await _coreRepository.ManageService(request.username, request.id);
            return Ok("Service managed successfully.");
        }

        [HttpPost("takeover-van")]
        public async Task<IActionResult> TakeOverVan([FromForm] TakeOverVanModel request)
        {
            await _coreRepository.TakeOverVan(request.username, request.van_id, request.tag);
            return Ok("Van takeover successful.");
        }

        [HttpPost("load-van")]
        public async Task<IActionResult> LoadVan([FromForm] LoadVanModel request)
        {
            await _coreRepository.LoadVan(request.van_id, request.tag, request.barcode, request.quantity,
                request.price);
            return Ok("Van loaded successfully.");
        }

        [HttpPost("refuel-van")]
        public async Task<IActionResult> RefuelVan([FromForm] RefuelVanModel request)
        {
            await _coreRepository.RefuelVan(request.van_id, request.tag, request.fuel_amount);
            return Ok("Van refueled successfully.");
        }

        [HttpPost("drive-van")]
        public async Task<IActionResult> DriveVan([FromForm] DriveVanModel request)
        {
            await _coreRepository.DriveVan(request.van_id, request.tag, request.destination);
            return Ok("Van driven to destination.");
        }

        [HttpPost("purchase-product")]
        public async Task<IActionResult> PurchaseProduct([FromForm] PurchaseProductModel request)
        {
            await _coreRepository.PurchaseProduct(request.long_name, request.id, request.tag, request.barcode,
                request.quantity);
            return Ok("Product purchased successfully.");
        }

        [HttpPost("remove-product")]
        public async Task<IActionResult> RemoveProduct([FromForm] RemoveProductModel request)
        {
            await _coreRepository.RemoveProduct(request.barcode);
            return Ok("Product removed successfully.");
        }

        [HttpPost("remove-van")]
        public async Task<IActionResult> RemoveVan([FromForm] RemoveVanModel request)
        {
            await _coreRepository.RemoveVan(request.van_id, request.tag);
            return Ok("Van removed successfully.");
        }

        [HttpPost("remove-driver-role")]
        public async Task<IActionResult> RemoveDriverRole([FromForm] RemoveDriverRoleModel request)
        {
            await _coreRepository.RemoveDriverRole(request.username);
            return Ok("Driver role removed successfully.");
        }
    }

}
