using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float bubbleSpeed = 5f;
    private float nextFireTime;

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootBubble();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.left * bubbleSpeed; // Dispara a la izquierda
    }
}
