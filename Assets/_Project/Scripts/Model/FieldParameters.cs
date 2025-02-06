using System;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Model
{
    [Serializable]
    public class FieldParameters
    {
        public FieldParameters(Vector2 generationArea, int starCount, float minDistanceBetweenStars, 
            float teamCircleRadius, int desiredEdgeCount)
        {
            StarCount = starCount;
            MinDistanceBetweenStars = minDistanceBetweenStars;
            TeamCircleRadius = teamCircleRadius;
            DesiredEdgeCount = desiredEdgeCount;
            GenerationArea = generationArea;
        }
        
        [JsonProperty] public int StarCount { get; private set; }
        [JsonProperty] public float MinDistanceBetweenStars { get; private set; }
        [JsonProperty] public float TeamCircleRadius { get; private set; }
        [JsonProperty] public int DesiredEdgeCount { get; private set; }
        [JsonProperty] public Vector2 GenerationArea { get; private set; }
        
        public void SetStarCount(int value, Action errorCallback = null)
        {
            var teams = Enum.GetValues(typeof(Team)).Length - 1;

            if (value <= teams || value > Constants.MaxStars)
            {
                errorCallback?.Invoke();
                return;
            }

            StarCount = value;
        }

        public void SetMinDistanceBetweenStars(float distance, Action errorCallback = null)
        {
            if (distance < Constants.MinDistanceBetweenStars || distance > Constants.MaxDistanceBetweenStars)
            {
                errorCallback?.Invoke();
                return;
            }

            MinDistanceBetweenStars = distance;
        }

        public void SetTeamCircleRadius(float radius, Action errorCallback = null)
        {
            // TODO: Надо проверить работу, возможно неверное условие
            if (radius < GenerationArea.x / 2 || radius > GenerationArea.x)
            {
                errorCallback?.Invoke();
                return;
            }

            TeamCircleRadius = radius;
        }

        public void SetDesiredEdgeCount(int edges, Action errorCallback = null)
        {
            if (edges < StarCount - 1 || edges > StarCount * 2)
            {
                errorCallback?.Invoke();
                return;
            }

            DesiredEdgeCount = edges;
        }

        public void SetGenerationArea(int size, Action errorCallback = null)
        {
            if (size < Constants.MinGenerationArea || size > Constants.MaxGenerationArea)
            {
                errorCallback?.Invoke();
                return;
            }

            GenerationArea = new Vector2(size, size);
        }
    }
}