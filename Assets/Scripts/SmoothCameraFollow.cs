using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // Jugador
    public float smoothTime = 0.3f; // Tiempo de suavizado
    public Vector3 offset; // Ajuste de posición
    private Vector3 velocity = Vector3.zero; // Velocidad usada por SmoothDamp

    // Límites de la cámara (ajústalos según el tamaño del mapa)
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;

        // Nueva posición deseada de la cámara
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        // Suavizar el movimiento con SmoothDamp
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Restringir la posición dentro de los límites del mapa
        float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        // Aplicar la nueva posición restringida
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
