using UnityEngine;
using Fusion;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;

    private CharacterController _controller;
    private PlayerStateMachine _stateMachine;

    public float MoveSpeed = 5f;
    public float SprintSpeed = 10f;
    public float JumpForce = 15f;
    public float GravityValue = -9.81f;
    public Camera Camera;

    [Networked] public float PlayerSpeed { get; set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _stateMachine.ChangeState(PlayerState.Jump);
        }
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<ThirdPersonCamera>().Target = transform;
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (Object.HasStateAuthority)
            {
                Movement();
                Jump();
            }
        }
        else
        {
            _stateMachine.ChangeState(PlayerState.Idle);
            PlayerSpeed = 0;
        }   
    }

    private void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (_controller.isGrounded && !_stateMachine.IsState(PlayerState.Jump))
        {
            //Di chuyển Idle - Move - Sprint
            if (direction.magnitude >= 0.1f)
            {
                // Xoay hướng theo camera
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.transform.eulerAngles.y;
                float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, 0.15f); // mượt hơn
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                // Tính vector move theo góc camera
                direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _stateMachine.ChangeState(PlayerState.Sprint);
                    _controller.Move(direction * SprintSpeed * Runner.DeltaTime);
                    PlayerSpeed = SprintSpeed;
                }
                else
                {
                    _stateMachine.ChangeState(PlayerState.Move);
                    _controller.Move(direction * MoveSpeed * Runner.DeltaTime);
                    PlayerSpeed = MoveSpeed;
                }
            }
            else
            {
                _stateMachine.ChangeState(PlayerState.Idle);
                PlayerSpeed = 0;
            }
        }
    }

    private void Jump()
    {
        if (_controller.isGrounded && _stateMachine.IsState(PlayerState.Jump))
        {
            _velocity.y = JumpForce;
        }
        else
        {
            if (_controller.isGrounded)
            {
                _velocity.y = -1f;
            }
            else
            {
                _velocity.y += GravityValue * Runner.DeltaTime;
                _stateMachine.ChangeState(PlayerState.Idle);
            } 
        }

        _controller.Move(_velocity * Runner.DeltaTime);
    }
}
