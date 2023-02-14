using SqlClient_LEAH.Services;

var menu = new MenuService();

while (true)
{
    Console.Clear();
    Console.WriteLine("Leahs App !!! ");
    Console.WriteLine("---------------");
    Console.WriteLine("1. Skapa en ny kund");
    Console.WriteLine("2. Visa alla kunder");
    Console.WriteLine("3. Visa en specifik kund");
    Console.WriteLine("4. Ta bort en specifik kund");
    Console.WriteLine("5. Updatera en specifik kund");
    Console.Write("Välj ett av följande alternativ (1-5): ");


    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            menu.CreateNewContact();
            break;
        case "2":
            Console.Clear();
            menu.ListAllContacts();
            break;
        case "3":
            Console.Clear();
            menu.ListSpecificContact();
            break;
        case "4":
            Console.Clear();
            menu.DeleteSpecificContact();
            break;
        case "5":
            Console.Clear();
            menu.UpdateSpecificCustomer();
            break;

    }

    Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
    Console.ReadKey();
}