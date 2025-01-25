using UnityEngine;

public class EnemyCapturer : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    public LayerMask characterLayer;
    public GameObject bubblePrefab; // Prefab de la burbuja
    public Transform bubbleSpawnPoint; // Lugar donde aparece la burbuja

    private void Update()
    {
        Patrol();
        DetectAndCapture();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[currentPoint].position) < 0.2f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void DetectAndCapture()
    {
        Collider2D character = Physics2D.OverlapCircle(transform.position, 2f, characterLayer);
        if (character)
        {
            CaptureCharacter(character.gameObject);
        }
    }

    void CaptureCharacter(GameObject character)
    {
        GameObject bubble = Instantiate(bubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
        bubble.GetComponent<Bubble>().Capture(character);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}

