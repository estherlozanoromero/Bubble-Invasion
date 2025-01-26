using UnityEngine;

public class GiantBubble : MonoBehaviour
{
    public float moveSpeed = 2f;
    private GameObject trappedCitizen;
    private Transform targetCitizen;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (targetCitizen)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetCitizen.position, moveSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform citizen)
    {
        targetCitizen = citizen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Citizen")) // Si toca a un ciudadano, lo atrapa
        {
            CaptureCitizen(collision.gameObject);
        }
    }

    void CaptureCitizen(GameObject citizen)
    {
        trappedCitizen = citizen;
        trappedCitizen.GetComponent<Citizen>().GetTrapped(transform);
        animator.SetTrigger("Trapped");
    }

    public void BreakBubble()
    {
        if (trappedCitizen)
        {
            trappedCitizen.GetComponent<Citizen>().GetReleased();
        }
        animator.SetTrigger("Destroyed");
        Destroy(gameObject, 0.5f);
    }
}
