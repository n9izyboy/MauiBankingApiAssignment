
namespace MauiBankingExercise.Models
{


    public class Asset
    {
        public int AssetId { get; set; }
        public int AssetTypeId { get; set; }
        public int CustomerId { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public AssetType AssetType { get; set; }
        public Customer Customer { get; set; }
    }
}
