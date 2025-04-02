using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float fuerzaAtraccion = 10f; // Fuerza con la que atrae el im�n
    public float rango = 5f; // Distancia m�xima de atracci�n
    public Vector2 direccion = Vector2.left; // Direcci�n de atracci�n

    public PolyMorph morph;

    public BolaSimple bola;

    public bool enrango = false;


    public void Update()
    {
       if(enrango == true)
        {
            Rigidbody2D rb = bola.GetComponent<Rigidbody2D>();
            Vector2 direccionAtraccion = (Vector2)transform.position + direccion * rango - rb.position;
            rb.AddForce(direccionAtraccion.normalized * fuerzaAtraccion);
            Debug.Log("BolaEnRango");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (morph.Metal && collision.gameObject.CompareTag("Pelota"))
        {
            enrango = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (morph.Metal && collision.gameObject.CompareTag("Pelota"))
        {
            enrango = false;
        }
    }

    void OnDrawGizmos()
    {
        // Dibujar la direcci�n del campo magn�tico
        Gizmos.color = Color.blue;
        Vector2 inicio = (Vector2)transform.position;
        Vector2 fin = inicio + direccion.normalized * rango;
        Gizmos.DrawLine(inicio, fin);
        Gizmos.DrawWireSphere(fin, 0.5f); // Representa el extremo del rango
    }
}