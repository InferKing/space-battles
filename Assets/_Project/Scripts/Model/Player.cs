using _Project.Scripts.Configs;

namespace _Project.Scripts.Model
{
    public class Player
    {
        private PlayerData _playerData;
        
        public Player(PlayerData playerData)
        {
            // TODO: что-то должно подаваться, что влияет на изначальные уровни апгрейдов
            _playerData = playerData;
            LevelSystem = new XpLevelSystem();
        }
        
        public XpLevelSystem LevelSystem { get; }
    }
}