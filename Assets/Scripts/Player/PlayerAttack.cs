using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab; // Prefab del ataque (espada, proyectil, etc.)
    public Transform playerTransform; // Transform del jugador
    public float attackRange = 1f; // Distancia del punto de ataque desde el jugador
    public float attackCooldown = 0.5f;

    private float nextAttackTime = 0f;
    private PlayerMovement playerMovement; // Referencia al script de movimiento del jugador
    private Animator animator; // Referencia al Animator

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>(); // Obtener el Animator del jugador
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void Attack()
    {
        // Determinar la posición del ataque basado en la dirección del personaje
        Vector3 attackPoint = playerTransform.position +
            (playerMovement.isFacingRight ? Vector3.right : Vector3.left) * attackRange;

        attackPoint.y += 0.33f;

        // Generar la bala en la posición calculada
        if (attackPrefab != null)
        {
            GameObject bullet = Instantiate(attackPrefab, attackPoint, Quaternion.identity);

            // Obtener el script Bullet y pasarle la dirección del movimiento
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(playerMovement.isFacingRight);
            }
        }

        // Activar la animación de disparo
        if (animator != null)
        {
            animator.SetBool("shot", true); // Establecer el parámetro `shot` en `true`
            Invoke(nameof(ResetShotAnimation), 0.05f); // Reiniciar el parámetro después de un pequeño retraso
        }

        Debug.Log("Ataque realizado en: " + attackPoint);
    }

    private void ResetShotAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("shot", false); // Volver a establecer `shot` en `false`
        }
    }
}
