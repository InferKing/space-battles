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
            
        public const float Epsilon = 0.001f;
        
        public const string DefaultDataFolder = "Game data";
        public const string LocalizationFolder = "Localization";
    }
}