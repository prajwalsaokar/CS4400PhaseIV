using Dapper;
using System;
using System.Data;
using BusinessAPI.Config;
using BusinessAPI.DAL.Models;
using BusinessAPI.DAL.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace BusinessAPI.DAL
{
    public class CoreRepository
    {
        private readonly string _connectionString;

        public CoreRepository(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        private void HandleMySqlException(MySqlException ex)
        {
            if (ex.SqlState == "45000")
            {
                Console.WriteLine("Database error");
                throw new InvalidOperationException("A custom database error occurred.", ex);
            }
        }

        public async Task  AddOwner(string username, string firstName, string lastName, string address, DateTime birthdate)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var parameters = new
                    {
                        ip_username = username,
                        ip_first_name = firstName,
                        ip_last_name = lastName,
                        ip_address = address,
                        ip_birthdate = birthdate
                    };
                    await connection.ExecuteAsync("add_owner", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddEmployee(string username, string firstName, string lastName, string address, DateTime birthdate, string taxID, DateTime hired, int experience, int salary)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddDriverRole(string username, string licenseID, string licenseType, int driverExperience)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddWorkerRole(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var parameters = new { ip_username = username };
                    await connection.ExecuteAsync("add_worker_role", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddProduct(string barcode, string iname, int weight)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddVan(string id, int tag, int fuel, int capacity, int sales, string drivenBy)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddBusiness(string longName, int rating, int spent, string location)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddService(string id, string longName, string homeBase, string manager)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task AddLocation( string label, int xCoord, int yCoord, int? space)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task StartFunding(string username, int invested, string business, DateTime investedDate)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task HireEmployee(string username, string id)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task FireEmployee(string username, string id)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task ManageService(string username, string id)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task TakeOverVan(string username, string id, int tag)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task LoadVan(string id, int tag, string barcode, int quantity, int price)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task RefuelVan(string id, int tag, int fuelAmount)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task DriveVan(string id, int tag, string destination)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task PurchaseProduct(string longName, string id, int tag, string barcode, int quantity)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task RemoveProduct(string barcode)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var parameters = new { ip_barcode = barcode };
                    await connection.ExecuteAsync("remove_product", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task RemoveVan(string id, int tag)
        {
            try
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
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task RemoveDriverRole(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var parameters = new { ip_username = username };
                    await connection.ExecuteAsync("remove_driver_role", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
            }
        }

        public async Task<List<OwnerView>> GetOwnerView()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM display_owner_view";
                    return (await connection.QueryAsync<OwnerView>(query)).ToList();
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
                return null; // Optional: Return null or handle based on your app's needs
            }
        }

        public async Task<List<EmployeeView>> GetEmployeeView()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM display_employee_view";
                    return (await connection.QueryAsync<EmployeeView>(query)).ToList();
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
                return null;
            }
        }

        public async Task<List<DriverView>> GetDriverView()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM display_driver_view";
                    return (await connection.QueryAsync<DriverView>(query)).ToList();
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
                return null;
            }
        }

        public async Task<List<LocationView>> GetLocationView()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM display_location_view";
                    return (await connection.QueryAsync<LocationView>(query)).ToList();
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
                return null;
            }
        }

        public async Task<List<ProductView>> GetProductView()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM display_product_view";
                    return (await connection.QueryAsync<ProductView>(query)).ToList();
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
                return null;
            }
        }

        public async Task<List<ServiceView>> GetServiceView()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM display_service_view";
                    return (await connection.QueryAsync<ServiceView>(query)).ToList();
                }
            }
            catch (MySqlException ex)
            {
                HandleMySqlException(ex);
                return null;
            }
        }
    }
}
