using UnityEngine;

public class Venti : MonoBehaviour
{
    public Vector2 direccionAire = Vector2.up;
    public float fuerzaAire = 10f;

    public LayerMask capaObstaculos;

    public Rigidbody2D rb;

    public PolyMorph morph;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            if (rb != null && morph != null)
            {
                // Verificamos si hay algo obstruyendo el aire entre la fuente y la pelota
                Vector2 origen = transform.position;
                Vector2 destino = other.transform.position;
                Vector2 direccion = (destino - origen).normalized;
                float distancia = Vector2.Distance(origen, destino);

                RaycastHit2D hit = Physics2D.Raycast(origen, direccion, distancia, capaObstaculos);
                if (!hit)
                {
                    // Aplica aire según el material
                    if (morph.Plastico == true)
                    {
                        rb.AddForce(direccionAire.normalized * fuerzaAire, ForceMode2D.Force);
                    }
                    else if (morph.Madera == true)
                    {
                        rb.AddForce(direccionAire.normalized * (fuerzaAire * 0.5f), ForceMode2D.Force);
                    }
                    else if (morph.Metal == true)
                    {
                        // Solo si está tocando el suelo
                        RaycastHit2D enSuelo = Physics2D.Raycast(rb.position, Vector2.down, 0.1f, capaObstaculos);
                        if (enSuelo)
                        {
                            rb.velocity = Vector2.zero;
                            rb.AddForce(direccionAire.normalized * fuerzaAire * 2f, ForceMode2D.Impulse);
                        }
                    }
                }
            }
        }
    }
}



