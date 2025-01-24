using UnityEngine;

public class TrappedCitizen : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D characterCollider;
    private bool isTrapped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<Collider2D>();
    }

    public void GetTrapped(Transform bubble)
    {
        isTrapped = true;
        transform.SetParent(bubble); // Hace que el personaje se mueva con la burbuja
        rb.bodyType = RigidbodyType2D.Kinematic; // Evita que la f�sica lo afecte
        rb.linearVelocity = Vector2.zero; // Detiene el movimiento
        characterCollider.enabled = false; // Desactiva colisiones para evitar errores
    }

    public void GetReleased()
    {
        isTrapped = false;
        transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic; // Reactiva la f�sica
        characterCollider.enabled = true; // Reactiva colisiones
    }
}
