using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float riseSpeed = 1f;
    private GameObject trappedCharacter;

    private void Update()
    {
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
    }

    public void Capture(GameObject character)
    {
        trappedCharacter = character;
        character.transform.SetParent(transform);
        character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void BreakBubble()
    {
        if (trappedCharacter)
        {
            trappedCharacter.transform.SetParent(null);
            trappedCharacter.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        Destroy(gameObject);
    }
}
