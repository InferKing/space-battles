using Zenject;

namespace _Project.Scripts.Model.Core.TargetSelector
{
    public class FactorySelector : IFactory<bool, ITargetSelector>
    {
        private readonly DiContainer _container;

        public FactorySelector(DiContainer container)
        {
            _container = container;
        }
        
        public ITargetSelector Create(bool isPlayer)
        {
            return isPlayer ? _container.Instantiate<PlayerSelector>() : _container.Instantiate<BotSelector>();
        }
    }
}