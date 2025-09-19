using System;
using UnityEngine;
using Fusion;

public enum PlayerState
{
    Idle,
    Move,
    Sprint,
    Jump
}

public class PlayerStateMachine : NetworkBehaviour
{
    [Networked] public PlayerState CurrentState { get; private set; } = PlayerState.Idle;

    //Sự kiện khi đổi state, dùng cho Animator hoặc logic khác
    public event Action<PlayerState> OnStateChanged;

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);

        Debug.Log($"State changed to: {newState}");
    }

    public bool IsState(PlayerState state)
    {
        return CurrentState == state;
    }
}
