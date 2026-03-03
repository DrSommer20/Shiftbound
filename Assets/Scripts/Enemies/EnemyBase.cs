using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 30f;
    public float moveSpeed = 3f;

    [Header("Collision & Movement")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float checkDistance = 0.5f;

    public Rigidbody2D RB { get; private set; }
    public int FacingDirection { get; private set; } = 1; // 1 = Rechts, -1 = Links

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Hilfsfunktionen für die KI (Patrouille)
    public bool IsDetectingGround()
    {
        // Schießt einen unsichtbaren Strahl nach unten, um zu prüfen, ob da noch Boden ist (Abgrund-Erkennung)
        return Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, whatIsGround);
    }

    public bool IsDetectingWall()
    {
        // Schießt einen unsichtbaren Strahl nach vorne, um Wände zu erkennen
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, checkDistance, whatIsGround);
    }

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null && wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3.right * FacingDirection * checkDistance));
        }
    }
}