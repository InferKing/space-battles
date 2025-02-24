using _Project.Scripts.Model.Field;
using Newtonsoft.Json;

namespace _Project.Scripts.Model.Player
{
    [System.Serializable]
    public class PlayerSession
    {
        public PlayerSession(FieldParameters fieldParameters, Team team, DifficultyType difficultyType)
        {
            FieldParameters = fieldParameters;
            Team = team;
            DifficultyType = difficultyType;
        }
        
        [JsonProperty] public FieldParameters FieldParameters { get; }
        [JsonProperty] public Team Team { get; }
        [JsonProperty] public DifficultyType DifficultyType { get; }
    }
}