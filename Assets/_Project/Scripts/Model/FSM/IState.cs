namespace _Project.Scripts.Model.FSM
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}