using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 scale = transform.localScale;
        scale.x = worldScreenWidth / sr.sprite.bounds.size.x;
        scale.y = worldScreenHeight / sr.sprite.bounds.size.y;
        transform.localScale = scale;
    }
}
