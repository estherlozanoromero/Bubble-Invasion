using UnityEngine;

public class Citizen : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator animator;
    private bool isTrapped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("ChangeDirection", 2f, 3f);
    }

    private void Update()
    {
        if (!isTrapped)
        {
            rb.linearVelocity = moveDirection * moveSpeed; // Corregido: rb.linearVelocity → rb.velocity
            animator.SetBool("Walk", rb.linearVelocity.magnitude > 0.1f); // Corregido: isWalking → Walk
        }
    }

    void ChangeDirection()
    {
        if (!isTrapped)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            moveDirection = new Vector2(randomX, randomY).normalized;
        }
    }

    public void GetTrapped(Transform bubble)
    {
        isTrapped = true;
        transform.SetParent(bubble);
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero; // Detener el movimiento
        animator.SetBool("Walk", false); // Detener animación de caminar
        animator.SetTrigger("Captured"); // Activar animación de atrapado
    }

    public void GetReleased()
    {
        isTrapped = false;
        transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;
        animator.SetTrigger("Released"); // Activar animación de liberado
    }
}
