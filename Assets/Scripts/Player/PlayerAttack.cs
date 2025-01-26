using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab; // Prefab del ataque (espada, proyectil, etc.)
    public Transform playerTransform; // Transform del jugador
    public float attackRange = 1f; // Distancia del punto de ataque desde el jugador
    public float attackCooldown = 0.5f;

    private float nextAttackTime = 0f;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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

    // Generar la bala en la posición calculada
    if (attackPrefab != null)
    {
        // Instanciar la bala
        GameObject bullet = Instantiate(attackPrefab, attackPoint, Quaternion.identity);
        
        // Obtener el script Bullet y pasarle la dirección del movimiento
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(playerMovement.isFacingRight);
        }
    }

    Debug.Log("Ataque realizado en: " + attackPoint);
}
}
