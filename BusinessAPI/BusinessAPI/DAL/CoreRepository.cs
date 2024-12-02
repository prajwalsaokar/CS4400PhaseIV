using Dapper;
using System;
using System.Data;
using BusinessAPI.Config;
using BusinessAPI.DAL.Models;
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
    public async Task<IEnumerable<User>> GetUsers()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            return await connection.QueryAsync<User>("GetUsers", commandType: CommandType.StoredProcedure);
        }
    }
    public async Task AddOwner(string username)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new { Username = username };
            await connection.ExecuteAsync("add_owner", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddEmployee(string username, string taxID, DateTime hired, int experience, int salary)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Username = username,
                TaxID = taxID,
                Hired = hired,
                Experience = experience,
                Salary = salary
            };
            await connection.ExecuteAsync("add_employee", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddDriverRole(string username, string licenseID, string licenseType, int successfulTrips)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Username = username,
                LicenseID = licenseID,
                LicenseType = licenseType,
                SuccessfulTrips = successfulTrips
            };
            await connection.ExecuteAsync("add_driver_role", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    // Add Worker Role
    public async Task AddWorkerRole(string username)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new { Username = username };
            await connection.ExecuteAsync("add_worker_role", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddProduct(string barcode, string iname, int weight)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Barcode = barcode,
                Iname = iname,
                Weight = weight
            };
            await connection.ExecuteAsync("add_product", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddVan(string id, int tag, int fuel, int capacity, int sales, string drivenBy, string locatedAt)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Id = id,
                Tag = tag,
                Fuel = fuel,
                Capacity = capacity,
                Sales = sales,
                DrivenBy = drivenBy,
                LocatedAt = locatedAt
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
                LongName = longName,
                Rating = rating,
                Spent = spent,
                Location = location
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
                Id = id,
                LongName = longName,
                HomeBase = homeBase,
                Manager = manager
            };
            await connection.ExecuteAsync("add_service", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task AddLocation(string label, int xCoord, int yCoord, int? space)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Label = label,
                XCoord = xCoord,
                YCoord = yCoord,
                Space = space
            };
            await connection.ExecuteAsync("add_location", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task StartFunding(string username, int invested, DateTime investedDate, string business)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Username = username,
                Invested = invested,
                InvestedDate = investedDate,
                Business = business
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
                Username = username,
                Id = id
            };
            await connection.ExecuteAsync("hire_employee", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task FireEmployee(string username)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new { Username = username };
            await connection.ExecuteAsync("fire_employee", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task ManageService(string id, string manager)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Id = id,
                Manager = manager
            };
            await connection.ExecuteAsync("manage_service", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task TakeOverVan(string id, int tag, string username)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Id = id,
                Tag = tag,
                Username = username
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
                Id = id,
                Tag = tag,
                Barcode = barcode,
                Quantity = quantity,
                Price = price
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
                Id = id,
                Tag = tag,
                FuelAmount = fuelAmount
            };
            await connection.ExecuteAsync("refuel_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DriveVan(string id, int tag, string username, int distance)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Id = id,
                Tag = tag,
                Username = username,
                Distance = distance
            };
            await connection.ExecuteAsync("drive_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task PurchaseProduct(string barcode, int quantity, int totalPrice)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Barcode = barcode,
                Quantity = quantity,
                TotalPrice = totalPrice
            };
            await connection.ExecuteAsync("purchase_product", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task RemoveProduct(string barcode)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new { Barcode = barcode };
            await connection.ExecuteAsync("remove_product", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task RemoveVan(string id, int tag)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new
            {
                Id = id,
                Tag = tag
            };
            await connection.ExecuteAsync("remove_van", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task RemoveDriverRole(string username)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var parameters = new { Username = username };
            await connection.ExecuteAsync("remove_driver_role", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}