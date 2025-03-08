namespace _Project.Scripts.Model.FSM
{
    public class BaseState : IState
    {
        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
    }
}