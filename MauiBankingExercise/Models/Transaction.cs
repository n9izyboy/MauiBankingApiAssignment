using System;

namespace MauiBankingExercise.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public int AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        // Navigation properties for API/EF compatibility
        public Account Account { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
