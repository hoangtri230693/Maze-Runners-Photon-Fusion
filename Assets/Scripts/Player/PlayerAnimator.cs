using UnityEngine;
using Fusion;


[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerAnimator : NetworkBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerStateMachine _stateMachine;

    //Biến để lưu trữ tốc độ đã được làm mượt
    private float _smoothSpeed;
    private float _speedSmoothVelocity;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public override void Render()
    {
        base.Render();
        _smoothSpeed = Mathf.SmoothDamp(_smoothSpeed, _playerMovement.PlayerSpeed, ref _speedSmoothVelocity, 0.1f, Mathf.Infinity, Runner.DeltaTime);
        _animator.SetFloat("Speed", _smoothSpeed);
        _animator.SetBool("Jump", _stateMachine.IsState(PlayerState.Jump));
    }
}
