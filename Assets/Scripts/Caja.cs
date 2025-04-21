using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    public PolyMorph morph;

    public GameObject parteIzquierda;
    public GameObject parteDerecha;
    public float tiempoParaDestruir = 3f;
    public float fuerzaSeparacion = 5f;
    private bool yaSeDividio = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (yaSeDividio) return;

        if (morph.Metal && collision.gameObject.CompareTag("Pelota"))
        {
            Debug.Log("choque");

            yaSeDividio = true;
            DividirPared();
        }
    }

    public void DividirPared()
    {
        // Separar las piezas y activar gravedad
        Rigidbody2D rbIzq = parteIzquierda.GetComponent<Rigidbody2D>();
        Rigidbody2D rbDer = parteDerecha.GetComponent<Rigidbody2D>();

        rbIzq.bodyType = RigidbodyType2D.Dynamic;
        rbDer.bodyType = RigidbodyType2D.Dynamic;

        // Separarlas con una fuerza
        rbIzq.AddForce(Vector2.left * fuerzaSeparacion, ForceMode2D.Impulse);
        rbDer.AddForce(Vector2.right * fuerzaSeparacion, ForceMode2D.Impulse);

        // Opcional: desactivar el objeto padre
        GetComponent<Collider2D>().enabled = false;

        // Destruir las piezas después de un tiempo
        Destroy(parteIzquierda, tiempoParaDestruir);
        Destroy(parteDerecha, tiempoParaDestruir);
        Destroy(gameObject, tiempoParaDestruir + 0.1f); // Limpieza del objeto padre
    }
}
