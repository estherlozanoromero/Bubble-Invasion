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

    private void OnCollisionEnter2D(Collision2D collision) // Ahora usamos colisiones físicas
    {
        if (collision.gameObject.CompareTag("GiantBubble"))
        {
            collision.gameObject.GetComponent<GiantBubble>()?.BreakBubble(); // Rompe la burbuja
        }
        else if (collision.gameObject.CompareTag("BubbleTurret"))
        {
            collision.gameObject.GetComponent<BubbleTurret>()?.TakeDamage(); // Destruye la torreta
        }
        else if (collision.gameObject.CompareTag("BubbleShooter"))
        {
            collision.gameObject.GetComponent<BubbleShooter>()?.TakeDamage(); // Destruye el enemigo que dispara burbujas
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Si choca contra una pared, simplemente se destruye
        }

        Destroy(gameObject); // La bala se destruye tras el impacto
    }
}


