using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro
using System;

public class BolaSimple : MonoBehaviour
{
    public Slider speedometer; // Referencia al Slider
    public TextMeshProUGUI speedText; // Referencia al TextMeshPro
    public Transform Spawn;

    public Rigidbody2D rb;
    public PolyMorph morph;
    public Animator animator;

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
    public bool Destroyed = false;

    public float velocidad;
    public float detener = 0f;

    public CircleCollider2D col; // Referencia al Collider
    public GameObject Controles;

    // Materiales físicos
    public PhysicsMaterial2D woodMaterial;
    public PhysicsMaterial2D metalMaterial;
    public PhysicsMaterial2D plasticMaterial;


    public void Awake()
    {
        AutoReferenceUI();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (speedometer != null)
        {
            speedometer.minValue = 0;
            speedometer.maxValue = maxSpeed;
        }

        GravityWood();

    }
    public void AutoReferenceUI()
    {
        GameObject canvas = GameObject.Find("Controles"); // cambia el nombre según tu Canvas
        
        if (canvas != null)
        {
            speedometer = canvas.transform.Find("SliderVelocidad")?.GetComponent<Slider>();
            speedText = canvas.transform.Find("TextoVelocidad")?.GetComponent<TextMeshProUGUI>();
        }

        morph.GetComponent<PolyMorph>();
        spritewood = Resources.Load<Sprite>("PELOTAS/PM");
        spritemetal = Resources.Load<Sprite>("PELOTAS/PME");
        spriteplastic = Resources.Load<Sprite>("PELOTAS/PG");

        woodMaterial = Resources.Load<PhysicsMaterial2D>("PELOTAS/Wood");
        metalMaterial = Resources.Load<PhysicsMaterial2D>("PELOTAS/Metal");
        plasticMaterial = Resources.Load<PhysicsMaterial2D>("PELOTAS/Plastico");
        
    }


    void Update()
    {
        Mover();

        // Calcula la magnitud de la velocidad
        speed = rb.velocity.magnitude;

        // Limita la velocidad máxima si está en el suelo
        if (enSuelo && speed > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

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

        if (morph.Madera && collision.gameObject.CompareTag("Obstaculo"))
        {
            if(Destroyed == false)
            {
                StartCoroutine(MorirMadera());
            }          
        }

        if (morph.Plastico && collision.gameObject.CompareTag("Obstaculo"))
        {
            if (Destroyed == false)
            {
                StartCoroutine(MorirPlastico());
            }
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
        //rb.mass = 5;
        render.sprite = spritewood;
        animator.Play("Madera");
        col.sharedMaterial = woodMaterial;
        
    }

    public void GravityMetal()
    {
        //rb.mass = 20;
        render.sprite = spritemetal;
        animator.Play("Metal");
        col.sharedMaterial = metalMaterial;
    }

    public void GravityPlastic()
    {
        //rb.mass = 1;
        render.sprite = spriteplastic;
        animator.Play("Plastico");
        col.sharedMaterial = plasticMaterial;
        Debug.Log("BouncePlastic");
    }

    // Métodos para mover la bola
    public void MoveLeft()
    {
        moveDirection = -velocidad;
    }

    public void MoveRight()
    {
        moveDirection = velocidad;
        right = true;
        left = false;

        
    }

    public void StopMoving()
    {
        moveDirection = 0f;
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Agua"))
        {
            morph.DetectarAgua();
            //morph.AplicarPropiedadesMaterial();
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Agua"))
        {

        }
    }



    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Agua"))
        {
            morph.SaliAgua();
            rb.gravityScale = 10; // Restaurar la gravedad normal
        }
    }

    public IEnumerator MorirMadera()
    {
        animator.Play("Explocion");
        Destroyed = true;
        Controles.SetActive(false);
        yield return new WaitForSeconds(1);
        Controles.SetActive(true);
        Destroyed = false;
        animator.Play("Madera");
        transform.position = Spawn.transform.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    public IEnumerator MorirPlastico()
    {
        animator.Play("Explocion");
        Destroyed = true;
        Controles.SetActive(false);
        yield return new WaitForSeconds(1);
        Controles.SetActive(true);
        Destroyed = false;
        animator.Play("Plastico");
        transform.position = Spawn.transform.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }
}