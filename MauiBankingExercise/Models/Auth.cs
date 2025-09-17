


namespace MauiBankingExercise.Models
{

    public class Auth
    {
        public int AuthId { get; set; }
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AuthTypeId { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public AuthType AuthType { get; set; }
    }
}
