using System.Collections.Generic;

namespace MauiBankingExercise.Models
{
    public class AssetType
    {
        public int AssetTypeId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Asset> Assets { get; set; }
    }
}
