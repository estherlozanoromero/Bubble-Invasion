using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    public GameObject bubbleProjectilePrefab; // Prefab de la burbuja proyectil
    public Transform firePoint; // Punto desde donde dispara
    public float fireRate = 2f;
    public float detectionRange = 5f;
    public LayerMask playerLayer;
    public Transform leftLimit; // Límite izquierdo
    public Transform rightLimit; // Límite derecho
    public float moveSpeed = 2f; // Velocidad de movimiento

    private float nextFireTime = 0f;
    private Transform player;
    private Animator animator;
    private bool isDead = false;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        moveDirection = Vector2.right; // Iniciar moviéndose a la derecha
    }

    private void Update()
    {
        if (!isDead)
        {
            MoveBetweenLimits();
            DetectPlayerAndShoot();
        }
    }

    void MoveBetweenLimits()
    {
        rb.linearVelocity = moveDirection * moveSpeed;

        // Gira el sprite en la dirección del movimiento
        if (moveDirection.x > 0)
            spriteRenderer.flipX = false; // Mirando a la derecha
        else if (moveDirection.x < 0)
            spriteRenderer.flipX = true; // Mirando a la izquierda

        // Cambia de dirección si alcanza un límite
        if (transform.position.x >= rightLimit.position.x)
        {
            moveDirection = Vector2.left;
        }
        else if (transform.position.x <= leftLimit.position.x)
        {
            moveDirection = Vector2.right;
        }
    }

    void DetectPlayerAndShoot()
    {
        Collider2D detectedPlayer = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (detectedPlayer)
        {
            player = detectedPlayer.transform;
            animator.SetTrigger("Attack"); // Activa la animación de disparo

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

    public void TakeDamage()
    {
        if (!isDead)
        {
            isDead = true;
            animator.SetTrigger("Death"); // Activa la animación de muerte
            Destroy(gameObject, 1f); // Destruye al enemigo tras la animación
        }
    }
}
