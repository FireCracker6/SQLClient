


using Microsoft.Data.SqlClient;
using SqlClient.Models;
using SqlClient.Models.Entitites;

namespace SqlClient.Services
{
    internal class DatabaseService
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Svart\Source\Repos\Webbutveckling\SqlClient\SqlClient\Data\local_sql.mdf;Integrated Security=True;Connect Timeout=30";
        public void SaveCustomer(Customer customer)
        {
            var customerEntity = new CustomerEntity
            {
                Firstname = customer.Firstname,
                Lastname= customer.Lastname,
                Email  = customer.Email,
                PhoneNumber= customer.PhoneNumber,
                AddressId = GetOrSaveAddressToOrFromDatabase(customer.Address)
            };

            SaveCustomerToDatabase(customerEntity);
         
        }
        public IEnumerable<Customer> GetCustomers() 
        {
            var customers = new List<Customer>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT c.Id, c.FirstName, c.LastName, c.Email, c.PhoneNumber, a.StreetName, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id", conn);
            
            var result = cmd.ExecuteReader();
           
           if (result.HasRows)
            {
                while (result.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerId = result.GetInt32(0),
                        Firstname = result.GetString(1),
                        Lastname = result.GetString(2),
                        Email = result.GetString(3),
                        PhoneNumber = result.GetString(4),
                        Address = new Address
                        {
                            StreetName = result.GetString(5),
                            PostalCode = result.GetString(6),
                            City = result.GetString(7),
                        }
                    });
                }
            }
            return customers;
        }

        public Customer GetCustomer(string email)
        {
           
           
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT c.Id, c.FirstName, c.LastName, c.Email, c.PhoneNumber, a.StreetName, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id WHERE c.Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", email);


            var result = cmd.ExecuteReader();
            var customer = new Customer();
              
            if (result.HasRows)
            {
                while (result.Read())
                {

                    customer.CustomerId = result.GetInt32(0);
                    customer.Firstname = result.GetString(1);
                    customer.Lastname = result.GetString(2);
                    customer.Email = result.GetString(3);
                    customer.PhoneNumber = result.GetString(4);
                    customer.Address = new Address
                    {
                        StreetName = result.GetString(5),
                        PostalCode = result.GetString(6),
                        City = result.GetString(7),
                    };
                }
                return customer;
            }

            return null!;

        }

        private int GetOrSaveAddressToOrFromDatabase(Address address)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();


            using var cmd = new SqlCommand("IF NOT EXISTS ( SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City) INSERT INTO Addresses OUTPUT INSERTED.Id VALUES (@StreetName, @PostalCode, @City) ELSE ( SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City)", conn);
            cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
            cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
            cmd.Parameters.AddWithValue("@City", address.City);

            return int.Parse(cmd.ExecuteScalar().ToString()!);

        }
        private void SaveCustomerToDatabase(CustomerEntity customerEntity) 
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("IF NOT EXISTS (SELECT Id From Customers WHERE Email = @Email) INSERT INTO Customers VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressId)", conn);
            cmd.Parameters.AddWithValue("@FirstName", customerEntity.Firstname);
            cmd.Parameters.AddWithValue("@LastName", customerEntity.Lastname);
            cmd.Parameters.AddWithValue("@Email", customerEntity.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", customerEntity.PhoneNumber);
            cmd.Parameters.AddWithValue("@AddressId", customerEntity.AddressId);

        
            cmd.ExecuteNonQuery();

        }
    }
}
