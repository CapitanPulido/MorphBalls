using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class BolaSimple : MonoBehaviour
{
    public Slider speedometer; // Referencia al Slider
    public TextMeshProUGUI speedText; // Referencia al TextMeshPro

    private Rigidbody2D rb;

    public float maxSpeed = 10f; // Velocidad máxima esperada
    public float moveForce = 5f; // Fuerza de movimiento
    public float groundCheckDistance = 0.1f; // Distancia para detectar el suelo
    public LayerMask groundLayer; // Capa del suelo

    private float speed; // Velocidad actual de la pelota

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (speedometer != null)
        {
            speedometer.minValue = 0;
            speedometer.maxValue = maxSpeed;
        }
    }

    void Update()
    {
        Mover();

        speed = rb.velocity.magnitude; // Calcula la magnitud de la velocidad

        // Actualiza el Slider con la velocidad
        if (speedometer != null)
        {
            speedometer.value = speed;
        }

        // Actualiza el TextMeshPro con la velocidad
        if (speedText != null)
        {
            speedText.text = "Velocidad: " + speed.ToString("F2"); // Muestra la velocidad con 2 decimales
        }
    }

    void Mover()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal"); // Captura el input horizontal

        if (EnSuelo())
        {
            // Ajusta la fuerza en la dirección del suelo
            Vector2 direccionSuelo = ObtenerDireccionSuelo();
            rb.AddForce(direccionSuelo * movimientoHorizontal * moveForce, ForceMode2D.Force);
        }
        else
        {
            // Movimiento libre en el aire
            rb.AddForce(Vector2.right * movimientoHorizontal * moveForce, ForceMode2D.Force);
        }
    }

    bool EnSuelo()
    {
        // Detecta si la bola está tocando el suelo
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    Vector2 ObtenerDireccionSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            return Vector2.Perpendicular(hit.normal).normalized * Mathf.Sign(Input.GetAxis("Horizontal"));
        }
        return Vector2.right;
    }

    void OnDrawGizmos()
    {
        // Dibuja el raycast en la escena para visualizar la detección del suelo
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
