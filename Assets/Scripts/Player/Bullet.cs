using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Velocidad de la bala
    public float lifetime = 5f;  // Tiempo antes de que la bala se destruya
    public bool isFacingRight; // Dirección en la que el jugador está mirando
    public bool isLookingUp; // Indica si el jugador está mirando hacia arriba

    private Rigidbody2D rb;

    // Método Initialize para recibir la dirección y estado
    public void Initialize(bool facingRight, bool lookingUp = false)
    {
        isFacingRight = facingRight;
        isLookingUp = lookingUp;
    }

    private void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        // Asegurarse de que la gravedad no afecte a la bala
        rb.gravityScale = 0;

        // Destruir la bala después del tiempo de vida especificado
        Destroy(gameObject, lifetime);

        // Determinar la dirección en la que se mueve la bala
        if (isLookingUp)
        {
            // Mover hacia arriba si el jugador está mirando hacia arriba
            rb.linearVelocity = Vector2.up * speed;
        }
        else if (isFacingRight)
        {
            // Mover hacia la derecha si el jugador está mirando a la derecha
            rb.linearVelocity = Vector2.right * speed;
        }
        else
        {
            // Mover hacia la izquierda si el jugador está mirando a la izquierda
            rb.linearVelocity = Vector2.left * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir la bala al colisionar con cualquier objeto
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si golpea una burbuja gigante, la destruye
        if (collision.CompareTag("GiantBubble"))
        {
            collision.GetComponent<GiantBubble>().BreakBubble(); // Rompe la burbuja y libera al ciudadano
            Destroy(gameObject); // La bala se destruye tras el impacto
        }

        // Si golpea un proyectil enemigo, lo destruye
        else if (collision.CompareTag("BubbleTurret"))
        {
            Destroy(collision.gameObject); // Destruye la torre enemiga
            Destroy(gameObject); // La bala del jugador también desaparece
        }

        // Si golpea una pared, la bala desaparece
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("BubbleShooter")) // Si golpea al BubbleShooter
        {
            collision.GetComponent<BubbleShooter>().TakeDamage(); // Mata al enemigo
            Destroy(gameObject); // La bala desaparece tras el impacto
        }
    }
}
