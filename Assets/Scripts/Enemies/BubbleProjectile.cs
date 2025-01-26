using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    private Animator animator;
    private bool isDestroying = false; // Para evitar múltiples llamadas

    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifetime); // Autodestrucción tras un tiempo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroying) return; // Evita ejecutar el código más de una vez

        if (collision.CompareTag("Player")) // Si golpea al jugador
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage); // Hace daño
            TriggerDestroy(); // Activa la animación de destrucción
        }
        else if (collision.CompareTag("Wall")) // Si choca contra una pared
        {
            TriggerDestroy(); // Activa la animación antes de desaparecer
        }
    }

    void TriggerDestroy()
    {
        isDestroying = true;
        animator.SetTrigger("Destroy"); // Activa la animación de salida
        Destroy(gameObject, 0.5f); // Se destruye después de la animación (ajusta el tiempo si es necesario)
    }
}
