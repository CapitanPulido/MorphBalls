using UnityEngine;

public class TunelDeVientoHorizontal : MonoBehaviour
{
    [Header("Posiciones X por estado")]
    public float posicionMadera = 4f;
    public float posicionPlastico = 5.5f;
    public float posicionMetal = 2f;

    [Header("Fuerzas por estado")]
    public float fuerzaMadera = 12f;
    public float fuerzaPlastico = 15f;
    public float fuerzaMetal = 8f;

    [Header("Centro del Objeto (ajustable en el Inspector)")]
    public Vector3 centroDelObjeto = Vector3.zero;

    [Header("General")]
    public float margen = 0.2f;
    public float fuerzaVertical = 5f; // Ahora esto será el "centrado" vertical
    public PolyMorph morph;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null && morph != null)
            {
                rb.gravityScale = 1f;
                //rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.2f);

                float posicionDeseada = 0f;
                float fuerzaHorizontal = 0f;
                float diferencia;

                if (morph.Madera == true)
                {
                    posicionDeseada = posicionMadera;
                    fuerzaHorizontal = fuerzaMadera;
                }
                else if (morph.Plastico == true)
                {
                    posicionDeseada = posicionPlastico;
                    fuerzaHorizontal = fuerzaPlastico;
                }
                else if (morph.Metal == true)
                {
                    posicionDeseada = posicionMetal;
                    fuerzaHorizontal = fuerzaMetal;
                }

                diferencia = posicionDeseada - rb.position.x;

                if (Mathf.Abs(diferencia) > margen)
                {
                    rb.AddForce(new Vector2(diferencia * fuerzaHorizontal, 0f));
                }

                // Mantener al centro verticalmente
                float diferenciaY = transform.position.y - rb.position.y;
                rb.AddForce(new Vector2(0f, diferenciaY * fuerzaVertical));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origen = transform.position + centroDelObjeto;
        Vector3 direccionViento = transform.right.normalized;

        // Centro
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origen, 0.1f);

        // Madera
        Gizmos.color = Color.green;
        Vector3 posMadera = origen + direccionViento * posicionMadera;
        Gizmos.DrawLine(posMadera - transform.up * 1.5f, posMadera + transform.up * 1.5f);

        // Plástico
        Gizmos.color = Color.yellow;
        Vector3 posPlastico = origen + direccionViento * posicionPlastico;
        Gizmos.DrawLine(posPlastico - transform.up * 1.5f, posPlastico + transform.up * 1.5f);

        // Metal
        Gizmos.color = Color.gray;
        Vector3 posMetal = origen + direccionViento * posicionMetal;
        Gizmos.DrawLine(posMetal - transform.up * 1.5f, posMetal + transform.up * 1.5f);

        // Línea de dirección del viento
        Gizmos.color = new Color(1, 1, 1, 0.3f);
        Gizmos.DrawLine(origen - direccionViento * 5f, origen + direccionViento * 5f);
    }
}

