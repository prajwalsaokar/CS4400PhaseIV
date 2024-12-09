// using Microsoft.AspNetCore.Mvc;
//
// namespace BusinessAPI.Controllers;
// using BusinessAPI.DAL;
// using BusinessAPI.DAL.Models;
// using BusinessAPI.DAL.Models.Views;
// [ApiController]
// [Route("api/v1")]
//
// public class ServiceController: ControllerBase
// {
//     private readonly CoreRepository _repository;
//     public ServiceController(CoreRepository repository)
//     {
//         _repository = repository;
//     }
//
//         [HttpPost("add-owner")]
//         public async Task<IActionResult> AddOwner([FromBody] string username)
//         {
//             await _repository.AddOwner(username);
//             return Ok(new { Message = "Owner added successfully." });
//         }
//
//         [HttpPost("add-employee")]
//         public async Task<IActionResult> AddEmployee([FromBody] Employee request)
//         {
//             await _repository.AddEmployee(request.Username, request.TaxID, request.Hired, request.Experience, request.Salary);
//             return Ok(new { Message = "Employee added successfully." });
//         }
//
//         [HttpPost("add-driver")]
//         public async Task<IActionResult> AddDriverRole([FromBody] Driver request)
//         {
//             await _repository.AddDriverRole(request.Username, request.LicenseID, request.LicenseType, request.SuccessfulTrips);
//             return Ok(new { Message = "Driver role added successfully." });
//         }
//
//         [HttpPost("add-product")]
//         public async Task<IActionResult> AddProduct([FromBody] Product request)
//         {
//             await _repository.AddProduct(request.Barcode, request.Iname, request.Weight);
//             return Ok(new { Message = "Product added successfully." });
//         }
//
//         [HttpPost("add-van")]
//         public async Task<IActionResult> AddVan([FromBody] Van request)
//         {
//             await _repository.AddVan(
//                     request.Id,
//                     request.Tag,
//                     request.Fuel,
//                     request.Capacity,
//                     request.Sales,
//                     request.DrivenBy
//                 );
//             return Ok(new { Message = "Van added successfully." });
//         }
//
//         [HttpPost("add-business")]
//         public async Task<IActionResult> AddBusiness([FromBody] Business request)
//         {
//             await _repository.AddBusiness(request.LongName, request.Rating, request.Spent, request.Location);
//             return Ok(new { Message = "Business added successfully." });
//         }
//         [HttpPost("add-delivery")]
//         public async Task<IActionResult> AddDeliveryService([FromBody] DeliveryService request)
//         {
//             await _repository.AddService(request.Id, request.LongName, request.HomeBase, request.Manager);
//             return Ok(new { Message = "Business added successfully." });
//         }
//         [HttpPost("add-location")]
//
//         public async Task<IActionResult> AddLocation([FromBody] Location request)
//         {
//             await _repository.AddLocation(request.Label, request.XCoord, request.YCoord, request.Space);
//             return Ok("Location added successfully.");
//         }
//         [HttpPost("start-funding")]
//         public async Task<IActionResult> StartFunding([FromBody] Fund request)
//         {
//             await _repository.StartFunding(request.Username, request.Invested, request.InvestedDate, request.Business);
//             return Ok("Funding started successfully.");
//         }
//         [HttpGet("hire-employee/{username}/{id}")]
//         public async Task<IActionResult> HireEmployee(string username, string id)
//         {
//             await _repository.HireEmployee(username, id);
//             return Ok("Employee hired successfully.");
//         }
//         [HttpGet("fire-employee/{username}")]
//         public async Task<IActionResult> FireEmployee(string username)
//         {
//             await _repository.FireEmployee(username);
//             return Ok("Employee fired successfully.");
//         }
//
//         [HttpGet("manage-service/{username}/{id}")]
//         public async Task<IActionResult> ManageService(string username, string id)
//         {
//             await _repository.ManageService(id, username);
//             return Ok("Service managed successfully.");
//         }
//
//         [HttpGet("takeover-van/{id}/{tag}/{username}")]
//         public async Task<IActionResult> TakeOverVan(string id, int tag, string username)
//         {
//             await _repository.TakeOverVan(id, tag, username);
//             return Ok("Van taken over successfully.");
//         }
//         [HttpPost("load-van")]
//         public async Task<IActionResult> LoadVan([FromBody] Contain request)
//         {
//             await _repository.LoadVan(request.Id, request.Tag, request.Barcode, request.Quantity, request.Price);
//             return Ok("Van loaded successfully.");
//         }
//         [HttpGet("refuel-van/{id}/{tag}/{fuelAmount}")]
//         public async Task<IActionResult> RefuelVan(string id, int tag, int fuelAmount)
//         {
//             await _repository.RefuelVan(id, tag, fuelAmount);
//             return Ok("Van refueled successfully.");
//         }
//         [HttpGet("drive-van/{id}/{tag}/{username}/{distance}")]
//         public async Task<IActionResult> DriveVan(string id, int tag, string username, int distance)
//         {
//             await _repository.DriveVan(id, tag, username, distance);
//             return Ok("Van driven successfully.");
//         }
//
//         [HttpGet("purchase-product/{barcode}/{quantity}/{totalPrice}")]
//         public async Task<IActionResult> PurchaseProduct(string barcode, int quantity, int totalPrice)
//         {
//             await _repository.PurchaseProduct(barcode, quantity, totalPrice);
//             return Ok("Product purchased successfully.");
//         }
//
//         [HttpGet("owner-view")]
//         public async Task<IActionResult> GetOwnerView()
//         {
//             var owners = await _repository.GetOwnerView();
//             return Ok(owners);
//         }
//
//         [HttpGet("employee-view")]
//         public async Task<IActionResult> GetEmployeeView()
//         {
//             var employees = await _repository.GetEmployeeView();
//             return Ok(employees);
//         }
//
//         [HttpGet("driver-view")]
//         public async Task<IActionResult> GetDriverView()
//         {
//             var drivers = await _repository.GetDriverView();
//             return Ok(drivers);
//         }
//
//         [HttpGet("location-view")]
//         public async Task<IActionResult> GetLocationView()
//         {
//             var locations = await _repository.GetLocationView();
//             return Ok(locations);
//         }
//
//         [HttpGet("product-view")]
//         public async Task<IActionResult> GetProductView()
//         {
//             var products = await _repository.GetProductView();
//             return Ok(products);
//         }
//
//         [HttpGet("service-view")]
//         public async Task<IActionResult> GetServiceView()
//         {
//             var services = await _repository.GetServiceView();
//             return Ok(services);
//         }
//         [HttpDelete("remove-product/{barcode}")]
//         public async Task<IActionResult> RemoveProduct(string barcode)
//         {
//             await _repository.RemoveProduct(barcode);
//             return Ok("Product removed successfully.");
//         }
//         [HttpDelete("remove-van/{id}/{tag}")]
//         public async Task<IActionResult> RemoveVan(string id, int tag)
//         {
//             await _repository.RemoveVan(id, tag);
//             return Ok("Van removed successfully.");
//         }
//         [HttpDelete("remove-driver-role/{username}")]
//         public async Task<IActionResult> RemoveDriverRole(string username)
//         {
//             await _repository.RemoveDriverRole(username);
//             return Ok("Driver role removed successfully.");
//         }
// }