using UnityEngine;

public class BubbleTurret : MonoBehaviour
{
    public GameObject bubblePrefab; // Prefab de la burbuja gigante
    public Transform firePoint; // Lugar desde donde se disparan las burbujas
    public float fireRate = 3f; // Tiempo entre disparos

    private float nextFireTime = 0f;

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
        rb.linearVelocity = Vector2.up * 2f; // Hace que la burbuja suba lentamente
    }
}
