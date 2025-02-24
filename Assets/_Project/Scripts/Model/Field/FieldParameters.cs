using System;
using System.IO;
using System.Runtime.Serialization;
using _Project.Scripts.Model.GameParameters;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Model.Field
{
    [Serializable]
    public class FieldParameters
    {
        [JsonIgnore] private MapConfig _mapConfig;
        [JsonProperty] private string _mapConfigPath;
        [JsonProperty] private EdgeAmountType _edgeAmountType;
        
        public FieldParameters(MapConfig mapConfig, EdgeAmountType edgeAmountType)
        {
            MapConfig = mapConfig;
            EdgeAmountType = edgeAmountType;
        }

        [JsonIgnore]
        public MapConfig MapConfig
        {
            get => _mapConfig;
            set
            {
                if (!value) return;
                
                _mapConfig = value;
                _mapConfigPath = value.name;
            }
        }

        [JsonIgnore]
        public EdgeAmountType EdgeAmountType
        {
            get => _edgeAmountType;
            set
            {
                var intValue = (int)value;
                var isValid = intValue < Enum.GetValues(typeof(EdgeAmountType)).Length && intValue >= 0;
                
                if (isValid)
                {
                    _edgeAmountType = value;
                }
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (string.IsNullOrEmpty(_mapConfigPath)) return;
            
            var path = Path.Combine(Constants.MapConfigsFolder, _mapConfigPath);
            _mapConfig = Resources.Load<MapConfig>(path);
        }
    }
}