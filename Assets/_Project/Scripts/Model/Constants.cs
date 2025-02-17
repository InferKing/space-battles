using UnityEngine;

namespace _Project.Scripts.Model
{
    public class Constants
    {
        public const int MaxStars = 128;
        public const float MinDistanceBetweenStars = 6f;
        public const float MaxDistanceBetweenStars = 20f;
        public const float MinGenerationArea = 10f;
        public const float MaxGenerationArea = 200f;

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
    }
}