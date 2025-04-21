using UnityEngine;

public class PlataformaBasculante : MonoBehaviour
{
    private Rigidbody2D rb;

    public PolyMorph morph;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {

        if (morph.Plastico == true)
        {
            // Si es pl�stico, congelamos la rotaci�n
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            // Si no es pl�stico, aseguramos que est� desbloqueada la rotaci�n
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pelota"))
        {
            PolyMorph morph = collision.collider.GetComponent<PolyMorph>();

            if (morph != null)
            {
                // Cuando se va cualquier pelota, desbloqueamos la rotaci�n
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
    }
}
