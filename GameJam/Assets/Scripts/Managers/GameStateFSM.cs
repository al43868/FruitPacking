using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFSM : IFsm
{
    [SerializeField]
    public int CurrentState { get; private set; }
    #region FSM Base
    private readonly Dictionary<int, Tuple<System.Action<int>, System.Action<int>>>
        _actions = new();
    private readonly List<ValueTuple<int, int, int>> _transitions = new();
    public int stateCount = 0;
    //添加状态
    public bool AddState(int state, Action<int> onEnter, Action<int> onExit)
    {
        if (_actions.ContainsKey(state))
        {
            throw new Exception($"不能重复添加状态{state}行为");
        }

        _actions.Add(state, new Tuple<Action<int>, Action<int>>(onEnter, onExit));
        stateCount++;
        return true;
    }
    //添加转换
    public bool AddTransition(int from, int to, int triggerCode)
    {
        if (!_actions.ContainsKey(from) || !_actions.ContainsKey(to))
        {
            return false;
        }
        _transitions.Add(item: (from, to, triggerCode));
        return true;
    }
    //移除状态
    public bool RemoveState(int state)
    {
        if (_actions.ContainsKey(state))
        {
            return false;
        }

        _actions.Remove(state);
        return true;
    }
    //切换状态
    public bool SwitchToState(int stateId, bool forceSwitch = false)
    {
        bool hasState = _actions.ContainsKey(stateId);
        if (!hasState)
        {
            Debug.Log("no state:" + stateId);
            return false;
        }
        bool stateChanged = stateId != CurrentState;
        if (!stateChanged)
        {
            if (!forceSwitch)
            {
                Debug.Log("Game state needChange" + stateId);
                return false;
            }
        }
        if (stateChanged)
        {
            if (_actions.TryGetValue(CurrentState, out var oldActions))
            {
                oldActions.Item2?.Invoke(stateId);
            }
        }
        var oldStateId = CurrentState;
        var newActions = _actions[stateId];
        CurrentState = stateId;
        newActions.Item1?.Invoke(oldStateId);
        Debug.Log("GameFSMcurrentState:" + CurrentState);
        return true;
    }
    //切换状态事件
    public bool TriggerEvent(int eventCode)
    {
        foreach ((int, int, int) transition in _transitions)
        {
            if (transition.Item1 == CurrentState && transition.Item3 == eventCode)
            {
                SwitchToState(transition.Item2);
                return true;
            }
        }
        return false;
    }
    #endregion
    public void Init()
    {

        //-1:未开始
        AddState(-1, null, null);
        ////1：养成
        AddState(1,
            (x) =>
            {
                GameRoot.Instance.Clear();
                GameRoot.Instance.ShowDevelopMainPanel();
                AudioManager.Instance.PlayBG();
            },
        (x) =>
            {
                
            });
        //2：玩法
        AddState(2,
            (x) =>
            {
                GameRoot.Instance.Clear();
                GameRoot.Instance.ShowGamePlayPanel();
                GamePlayManager.Instance.Init();
            },
            (x) =>
            {

            });
    }
    public GameStateFSM(int prepareState = -1)
    {
        Init();
        CurrentState = -1;
        SwitchToState(prepareState);
    }
}