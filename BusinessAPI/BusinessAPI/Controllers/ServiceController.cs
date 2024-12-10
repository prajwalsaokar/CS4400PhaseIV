using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessAPI.DAL;
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
        public async Task<IActionResult> AddOwner(string username, string firstName, string lastName, string address, DateTime birthdate)
        {
            await _coreRepository.AddOwner(username, firstName, lastName, address, birthdate);
            return Ok("Owner added successfully.");
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee(string username, string firstName, string lastName, string address, DateTime birthdate, string taxID, DateTime hired, int experience, int salary)
        {
            await _coreRepository.AddEmployee(username, firstName, lastName, address, birthdate, taxID, hired, experience, salary);
            return Ok("Employee added successfully.");
        }

        [HttpPost("add-driver-role")]
        public async Task<IActionResult> AddDriverRole(string username, string licenseID, string licenseType, int driverExperience)
        {
            await _coreRepository.AddDriverRole(username, licenseID, licenseType, driverExperience);
            return Ok("Driver role added successfully.");
        }

        [HttpPost("add-worker-role")]
        public async Task<IActionResult> AddWorkerRole(string username)
        {
            await _coreRepository.AddWorkerRole(username);
            return Ok("Worker role added successfully.");
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(string barcode, string iname, int weight)
        {
            await _coreRepository.AddProduct(barcode, iname, weight);
            return Ok("Product added successfully.");
        }

        [HttpPost("add-van")]
        public async Task<IActionResult> AddVan(string id, int tag, int fuel, int capacity, int sales, string drivenBy)
        {
            await _coreRepository.AddVan(id, tag, fuel, capacity, sales, drivenBy);
            return Ok("Van added successfully.");
        }

        [HttpPost("add-business")]
        public async Task<IActionResult> AddBusiness(string longName, int rating, int spent, string location)
        {
            await _coreRepository.AddBusiness(longName, rating, spent, location);
            return Ok("Business added successfully.");
        }

        [HttpPost("add-service")]
        public async Task<IActionResult> AddService(string id, string longName, string homeBase, string manager)
        {
            await _coreRepository.AddService(id, longName, homeBase, manager);
            return Ok("Service added successfully.");
        }

        [HttpPost("add-location")]
        public async Task<IActionResult> AddLocation(string label, int xCoord, int yCoord, int? space)
        {
            await _coreRepository.AddLocation(label, xCoord, yCoord, space);
            return Ok("Location added successfully.");
        }

        [HttpPost("start-funding")]
        public async Task<IActionResult> StartFunding(string username, int invested, string business, DateTime investedDate)
        {
            await _coreRepository.StartFunding(username, invested, business, investedDate);
            return Ok("Funding started successfully.");
        }

        [HttpPost("hire-employee")]
        public async Task<IActionResult> HireEmployee(string username, string id)
        {
            await _coreRepository.HireEmployee(username, id);
            return Ok("Employee hired successfully.");
        }

        [HttpPost("fire-employee")]
        public async Task<IActionResult> FireEmployee(string username, string id)
        {
            await _coreRepository.FireEmployee(username, id);
            return Ok("Employee fired successfully.");
        }

        [HttpPost("manage-service")]
        public async Task<IActionResult> ManageService(string username, string id)
        {
            await _coreRepository.ManageService(username, id);
            return Ok("Service managed successfully.");
        }

        [HttpPost("takeover-van")]
        public async Task<IActionResult> TakeOverVan(string username, string id, int tag)
        {
            await _coreRepository.TakeOverVan(username, id, tag);
            return Ok("Van takeover successful.");
        }

        [HttpPost("load-van")]
        public async Task<IActionResult> LoadVan(string id, int tag, string barcode, int quantity, int price)
        {
            await _coreRepository.LoadVan(id, tag, barcode, quantity, price);
            return Ok("Van loaded successfully.");
        }

        [HttpPost("refuel-van")]
        public async Task<IActionResult> RefuelVan(string id, int tag, int fuelAmount)
        {
            await _coreRepository.RefuelVan(id, tag, fuelAmount);
            return Ok("Van refueled successfully.");
        }

        [HttpPost("drive-van")]
        public async Task<IActionResult> DriveVan(string id, int tag, string destination)
        {
            await _coreRepository.DriveVan(id, tag, destination);
            return Ok("Van driven to destination.");
        }

        [HttpPost("purchase-product")]
        public async Task<IActionResult> PurchaseProduct(string longName, string id, int tag, string barcode, int quantity)
        {
            await _coreRepository.PurchaseProduct(longName, id, tag, barcode, quantity);
            return Ok("Product purchased successfully.");
        }

        [HttpDelete("remove-product")]
        public async Task<IActionResult> RemoveProduct(string barcode)
        {
            await _coreRepository.RemoveProduct(barcode);
            return Ok("Product removed successfully.");
        }

        [HttpDelete("remove-van")]
        public async Task<IActionResult> RemoveVan(string id, int tag)
        {
            await _coreRepository.RemoveVan(id, tag);
            return Ok("Van removed successfully.");
        }

        [HttpDelete("remove-driver-role")]
        public async Task<IActionResult> RemoveDriverRole(string username)
        {
            await _coreRepository.RemoveDriverRole(username);
            return Ok("Driver role removed successfully.");
        }

        [HttpGet("get-owner-view")]
        public async Task<IActionResult> GetOwnerView()
        {
            var result = await _coreRepository.GetOwnerView();
            return Ok(result);
        }

        [HttpGet("get-employee-view")]
        public async Task<IActionResult> GetEmployeeView()
        {
            var result = await _coreRepository.GetEmployeeView();
            return Ok(result);
        }

        [HttpGet("get-driver-view")]
        public async Task<IActionResult> GetDriverView()
        {
            var result = await _coreRepository.GetDriverView();
            return Ok(result);
        }

        [HttpGet("get-location-view")]
        public async Task<IActionResult> GetLocationView()
        {
            var result = await _coreRepository.GetLocationView();
            return Ok(result);
        }

        [HttpGet("get-product-view")]
        public async Task<IActionResult> GetProductView()
        {
            var result = await _coreRepository.GetProductView();
            return Ok(result);
        }

        [HttpGet("get-service-view")]
        public async Task<IActionResult> GetServiceView()
        {
            var result = await _coreRepository.GetServiceView();
            return Ok(result);
        }
    }
}
