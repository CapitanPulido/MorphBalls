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
    public float friction = 0.95f; // Fricción para reducir la velocidad

    public Sprite spritewood;
    public Sprite spritemetal;
    public Sprite spriteplastic;

    public SpriteRenderer render;

    private float speed; // Velocidad actual de la pelota
    private float moveDirection = 0f; // Dirección del movimiento
    public bool enSuelo = false; // Bandera para saber si está tocando el suelo

    public bool left = false;
    public bool right = false;

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

        // Calcula la magnitud de la velocidad
        speed = rb.velocity.magnitude;

        // Actualiza el Slider con la velocidad
        if (speedometer != null)
        {
            speedometer.value = speed;
        }

        // Actualiza el TextMeshPro con la velocidad
        if (speedText != null)
        {
            speedText.text = "Velocidad: " + speed.ToString("F2");
        }

        // Si no se mueve y está en el suelo, aplica fricción para frenar
        if (moveDirection == 0 && enSuelo == true)
        {
            rb.velocity *= friction;
            if (rb.velocity.magnitude < 0.1f)
            {
                rb.velocity = Vector2.zero; // Detiene por completo si la velocidad es muy baja
            }
        }
    }

    void Mover()
    {
        rb.AddForce(Vector2.right * moveDirection * moveForce, ForceMode2D.Force);
    }

    // Detecta si toca el suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }

    // Métodos para cambiar la gravedad y el sprite
    public void GravityWood()
    {
        rb.mass = 5;
        render.sprite = spritewood;
    }

    public void GravityMetal()
    {
        rb.mass = 20;
        render.sprite = spritemetal;
    }

    public void GravityPlastic()
    {
        rb.mass = 1;
        render.sprite = spriteplastic;
    }

    // Métodos para mover la bola
    public void MoveLeft()
    {
        moveDirection = -1f;
        left = true;
        right = false;
    }

    public void MoveRight()
    {
        moveDirection = 1f;
        right = true;
        left = false;
    }

    public void StopMoving()
    {
        moveDirection = 0f;
    }
}