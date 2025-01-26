using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
    public Transform target; // El transform del personaje principal que la cámara debe seguir
    public Vector3 offset = new Vector3(0, 0, -10); // Offset de la cámara
    public float smoothSpeed = 0.125f; // Velocidad de suavizado

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada
            Vector3 desiredPosition = target.position + offset;

            // Suaviza el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualiza la posición de la cámara
            transform.position = smoothedPosition;
        }
    }
}
