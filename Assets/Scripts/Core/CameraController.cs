using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target; // Dein Player
    [SerializeField] private float smoothing = 5f; // Wie schnell die Kamera folgt
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, -10f); // Abstand zum Spieler

    [Header("Level Boundaries")]
    [SerializeField] private bool useBounds = true;
    [SerializeField] private Vector2 minBounds; // Unten Links (X, Y)
    [SerializeField] private Vector2 maxBounds; // Oben Rechts (X, Y)

    private void LateUpdate()
    {
        if (target == null) return;

        // 1. Berechne die Zielposition
        Vector3 targetPosition = target.position + offset;

        // 2. Wenn Grenzen aktiv sind, "klemme" die Position fest
        if (useBounds)
        {
            float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
            targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);
        }

        // 3. Bewege die Kamera geschmeidig (Lerp) zur Zielposition
        // Time.deltaTime sorgt dafür, dass es bei jeder Framerate gleich schnell ist
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }

    // Hilfsmittel, um die Grenzen im Editor zu sehen
    private void OnDrawGizmos()
    {
        if (!useBounds) return;

        Gizmos.color = Color.cyan;
        Vector3 topLeft = new Vector3(minBounds.x, maxBounds.y, 0);
        Vector3 topRight = new Vector3(maxBounds.x, maxBounds.y, 0);
        Vector3 bottomLeft = new Vector3(minBounds.x, minBounds.y, 0);
        Vector3 bottomRight = new Vector3(maxBounds.x, minBounds.y, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}