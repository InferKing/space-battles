using _Project.Scripts.Model.GameParameters;

namespace _Project.Scripts.Model
{
    public abstract class Constants
    {
        public const int MaxStars = 128;
        public const int MinStars = 6;
        public const float MinTeamRadius = 7f;
        public const float MaxTeamRadius = 20f;
        public const float MinDistanceBetweenStars = 6f;
        public const float MaxDistanceBetweenStars = 20f;
        public const float MinGenerationArea = 10f;
        public const float MaxGenerationArea = 200f;

        public const DifficultyType OptimalDifficulty = DifficultyType.Medium;
        public const MapType OptimalMapSize = MapType.Medium;
        public const EdgeAmountType OptimalEdgeAmount = EdgeAmountType.Medium;
        public const float SpawnDelay = 0.5f;
        public const float OptimalDistanceBetweenStars = 10f;
        public const float OptimalGenerationArea = 30f;
        public const float OptimalTeamRadius = 12f;
        public const int OptimalStarCount = 14;
        public const int OptimalEdgeCount = 21;
        
        public const float MinBaseSpeed = 1f;
        public const float MaxBaseSpeed = 5f;
        public const float MinBaseAcceleration = 2f;
        public const float MaxBaseAcceleration = 9f;
        public const float MinBaseShipHealth = 5f;
        public const float MaxBaseShipHealth = 25f;
        public const float MinBaseStarHealth = 500f;
        public const float MaxBaseStarHealth = 1500f;
        public const float MinBaseDamage = 3f;
        public const float MaxBaseDamage = 10f;
        public const float MinBaseAttackSpeed = 0.5f;
        public const float MaxBaseAttackSpeed = 1.5f;
        public const int MinBaseShipLimit = 8;
        public const int MaxBaseShipLimit = 20;
        
        public const float Epsilon = 0.001f;
        
        public const string DefaultDataFolder = "Game data";
        public const string LocalizationFolder = "Localization";
        public const string SessionFile = "session.json";
        public const string MapConfigsFolder = "Map";
    }
}