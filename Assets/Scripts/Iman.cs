using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float velocidadDeAtraccion = 5f;
    public Vector2 offsetDelCentro = Vector2.zero;

    public PolyMorph morph;  // Estado del material (metal, madera, pl�stico)
    public BolaSimple bola;  // Referencia a la pelota

    public float fuerzaLanzamiento = 15f;  // Fuerza con la que lanzar la pelota
    public float velocidadLanzamiento = 10f;  // Velocidad de lanzamiento

    private bool enrango = false;  // Si la pelota est� dentro del rango del im�n
    private Rigidbody2D rb;  // El Rigidbody de la pelota

    void Start()
    {
        rb = bola.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (enrango && morph.Metal)
        {
            // Si la pelota es de metal, seguimos con la atracci�n
            AtraerPelota();
        }
        else if (enrango && !morph.Metal)
        {
            // Si la pelota deja de ser de metal, la lanzamos
            LanzarPelota();
        }
    }

    void AtraerPelota()
    {
        // Si la pelota es de metal, la atraemos hacia el im�n
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;

        // Movimiento suave hacia el punto de atracci�n
        bola.transform.localPosition = Vector2.Lerp(
            bola.transform.localPosition,
            offsetDelCentro,
            Time.deltaTime * velocidadDeAtraccion
        );
    }

    void LanzarPelota()
    {
        // Calcular la direcci�n de lanzamiento solo en el eje X
        float direccionX = bola.transform.position.x - transform.position.x;
        Vector2 direccion = new Vector2(direccionX, 0).normalized;  // Solo direcci�n en el eje X

        // Llamamos al m�todo que lanza la pelota
        // Aplicar una fuerza en la direcci�n calculada, para lanzar la pelota
        rb.velocity = new Vector2(direccion.x * velocidadLanzamiento, rb.velocity.y); // Mantener la velocidad en Y

        // A�adir un impulso extra en la direcci�n de la velocidad de lanzamiento
        rb.AddForce(direccion * fuerzaLanzamiento, ForceMode2D.Impulse);

        // Restablecer la gravedad despu�s de lanzar la pelota
        rb.gravityScale = 1f;

        // Desactivar el emparentamiento de la pelota al im�n
        bola.transform.SetParent(null);
        enrango = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pelota") && morph.Metal)
        {
            enrango = true;
            rb.gravityScale = 0f;

            // Hacer hija del im�n
            bola.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pelota"))
        {
            enrango = false;

            // Restablecer la gravedad de la pelota
            rb.gravityScale = 7f;

            // Liberar la pelota del im�n
            bola.transform.SetParent(null);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector2 punto = (Vector2)transform.position + offsetDelCentro;
        Gizmos.DrawWireSphere(punto, 1f);  // Representa la posici�n del im�n
    }
}
