using UnityEngine;
using UnityEngine.InputSystem; // WICHTIG: Das neue System einbinden!

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region State Machine
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    #endregion

    #region Movement Settings
    [Header("Movement")]
    public float moveSpeed = 8f;
    #endregion

    #region Input Variables
    [Header("Input")]
    public InputAction moveAction; // NEU: Unsere Input-Aktion für das neue System
    
    public float InputX { get; private set; } 
    public bool FacingRight { get; private set; } = true;
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
    }

    // NEU: Input Actions müssen aktiviert werden!
    private void OnEnable()
    {
        moveAction.Enable();
    }

    // NEU: Input Actions müssen deaktiviert werden, wenn das Objekt inaktiv ist!
    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        // NEU: Wir lesen den Wert (z.B. -1 bis 1) aus der Aktion aus
        InputX = moveAction.ReadValue<float>();

        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Helper Methods
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
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