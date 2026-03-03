using UnityEngine;
using UnityEngine.InputSystem; 

namespace Assets.Scripts.Player.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region State Machine
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerFallState FallState { get; private set; }
        public PlayerLightAttackState LightAttackState { get; private set; }    
        public PlayerHeavyAttackState HeavyAttackState { get; private set; }
        #endregion

        #region Components
        public Rigidbody2D RB { get; private set; }
        public Animator Anim { get; private set; }
        #endregion

        #region Movement Settings
        [Header("Movement")]
        public float moveSpeed = 8f;

        [Header("Jump Settings")]
        public float jumpForce = 16f;
        public float coyoteTime = 0.05f;
        public float jumpBufferTime = 0.1f;

        [Header("Dash Settings")]
        public float dashSpeed = 24f;
        public float dashDuration = 0.2f;
        public float dashCooldown = 1f;
        #endregion

        #region Input Variables
        [Header("Input")]
        public InputAction moveAction;
        public InputAction jumpAction;
        public InputAction dashAction;
        public InputAction lightStrikeAction;
        public InputAction heavyStrikeAction;

        public float InputX { get; private set; }
        public bool FacingRight { get; private set; } = true;
        #endregion

        #region State Variables & Timers
        public float CoyoteTimeCounter { get; private set; }
        public float JumpBufferCounter { get; private set; }
        public float DashCooldownTimer { get; private set; }
        public float DefaultGravity { get; private set; }
        #endregion

        #region Collision Checks
        [Header("Collision")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask whatIsGround;
        #endregion

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, StateMachine);
            RunState = new PlayerRunState(this, StateMachine);
            JumpState = new PlayerJumpState(this, StateMachine);
            DashState = new PlayerDashState(this, StateMachine);
            FallState = new PlayerFallState(this, StateMachine);
            LightAttackState = new PlayerLightAttackState(this, StateMachine);
            HeavyAttackState = new PlayerHeavyAttackState(this, StateMachine);
        }

        private void OnEnable()
        {
            moveAction.Enable();
            jumpAction.Enable();
            dashAction.Enable();
            lightStrikeAction.Enable();
            heavyStrikeAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            jumpAction.Disable();
            dashAction.Disable();
            lightStrikeAction.Disable();
            heavyStrikeAction.Disable();
        }

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            Anim = GetComponentInChildren<Animator>();
            DefaultGravity = RB.gravityScale;
            StateMachine.Initialize(IdleState);

        }

        private void Update()
        {
            InputX = moveAction.ReadValue<float>();

            if (CheckIfGrounded()) CoyoteTimeCounter = coyoteTime;
            else CoyoteTimeCounter -= Time.deltaTime;

            if (jumpAction.WasPressedThisFrame()) JumpBufferCounter = jumpBufferTime;
            else JumpBufferCounter -= Time.deltaTime;

            if (DashCooldownTimer > 0) DashCooldownTimer -= Time.deltaTime;

            StateMachine.CurrentState.LogicUpdate();

            CheckStateTransitions();
        }

        private void CheckStateTransitions()
        {
            PlayerState currentState = StateMachine.CurrentState;
            PlayerState nextState = currentState;

            if (dashAction.WasPressedThisFrame() && DashCooldownTimer <= 0f)
            {
                nextState = DashState;
            }
            else if (JumpBufferCounter > 0f && CoyoteTimeCounter > 0f && currentState != JumpState && jumpAction.WasPressedThisFrame())
            {
                nextState = JumpState;
            }
            else if (lightStrikeAction.WasPressedThisFrame())
            {
                nextState = LightAttackState;
            }
            else if (heavyStrikeAction.WasPressedThisFrame())
            {
                nextState = HeavyAttackState;
            }
            else if (CheckIfGrounded() && RB.linearVelocity.y <= 0.1f)
            {
                if (InputX != 0f) nextState = RunState;
                else nextState = IdleState;
            }
            else if (!CheckIfGrounded() && RB.linearVelocity.y < -0.1f)
            {
                nextState = FallState;
            }

            if (nextState != currentState)
            {
                if ((currentState.IsFinished || currentState.CanBeInterrupted()) && nextState.IsAllowedToChangeFromState(currentState))
                {
                    StateMachine.ChangeState(nextState);
                }
            }
        }

        private void FixedUpdate()
        {
            RB.linearVelocityX *= 0.9f;
            StateMachine.CurrentState.PhysicsUpdate();
        }

        #region Helper Methods
        public void ConsumeJump()
        {
            CoyoteTimeCounter = 0f;
            JumpBufferCounter = 0f;
        }
        public bool CheckIfGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }

        public void ResetDashCooldown()
        {
            DashCooldownTimer = dashCooldown;
        }

        public void CheckIfShouldFlip(float xInput)
        {
            if (xInput != 0 && xInput > 0 != FacingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            FacingRight = !FacingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
        #endregion
    }
}