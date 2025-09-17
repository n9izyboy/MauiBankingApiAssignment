using System;
using System.Collections.Generic;

namespace MauiBankingExercise.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public int AccountTypeId { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateOpened { get; set; }
        public decimal AccountBalance { get; set; }

        // Navigation properties for API/EF compatibility
        public Customer Customer { get; set; }
        public AccountType AccountType { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
