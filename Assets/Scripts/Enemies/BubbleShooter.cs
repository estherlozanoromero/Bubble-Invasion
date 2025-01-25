using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    public GameObject bubbleProjectilePrefab; // Prefab de la burbuja proyectil
    public Transform firePoint; // Punto desde donde dispara
    public float fireRate = 2f;
    public float detectionRange = 5f;
    public LayerMask playerLayer;

    private float nextFireTime = 0f;
    private Transform player;

    private void Update()
    {
        DetectPlayerAndShoot();
    }

    void DetectPlayerAndShoot()
    {
        Collider2D detectedPlayer = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (detectedPlayer)
        {
            player = detectedPlayer.transform;
            if (Time.time >= nextFireTime)
            {
                ShootBubble();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ShootBubble()
    {
        GameObject bubble = Instantiate(bubbleProjectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.position - firePoint.position).normalized;
        rb.linearVelocity = direction * 5f;
    }
}
