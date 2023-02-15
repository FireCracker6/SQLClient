using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLClient_Dapper.Models;

namespace SQLClient_Dapper.Services
{
    internal class MenuService
    {
        public async Task CreateNewContactAsync()
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
            customer.StreetName = Console.ReadLine() ?? "";

            Console.Write("Postnummer: ");
            customer.PostalCode = Console.ReadLine() ?? "";

            Console.Write("Ort: ");
            customer.City = Console.ReadLine() ?? "";

            // Save customer to database
            await CustomerService.SaveAsync(customer);


        }
        public async Task ListAllContactsAsync()
        {
            // get all customers plus address from database
          var customers = await CustomerService.GetAllAsync();

            if (customers.Any())
            {
                foreach (Customer customer in customers)
                {
                    Console.WriteLine($"Kundnummer: {customer.Id}");
                    Console.WriteLine($"Namn: {customer.Firstname} {customer.Lastname}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefon: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.StreetName}, {customer.PostalCode}, {customer.City}");
                    Console.WriteLine("");
                }

            }
            else
            {
                Console.WriteLine("Inga kunder finns i databasen.");
                Console.WriteLine("");
            }


        }
        public async Task ListSpecificContactAsync()
        {
            

            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(email))
            {
                // get specific customer plus address from database
                var customer = await CustomerService.GetAsync(email);

                if (customer != null)
                {
                    Console.WriteLine($"Kundnummer:  {customer.Id}");
                    Console.WriteLine($"Namn: {customer.Firstname} {customer.Lastname}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefon: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.StreetName}, {customer.PostalCode}, {customer.City}");
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

        public async Task UpdateSpecificContactAsync()
        {


            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(email))
            {


                var customer = await CustomerService.GetAsync(email);


                Console.Write("Skriv in information på de fält som du vill uppdatera: \n");

                Console.Write("Förnamn: ");
                customer.Firstname = Console.ReadLine() ?? null!;

                Console.Write("Efternamn: ");
                customer.Lastname = Console.ReadLine() ?? null!;

                Console.Write("E-postadress: ");
                customer.Email = Console.ReadLine() ?? null!;

                Console.Write("Telefonnummer: ");
                customer.PhoneNumber = Console.ReadLine() ?? null!;


                Console.Write("Gatuadress: ");
                customer.StreetName = Console.ReadLine() ?? null!;

                Console.Write("Postnummer: ");
                customer.PostalCode = Console.ReadLine() ?? null!;

                Console.Write("Ort: ");
                customer.City = Console.ReadLine() ?? null!;

              

                // update specific customer from database
                await CustomerService.UpdateAsync(customer);


            }
            else
            {
                Console.WriteLine($"Ingen kund med den angivna e-postadressen angiven.");
                Console.WriteLine("");
            }
        }

        public async Task DeleteSpecificContactAsync()
        {


            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(email))
            {
                // delete specific customer from database
                await CustomerService.DeleteAsync(email);

               
            }
            else
            {
                Console.WriteLine($"Ingen kund med den angivna e-postadressen angiven.");
                Console.WriteLine("");
            }
        }
    }
}
