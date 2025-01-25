using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Velocidad de la bala
    public float lifetime = 5f;  // Tiempo antes de que la bala se destruya
    public bool isFacingRight; // Dirección en la que el jugador está mirando

    private Rigidbody2D rb;

    // Método Initialize para recibir la dirección
    public void Initialize(bool facingRight)
    {
        isFacingRight = facingRight;
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
        if (isFacingRight)
        {
            // Mover hacia la derecha si el jugador está mirando a la derecha
            rb.linearVelocity = Vector2.right * speed;  // Mover en la dirección de 'right'
        }
        else
        {
            // Mover hacia la izquierda si el jugador está mirando a la izquierda
            rb.linearVelocity = Vector2.left * speed;  // Mover en la dirección opuesta
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir la bala al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}
