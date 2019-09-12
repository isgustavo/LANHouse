namespace ThreeHornDino.Core
{
    public abstract class State
    {
        public virtual void OnEnterState(State previousState = null) { }
        public virtual void OnUpdateState() { }
        public virtual void OnFixedUpdateState() { }
        public virtual void OnLeaveState() { }
    }
}
