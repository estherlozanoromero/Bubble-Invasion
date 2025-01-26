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
        // Determinar offsets según la animación activa
        Vector3 offset = Vector3.zero;
        if (animator.GetBool("crouch"))
        {
            offset = new Vector3(playerMovement.isFacingRight ? attackRange : -attackRange, 0.33f, 0f); // Offset actual
        }
        else if (animator.GetBool("look-up"))
        {
            offset = new Vector3(playerMovement.isFacingRight ? 0f : 0f, 2.5f, 0f); // Offset para look-up
        }
        else
        {
            offset = new Vector3(playerMovement.isFacingRight ? attackRange : -attackRange, 1.15f, 0f); // Offset para ataque normal
        }

        // Calcular la posición del ataque
        Vector3 attackPoint = playerTransform.position + offset;

        // Generar la bala en la posición calculada
        if (attackPrefab != null)
        {
            GameObject bullet = Instantiate(attackPrefab, attackPoint, Quaternion.identity);

            // Obtener el script Bullet y pasarle la dirección del movimiento
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(playerMovement.isFacingRight, animator.GetBool("look-up"));
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
