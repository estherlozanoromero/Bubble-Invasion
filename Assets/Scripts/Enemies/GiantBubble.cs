using UnityEngine;

public class GiantBubble : MonoBehaviour
{
    public float riseSpeed = 1f;
    private GameObject trappedCitizen;

    private void Update()
    {
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
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
        trappedCitizen.transform.SetParent(transform);
        trappedCitizen.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void BreakBubble() // Si el jugador dispara, se libera al ciudadano
    {
        if (trappedCitizen)
        {
            trappedCitizen.transform.SetParent(null);
            trappedCitizen.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        Destroy(gameObject);
    }
}
