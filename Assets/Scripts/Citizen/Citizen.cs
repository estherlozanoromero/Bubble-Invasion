using UnityEngine;

public class Citizen : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform leftLimit; // Límite izquierdo
    public Transform rightLimit; // Límite derecho

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator animator;
    private bool isTrapped = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicia moviéndose a la derecha
        moveDirection = Vector2.right;
    }

    private void Update()
    {
        if (!isTrapped)
        {
            rb.linearVelocity = moveDirection * moveSpeed;

            // Activa animación de caminar si se mueve
            animator.SetBool("Walk", rb.linearVelocity.magnitude > 0.1f);

            // Gira el sprite en la dirección del movimiento
            if (moveDirection.x > 0)
                spriteRenderer.flipX = false; // Mirando a la derecha
            else if (moveDirection.x < 0)
                spriteRenderer.flipX = true; // Mirando a la izquierda

            // Cambiar dirección al llegar a los límites
            if (transform.position.x >= rightLimit.position.x)
            {
                moveDirection = Vector2.left; // Ir a la izquierda
            }
            else if (transform.position.x <= leftLimit.position.x)
            {
                moveDirection = Vector2.right; // Ir a la derecha
            }
        }
    }

    public void GetTrapped(Transform bubble)
    {
        isTrapped = true;
        transform.SetParent(bubble);
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Captured");
    }

    public void GetReleased()
    {
        isTrapped = false;
        transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;
        animator.SetTrigger("Released");
        Invoke("ReturnToWalk", 1f); // Volver a caminar después de 1 segundo
    }

    private void ReturnToWalk()
    {
        animator.Play("Walk"); // Asegurar que la animación vuelva a Walk
    }
}
