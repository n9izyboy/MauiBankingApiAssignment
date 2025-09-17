using System.Collections.Generic;

namespace MauiBankingExercise.Models
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Account> Accounts { get; set; }
    }
}
