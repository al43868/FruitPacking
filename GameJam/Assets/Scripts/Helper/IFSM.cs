
public interface IFsm
{
    int CurrentState { get; }
    bool AddState(int state, System.Action<int> onEnter, System.Action<int> onExit);
    bool RemoveState(int state);
    bool AddTransition(int from, int to, int triggerCode);
    bool TriggerEvent(int eventCode);
    bool SwitchToState(int stateId, bool forceSwitch);
}

