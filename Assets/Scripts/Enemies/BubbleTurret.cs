using UnityEngine;

public class BubbleTurret : MonoBehaviour
{
    public GameObject bubblePrefab; // Prefab de la burbuja gigante
    public Transform firePoint; // Lugar desde donde dispara
    public float fireRate = 3f; // Tiempo entre disparos
    public float detectionRange = 5f; // Rango de detección de ciudadanos
    public LayerMask citizenLayer;
    private Animator animator;
    private float nextFireTime = 0f;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead)
        {
            DetectCitizenAndShoot();
        }
    }

    void DetectCitizenAndShoot()
    {
        Collider2D[] citizens = Physics2D.OverlapCircleAll(transform.position, detectionRange, citizenLayer);

        if (citizens.Length > 0)
        {
            Transform targetCitizen = citizens[0].transform;

            if (Time.time >= nextFireTime)
            {
                ShootBubble(targetCitizen);
                animator.SetTrigger("Attack"); // Activa animación de disparo
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ShootBubble(Transform target)
    {
        GameObject bubble = Instantiate(bubblePrefab, firePoint.position, Quaternion.identity);
        bubble.GetComponent<GiantBubble>().SetTarget(target);
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            isDead = true;
            animator.SetTrigger("Death");
            Destroy(gameObject, 1f); // Se destruye después de la animación
        }
    }
}
