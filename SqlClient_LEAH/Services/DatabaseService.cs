using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SqlClient_LEAH.Models.Entitites;
using SqlClient_LEAH.Models;
using System.Data;

namespace SqlClient_LEAH.Services
{
    internal class DatabaseService
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Svart\Source\Repos\Webbutveckling\SqlClient\SqlClient_LEAH\Data\local_shopping_cart.mdf;Integrated Security=True;Connect Timeout=30";
        public void SaveCustomer(Customer customer)
        {
            var customerEntity = new CustomerEntity
            {
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
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
                        Id = result.GetInt32(0),
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

                    customer.Id = result.GetInt32(0);
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

            using var cmd = new SqlCommand("IF NOT EXISTS (SELECT Id From Customers WHERE Email = @Email) INSERT INTO Customers VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @AddressId)", conn);
            cmd.Parameters.AddWithValue("@FirstName", customerEntity.Firstname);
            cmd.Parameters.AddWithValue("@LastName", customerEntity.Lastname);
            cmd.Parameters.AddWithValue("@Email", customerEntity.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", customerEntity.PhoneNumber);
            cmd.Parameters.AddWithValue("@Password", customerEntity.Password);
            cmd.Parameters.AddWithValue("@AddressId", customerEntity.AddressId);


            cmd.ExecuteNonQuery();

        }

        public void DeleteCustomerFromDatabase(Customer customer)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("IF EXISTS (SELECT Id From Customers WHERE Email = @Email) DELETE FROM Customers WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Id", customer.Id);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
     
        }

        public void UpdateCustomerFromDatabase(Customer customer)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("IF EXISTS (SELECT Id From Customers WHERE Email= @Email) UPDATE c SET FirstName = @FirstName, LastName =@LastName, PhoneNumber = @PhoneNumber FROM Customers c JOIN Addresses a ON c.AddressId = a.Id WHERE c.Email = @Email UPDATE a SET StreetName = @StreetName, PostalCode = @PostalCode,  City = @City FROM Addresses a JOIN Customers c ON a.Id = c.AddressId WHERE c.Email = @Email\r\n\r\n\r\n", conn);
            cmd.Parameters.AddWithValue("@FirstName", customer.Firstname);
            cmd.Parameters.AddWithValue("@LastName", customer.Lastname);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@StreetName", customer.Address.StreetName);
            cmd.Parameters.AddWithValue("@PostalCode", customer.Address.PostalCode);
            cmd.Parameters.AddWithValue("@City", customer.Address.City);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

        }
    }
}
