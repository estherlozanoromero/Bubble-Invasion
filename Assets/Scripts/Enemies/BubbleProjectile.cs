using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    private Animator animator;
    private bool isDestroying = false; // Para evitar m�ltiples llamadas

    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifetime); // Autodestrucci�n tras un tiempo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroying) return; // Evita ejecutar el c�digo m�s de una vez

        if (collision.CompareTag("Player")) // Si golpea al jugador
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage); // Hace da�o
            TriggerDestroy(); // Activa la animaci�n de destrucci�n
        }
        else if (collision.CompareTag("Wall")) // Si choca contra una pared
        {
            TriggerDestroy(); // Activa la animaci�n antes de desaparecer
        }
    }

    void TriggerDestroy()
    {
        isDestroying = true;
        animator.SetTrigger("Destroy"); // Activa la animaci�n de salida
        Destroy(gameObject, 0.5f); // Se destruye despu�s de la animaci�n (ajusta el tiempo si es necesario)
    }
}
