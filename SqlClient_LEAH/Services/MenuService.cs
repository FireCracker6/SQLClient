using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlClient_LEAH.Models;

namespace SqlClient_LEAH.Services
{
    internal class MenuService
    {
        public void CreateNewContact()
        {
            var customer = new Customer();

            Console.Write("Förnamn: ");
            customer.Firstname = Console.ReadLine() ?? "";

            Console.Write("Efternamn: ");
            customer.Lastname = Console.ReadLine() ?? "";

            Console.Write("E-postadress: ");
            customer.Email = Console.ReadLine() ?? "";

            Console.Write("Telefonnummer: ");
            customer.PhoneNumber = Console.ReadLine() ?? "";


            Console.Write("Gatuadress: ");
            customer.Address.StreetName = Console.ReadLine() ?? "";

            Console.Write("Postnummer: ");
            customer.Address.PostalCode = Console.ReadLine() ?? "";

            Console.Write("Ort: ");
            customer.Address.City = Console.ReadLine() ?? "";

            // Save customer to database
            var database = new DatabaseService();
            database.SaveCustomer(customer);


        }
        public void ListAllContacts()
        {
            // get all customers plus address from database
            var database = new DatabaseService();
            var customers = database.GetCustomers();

            if (customers.Any())
            {
                foreach (Customer customer in customers)
                {
                    Console.WriteLine($"{customer.Id}");
                    Console.WriteLine($"Namn: {customer.Firstname} {customer.Lastname}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefon: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.Address.StreetName}, {customer.Address.PostalCode}, {customer.Address.City}");
                    Console.WriteLine("");
                }

            }
            else
            {
                Console.WriteLine("Inga kunder finns i databasen.");
                Console.WriteLine("");
            }


        }
        public void ListSpecificContact()
        {
            // get specific customer plus address from database
            var database = new DatabaseService();

            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(email))
            {
                var customer = database.GetCustomer(email);

                if (customer != null)
                {
                    Console.WriteLine($"{customer.Id}");
                    Console.WriteLine($"Namn: {customer.Firstname} {customer.Lastname}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefon: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.Address.StreetName}, {customer.Address.PostalCode}, {customer.Address.City}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Ingen e-postadress {email} hittades.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"Ingen kund med den angivna e-postadressen angiven.");
                Console.WriteLine("");
            }

        }

        public void DeleteSpecificContact()
        {
            // get specific customer plus address from database
            var database = new DatabaseService();

            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(email))
            {
                var customer = database.GetCustomer(email);

                if (customer != null)
                {
                    Console.WriteLine($"{customer.Id}");
                    Console.WriteLine($"Namn: {customer.Firstname} {customer.Lastname}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefon: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.Address.StreetName}, {customer.Address.PostalCode}, {customer.Address.City}");
                    Console.WriteLine("");
                  //  Console.WriteLine("Vill du verkligen ta bort den här kunden?: ");
                  
                    database.DeleteCustomerFromDatabase(customer!);

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Ingen e-postadress {email} hittades.");
                    Console.WriteLine("");
                }
                
            }
            else
            {
                Console.WriteLine($"Ingen kund med den angivna e-postadressen angiven.");
                Console.WriteLine("");
            }

        }
        public void UpdateSpecificCustomer()
        {
            // get specific customer plus address from database
            var database = new DatabaseService();

            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(email))
            {
                var customer = database.GetCustomer(email);

                if (customer != null)
                {
                    Console.WriteLine($"{customer.Id}");
                    Console.WriteLine($"Namn: {customer.Firstname} {customer.Lastname}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefon: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.Address.StreetName}, {customer.Address.PostalCode}, {customer.Address.City}");
                    Console.WriteLine("");

                   

                    Console.Write("Förnamn: ");
                    customer.Firstname = Console.ReadLine() ?? "";

                    Console.Write("Efternamn: ");
                    customer.Lastname = Console.ReadLine() ?? "";

                    Console.Write("E-postadress: ");
                    customer.Email = Console.ReadLine() ?? "";

                    Console.Write("Telefonnummer: ");
                    customer.PhoneNumber = Console.ReadLine() ?? "";


                    Console.Write("Gatuadress: ");
                    customer.Address.StreetName = Console.ReadLine() ?? "";

                    Console.Write("Postnummer: ");
                    customer.Address.PostalCode = Console.ReadLine() ?? "";

                    Console.Write("Ort: ");
                    customer.Address.City = Console.ReadLine() ?? "";

                    // Save customer to database
               
                 

                    database.UpdateCustomerFromDatabase(customer!);

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Ingen e-postadress {email} hittades.");
                    Console.WriteLine("");
                }

            }
            else
            {
                Console.WriteLine($"Ingen kund med den angivna e-postadressen angiven.");
                Console.WriteLine("");
            }

        }
    }

}
