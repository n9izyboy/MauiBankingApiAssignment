using System.Collections.Generic;

namespace MauiBankingExercise.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BranchCode { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public bool IsActive { get; set; }
        public string OperatingHours { get; set; }

        // Navigation property
        public ICollection<Customer> Customers { get; set; }
    }
}
