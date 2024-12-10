namespace BusinessAPI.DAL.Models
{
    public class AddOwnerModel
    {
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public DateTime birthdate { get; set; }
    }

    public class AddEmployeeModel
    {
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public DateTime birthdate { get; set; }
        public string tax_id { get; set; }
        public DateTime hired_date { get; set; }
        public int salary { get; set; }
        public int experience { get; set; }
    }

    public class AddDriverRoleModel
    {
        public string username { get; set; }
        public string license_id { get; set; }
        public string license_type { get; set; }
        public int driver_experience { get; set; }
    }

    public class AddProductModel
    {
        public string barcode { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
    }

    public class AddVanModel
    {
        public string van_id { get; set; }
        public int tag { get; set; }
        public int fuel { get; set; }
        public int capacity { get; set; }
        public int sales { get; set; }
        public string driven_by { get; set; }
    }

    public class AddBusinessModel
    {
        public string long_name { get; set; }
        public int rating { get; set; }
        public int spent { get; set; }
        public string location { get; set; }
    }

    public class StartFundingModel
    {
        public string username { get; set; }
        public int invested { get; set; }
        public string business { get; set; }
        public DateTime invested_date { get; set; }
    }

    public class HireEmployeeModel
    {
        public string username { get; set; }
        public string id { get; set; }
    }

    public class FireEmployeeModel
    {
        public string username { get; set; }
        public string id { get; set; }
    }

    public class ManageServiceModel
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    public class TakeOverVanModel
    {
        public string username { get; set; }
        public string van_id { get; set; }
        public int tag { get; set; }
    }

    public class LoadVanModel
    {
        public string van_id { get; set; }
        public int tag { get; set; }
        public string barcode { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }

    public class RefuelVanModel
    {
        public string van_id { get; set; }
        public int tag { get; set; }
        public int fuel_amount { get; set; }
    }

    public class DriveVanModel
    {
        public string van_id { get; set; }
        public int tag { get; set; }
        public string destination { get; set; }
    }

    public class PurchaseProductModel
    {
        public string long_name { get; set; }
        public string id { get; set; }
        public int tag { get; set; }
        public string barcode { get; set; }
        public int quantity { get; set; }
    }

    public class RemoveProductModel
    {
        public string barcode { get; set; }
    }

    public class RemoveVanModel
    {
        public string van_id { get; set; }
        public int tag { get; set; }
    }

    public class RemoveDriverRoleModel
    {
        public string username { get; set; }
    }

    public class AddLocationModel
    {
        public string label { get; set; }
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public int space { get; set; }
    }
}
