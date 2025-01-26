using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento horizontal
    public float jumpForce = 10f; // Fuerza del salto
    private bool isGrounded = false; // Verifica si el jugador está en el suelo
    private Rigidbody2D rb;
    private PhysicsMaterial2D noFrictionMaterial; // Material sin fricción
    public bool isFacingRight = true; // Dirección del personaje (true = derecha, false = izquierda)
    private Animator animator; // Referencia al Animator
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de disparo

    void Start()
    {
        // Obtener el Rigidbody2D del personaje
        rb = GetComponent<Rigidbody2D>();

        // Obtener el componente Animator
        animator = GetComponent<Animator>();

        // Crear un PhysicsMaterial2D para evitar fricción lateral
        noFrictionMaterial = new PhysicsMaterial2D { friction = 0f, bounciness = 0f };
        rb.sharedMaterial = noFrictionMaterial;
    }

    void Update()
    {
        // Movimiento horizontal y bloqueo cuando se presionan W o S
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1 (izquierda), 1 (derecha), 0 (ninguno)
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Bloquea el movimiento horizontal
        }

        // Cambiar dirección del personaje
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        // Salto y bloqueo cuando se presionan W o S
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Controlar animaciones
        animator.SetBool("isRunning", moveInput != 0 && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)); // True si hay movimiento
        animator.SetBool("crouch", Input.GetKey(KeyCode.S)); // True si se mantiene pulsada 'S'
        animator.SetBool("look-up", Input.GetKey(KeyCode.W)); // True si se mantiene pulsada 'W'

        // Disparo
        if (Input.GetKeyDown(KeyCode.F)) // Disparo con tecla 'F'
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Determinar la dirección del disparo
        Vector2 shootDirection;
        if (Input.GetKey(KeyCode.W))
        {
            shootDirection = Vector2.up; // Disparo hacia arriba
        }
        else
        {
            shootDirection = isFacingRight ? Vector2.right : Vector2.left; // Disparo lateral
        }

        // Crear el proyectil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Asignar velocidad al proyectil
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.linearVelocity = shootDirection * 10f; // Ajusta la velocidad del proyectil
    }

    private void Flip()
    {
        // Cambiar la dirección del personaje
        isFacingRight = !isFacingRight;

        // Voltear el personaje invirtiendo la escala en el eje X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si está tocando el suelo
        CheckGround(collision);
        // Evitar que se quede pegado en colisiones laterales
        PreventSticking(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Continuar verificando si está tocando el suelo
        CheckGround(collision);
        // Evitar que se quede pegado en colisiones laterales
        PreventSticking(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si deja de tocar el suelo, deshabilitar la bandera de "en el suelo"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void CheckGround(Collision2D collision)
    {
        // Verificar si el objeto tiene el tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Recorre los puntos de contacto
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Verificar si la normal apunta hacia arriba
                if (Vector2.Dot(contact.normal, Vector2.up) > 0.9f)
                {
                    isGrounded = true;
                    return; // No es necesario seguir revisando más contactos
                }
            }

            // Si no hay una colisión válida, desactivar el salto
            isGrounded = false;
        }
    }

    private void PreventSticking(Collision2D collision)
    {
        // Verificar los puntos de contacto para detectar colisiones laterales
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Si la normal apunta hacia la izquierda o la derecha (colisión lateral)
            if (Mathf.Abs(contact.normal.x) > 0.9f)
            {
                // Permitir que la gravedad continúe afectando al personaje
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Min(rb.linearVelocity.y, -0.1f));
            }
        }
    }
}
