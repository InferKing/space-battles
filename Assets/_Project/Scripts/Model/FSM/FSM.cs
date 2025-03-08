using System;

namespace _Project.Scripts.Model.FSM
{
    public class FSM
    {
        private IState _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public bool IsCurrentState(Type stateType)
        {
            if (_currentState == null || stateType == null) 
                return false;

            return _currentState.GetType() == stateType;
        }
    }
}