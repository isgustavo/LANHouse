using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeHornDino.Core
{ 
    public class StateMachine
    {
        protected Dictionary<Type, State> states = new Dictionary<Type, State>();

        public State PreviousState { get; private set; }
        public State CurrentState { get; private set; }

        public StateMachine(Dictionary<Type, State> states)
        {
            this.states = states;
        }

        public void OnUpdate()
        {
            CurrentState?.OnUpdateState();
        }

        public void OnFixedUpdate()
        {
            CurrentState?.OnFixedUpdateState();
            
        }

        public void ChangeState<T>() where T : State
        {
            if (states.ContainsKey(typeof(T)))
            {
                CurrentState?.OnLeaveState();
                PreviousState = CurrentState;
                State nextState = states[typeof(T)];
                nextState.OnEnterState(PreviousState);
                CurrentState = nextState;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"State not found {typeof(T).ToString()}");
#endif
            }
        }
    }

}
