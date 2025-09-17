using System.Collections.Generic;

namespace MauiBankingExercise.Models
{
    public class AuthType
    {
        public int AuthTypeId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Auth> Auths { get; set; }
    }
}
