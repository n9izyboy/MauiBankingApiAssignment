using System.Collections.Generic;

namespace MauiBankingExercise.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhysicalAddress { get; set; }
        public string IdentityNumber { get; set; }
        public int GenderTypeId { get; set; }
        public int RaceTypeId { get; set; }
        public string Nationality { get; set; }
        public int MaritalStatusId { get; set; }
        public int EmploymentStatusId { get; set; }
        public int BankId { get; set; }
        public int CustomerTypeId { get; set; }

        // Navigation properties for API/EF compatibility
        public Bank Bank { get; set; }
        public required CustomerType CustomerType { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Auth> Auths { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}
