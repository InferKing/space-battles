using System;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Model
{
    [Serializable]
    public class FieldParameters
    {
        public FieldParameters(Vector2 generationArea, int starCount = Constants.OptimalStarCount, 
            float minDistanceBetweenStars = Constants.OptimalDistanceBetweenStars, 
            float teamCircleRadius = Constants.OptimalTeamRadius, int desiredEdgeCount = Constants.OptimalEdgeCount)
        {
            SetStarCount(starCount);
            SetMinDistanceBetweenStars(minDistanceBetweenStars);
            SetGenerationArea((int)generationArea.x);
            SetTeamCircleRadius(teamCircleRadius);
            SetDesiredEdgeCount(desiredEdgeCount);
        }
        
        [JsonProperty] public int StarCount { get; private set; }
        [JsonProperty] public float MinDistanceBetweenStars { get; private set; }
        [JsonProperty] public float TeamCircleRadius { get; private set; }
        [JsonProperty] public int DesiredEdgeCount { get; private set; }
        [JsonProperty] public Vector2 GenerationArea { get; private set; }
        
        public void SetStarCount(int value, Action<bool> validationResult = null)
        {
            var teams = Enum.GetValues(typeof(Team)).Length - 1;

            if (value <= teams || value > Constants.MaxStars)
            {
                validationResult?.Invoke(false);
                return;
            }

            StarCount = value;
            validationResult?.Invoke(true);
        }

        public void SetMinDistanceBetweenStars(float distance, Action<bool> validationResult = null)
        {
            if (distance < Constants.MinDistanceBetweenStars || distance > Constants.MaxDistanceBetweenStars)
            {
                validationResult?.Invoke(false);
                return;
            }

            MinDistanceBetweenStars = distance;
            validationResult?.Invoke(true);
        }

        public void SetTeamCircleRadius(float radius, Action<bool> validationResult = null)
        {
            // TODO: Надо проверить работу, возможно неверное условие
            if (radius < GenerationArea.x / 2 || radius > GenerationArea.x)
            {
                validationResult?.Invoke(false);
                return;
            }

            TeamCircleRadius = radius;
            validationResult?.Invoke(true);
        }

        public void SetDesiredEdgeCount(int edges, Action<bool> validationResult = null)
        {
            if (edges < StarCount - 1 || edges > StarCount * 2)
            {
                validationResult?.Invoke(false);
                return;
            }

            DesiredEdgeCount = edges;
            validationResult?.Invoke(true);
        }

        public void SetGenerationArea(int size, Action<bool> validationResult = null)
        {
            if (size < Constants.MinGenerationArea || size > Constants.MaxGenerationArea)
            {
                validationResult?.Invoke(false);
                return;
            }

            GenerationArea = new Vector2(size, size);
            validationResult?.Invoke(true);
        }
    }
}