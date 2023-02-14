

namespace SqlClient.Models
{
    internal class Customer
    {
        public int CustomerId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Address Address { get; set; } = new Address();
      
    }
}
