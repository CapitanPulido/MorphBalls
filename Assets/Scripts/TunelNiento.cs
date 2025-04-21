using UnityEngine;

public class TunelDeViento : MonoBehaviour
{
    [Header("Alturas por estado")]
    public float alturaMadera = 4f;
    public float alturaPlastico = 5.5f;
    public float alturaMetal = 2f;

    [Header("Fuerzas por estado")]
    public float fuerzaMadera = 12f;
    public float fuerzaPlastico = 15f;
    public float fuerzaMetal = 8f;

    [Header("Centro del Objeto (ajustable en el Inspector)")]
    public Vector3 centroDelObjeto = Vector3.zero; // Centro ajustable desde el Inspector

    [Header("General")]
    public float margen = 0.2f;
    public float fuerzaHorizontal = 5f;

    public float energiaAcumulada = 0f;
    public float maxEnergiaAcumulada = 15f;
    public float velocidadAcumulacion = 20f;
    public float impulsoLiberado = 12f;

    public PolyMorph morph;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null && morph != null)
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.2f);

                float alturaDeseada = 0f;
                float fuerzaVertical = 0f;
                float diferenciaAltura;

                // Detectamos estado seg�n las booleans
                if (morph.Madera == true)
                {
                    alturaDeseada = alturaMadera;
                    fuerzaVertical = fuerzaMadera;
                }
                else if (morph.Plastico == true)
                {
                    alturaDeseada = alturaPlastico;
                    fuerzaVertical = fuerzaPlastico;
                }
                else if (morph.Metal == true)
                {
                    alturaDeseada = alturaMetal;
                    fuerzaVertical = fuerzaMetal;
                }

                diferenciaAltura = alturaDeseada - rb.position.y;

                if (Mathf.Abs(diferenciaAltura) > margen)
                {
                    rb.AddForce(new Vector2(0f, diferenciaAltura * fuerzaVertical));
                }

                // Mantener al centro horizontalmente (opcional)
                float diferenciaX = transform.position.x - rb.position.x;
                rb.AddForce(new Vector2(diferenciaX * fuerzaHorizontal, 0f));
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
                rb.gravityScale = 1f; // Restaurar gravedad normal
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origen = transform.position + centroDelObjeto; // Ajustamos el origen con el centro definido

        // Direcci�n del viento (tomando en cuenta la rotaci�n del objeto)
        Vector3 direccionViento = transform.up.normalized; // Usamos up para la direcci�n del viento

        // === GIZMO DE CENTRO ===
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origen, 0.1f); // Punto central del t�nel

        // === L�NEAS DE ALTURA ===
        // L�nea para Madera
        Gizmos.color = Color.green;
        Vector3 alturaMaderaPos = origen + direccionViento * alturaMadera;
        Gizmos.DrawLine(alturaMaderaPos - transform.right * 1.5f, alturaMaderaPos + transform.right * 1.5f);

        // L�nea para Pl�stico
        Gizmos.color = Color.yellow;
        Vector3 alturaPlasticoPos = origen + direccionViento * alturaPlastico;
        Gizmos.DrawLine(alturaPlasticoPos - transform.right * 1.5f, alturaPlasticoPos + transform.right * 1.5f);

        // L�nea para Metal
        Gizmos.color = Color.gray;
        Vector3 alturaMetalPos = origen + direccionViento * alturaMetal;
        Gizmos.DrawLine(alturaMetalPos - transform.right * 1.5f, alturaMetalPos + transform.right * 1.5f);

        // === OPCIONAL: L�nea de direcci�n del viento (visualizaci�n de la direcci�n) ===
        Gizmos.color = new Color(1, 1, 1, 0.3f);
        Gizmos.DrawLine(origen - direccionViento * 5f, origen + direccionViento * 5f);
    }
}
