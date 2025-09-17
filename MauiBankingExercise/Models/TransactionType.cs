using System.Collections.Generic;

namespace MauiBankingExercise.Models
{
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Transaction> Transactions { get; set; }
    }
}
