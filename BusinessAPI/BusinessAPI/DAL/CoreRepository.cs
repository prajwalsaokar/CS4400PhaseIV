using Dapper;
using System;
using System.Data;
using BusinessAPI.Config;
using BusinessAPI.DAL.Models;
using BusinessAPI.DAL.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace BusinessAPI.DAL;
public class CoreRepository
{
    private readonly string _connectionString;

    public CoreRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }
    public async Task AddOwner(string username, string firstName, string lastName, string address, DateTime birthdate)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_first_name = firstName,
                ip_lastt_name = lastName,
                ip_address = address,
                ip_birthdate = birthdate
            };
            await connection.ExecuteAsync("add_owner", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task AddEmployee(string username, string firstName, string lastName, string address, DateTime birthdate, string taxID, DateTime hired, int experience, int salary)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_first_name = firstName,
                ip_last_name = lastName,
                ip_address = address,
                ip_birthdate = birthdate,
                ip_taxID = taxID,
                ip_hired = hired,
                ip_employee_experience = experience,
                ip_salary = salary
            };
            await connection.ExecuteAsync("add_employee", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task AddDriverRole(string username, string licenseID, string licenseType, int driverExperience)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_license_id = licenseID,
                ip_license_type = licenseType,
                ip_driver_experience = driverExperience
            };
            await connection.ExecuteAsync("add_driver_role", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    // Add Worker Role
    public async Task AddWorkerRole(string username)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username
            };
            await connection.ExecuteAsync("add_worker_role", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddProduct(string barcode, string iname, int weight)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_barcode = barcode,
                ip_iname = iname,
                ip_weight = weight
            };
            await connection.ExecuteAsync("add_product", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddVan(string id, int tag, int fuel, int capacity, int sales, string drivenBy)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_id = id,
                ip_tag = tag,
                ip_fuel = fuel,
                ip_capacity = capacity,
                ip_sales = sales,
                ip_driven_by = drivenBy
            };  
            await connection.ExecuteAsync("add_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddBusiness(string longName, int rating, int spent, string location)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_long_name = longName,
                ip_rating = rating,
                ip_spent = spent,
                ip_location = location
            };
            await connection.ExecuteAsync("add_business", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddService(string id, string longName, string homeBase, string manager)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_id = id,
                ip_long_name = longName,
                ip_home_base = homeBase,
                ip_manager = manager
            };
            await connection.ExecuteAsync("add_service", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddLocation([FromForm] string label, int xCoord, int yCoord, int? space)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_label = label,
                ip_x_coord = xCoord,
                ip_y_coord = yCoord,
                ip_space = space
            };
            await connection.ExecuteAsync("add_location", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task StartFunding(string username, int invested, string business, DateTime investedDate)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_invested = invested,
                ip_business = business,
                ip_invested_date = investedDate

            };
            await connection.ExecuteAsync("start_funding", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task HireEmployee(string username, string id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_id = id
            };
            await connection.ExecuteAsync("hire_employee", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task FireEmployee(string username, string id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_id = id
            };
            await connection.ExecuteAsync("fire_employee", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task ManageService(string username, string id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_id = id
            };
            await connection.ExecuteAsync("manage_service", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task TakeOverVan(string username, string id, int tag)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username,
                ip_id = id,
                ip_tag = tag
            };
            await connection.ExecuteAsync("takeover_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task LoadVan(string id, int tag, string barcode, int quantity, int price)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_id = id,
                ip_tag = tag,
                ip_barcode = barcode,
                ip_quantity = quantity,
                ip_price = price
            };
            await connection.ExecuteAsync("load_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task RefuelVan(string id, int tag, int fuelAmount)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_id = id,
                ip_tag = tag,
                ip_fuel_amount = fuelAmount
            };
            await connection.ExecuteAsync("refuel_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DriveVan(string id, int tag, string destination)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_id = id,
                ip_tag = tag,
                ip_destination = destination
            };
            await connection.ExecuteAsync("drive_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task PurchaseProduct(string longName, string id, int tag, string barcode, int quantity)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_long_name = longName,
                ip_id = id,
                ip_tag = tag,
                ip_barcode = barcode,
                ip_quantity = quantity
            };
            await connection.ExecuteAsync("purchase_product", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task RemoveProduct(string barcode)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_barcode = barcode
            };
            await connection.ExecuteAsync("remove_product", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task RemoveVan(string id, int tag)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_id = id,
                ip_tag = tag
            };
            await connection.ExecuteAsync("remove_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task RemoveDriverRole(string username)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                ip_username = username
            };
            await connection.ExecuteAsync("remove_driver_role", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<List<OwnerView>> GetOwnerView()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string query = "SELECT * FROM display_owner_view";
            return (await connection.QueryAsync<OwnerView>(query)).ToList();
        }
    }

    public async Task<List<EmployeeView>> GetEmployeeView()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string query = "SELECT * FROM display_employee_view";
            return (await connection.QueryAsync<EmployeeView>(query)).ToList();
        }
    }

    public async Task<List<DriverView>> GetDriverView()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string query = "SELECT * FROM display_driver_view";
            return (await connection.QueryAsync<DriverView>(query)).ToList();
        }
    }

    public async Task<List<LocationView>> GetLocationView()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string query = "SELECT * FROM display_location_view";
            return (await connection.QueryAsync<LocationView>(query)).ToList();
        }
    }

    public async Task<List<ProductView>> GetProductView()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string query = "SELECT * FROM display_product_view";
            return (await connection.QueryAsync<ProductView>(query)).ToList();
        }
    }

    public async Task<List<ServiceView>> GetServiceView()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string query = "SELECT * FROM display_service_view";
            return (await connection.QueryAsync<ServiceView>(query)).ToList();
        }
    }   
}