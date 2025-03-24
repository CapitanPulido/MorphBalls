using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // El jugador a seguir
    public float smoothSpeed = 0.125f; // Ajusta la suavidad del seguimiento
    public Vector3 offset; // Para ajustar la posición de la cámara

    private Vector3 velocity = Vector3.zero; // Usado para SmoothDamp

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
    }
}
