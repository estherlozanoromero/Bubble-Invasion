using UnityEngine;
using UnityEngine.UI; // Necesario si usas una barra de vida

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; // Vida máxima del jugador
    private int currentHealth;

    public Image healthBar; // Opcional: Barra de vida en UI
    public GameObject deathEffect; // Efecto al morir (opcional)

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // No tomar daño si ya está muerto

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evita que sea menor a 0
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Jugador ha muerto");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Opcional: Desactivar al jugador tras la muerte
        gameObject.SetActive(false);

        // Aquí puedes agregar lógica para reiniciar el nivel o mostrar un Game Over
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
