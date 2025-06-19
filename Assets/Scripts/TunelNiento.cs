using UnityEngine;

public class TunelDeViento : MonoBehaviour
{
    [Header("Alturas por estado")]
    public float alturaMadera ;
    public float alturaPlastico;
    public float alturaMetal;

    [Header("Fuerzas por estado")]
    public float fuerzaMadera;
    public float fuerzaPlastico;
    public float fuerzaMetal;

    [Header("Centro del Objeto")]
    public Vector3 centroDelObjeto = Vector3.zero;

    [Header("General")]
    public float margen = 0.2f;
    public float fuerzaHorizontal;

    public float energiaAcumulada;
    public float maxEnergiaAcumulada;
    public float velocidadAcumulacion;

    public PolyMorph morph;

    private Rigidbody2D rbPelota;
    private bool dentroDelTunel = false;
    private bool impulsoAplicado = false;

    private void Update()
    {
        if (!dentroDelTunel || rbPelota == null) return;

        float alturaObjetivo = 0f;
        float fuerzaVertical = 0f;

        // MADERA O METAL: acumulamos energía
        if (morph.Madera)
        {
            alturaObjetivo = alturaMadera;
            fuerzaVertical = fuerzaMadera;
            energiaAcumulada = Mathf.Min(energiaAcumulada + velocidadAcumulacion * Time.deltaTime, maxEnergiaAcumulada);
            impulsoAplicado = false; // Reseteamos si vuelve a madera
        }
        else if (morph.Metal)
        {
            alturaObjetivo = alturaMetal;
            fuerzaVertical = fuerzaMetal;
            energiaAcumulada = Mathf.Min(energiaAcumulada + velocidadAcumulacion * Time.deltaTime, maxEnergiaAcumulada);
            impulsoAplicado = false; // Reseteamos si vuelve a metal
        }
        else if (morph.Plastico)
        {
            alturaObjetivo = alturaPlastico;
            fuerzaVertical = fuerzaPlastico;

            float alturaActual = rbPelota.position.y;
            if (!impulsoAplicado && Mathf.Abs(alturaActual - alturaPlastico) <= margen)
            {
                rbPelota.AddForce(new Vector2(0f, energiaAcumulada), ForceMode2D.Impulse);
                energiaAcumulada = 0f;
                impulsoAplicado = true;
            }
        }

        // Mantener altura
        float diferenciaAltura = alturaObjetivo - rbPelota.position.y;
        if (Mathf.Abs(diferenciaAltura) > margen)
        {
            rbPelota.AddForce(new Vector2(0f, diferenciaAltura * fuerzaVertical));
        }

        // Mantener centrado en X
        float diferenciaX = transform.position.x - rbPelota.position.x;
        rbPelota.AddForce(new Vector2(diferenciaX * fuerzaHorizontal, 0f));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            rbPelota = other.GetComponent<Rigidbody2D>();
            if (rbPelota != null)
            {
                rbPelota.gravityScale = 0f;
                rbPelota.velocity = Vector2.Lerp(rbPelota.velocity, Vector2.zero, 0.5f);
                dentroDelTunel = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            if (rbPelota != null)
            {
                rbPelota.gravityScale = 10f;
            }
            rbPelota = null;
            dentroDelTunel = false;
            energiaAcumulada = 0f;
            impulsoAplicado = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origen = transform.position + centroDelObjeto;
        Vector3 direccionViento = transform.up.normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origen, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origen + direccionViento * alturaMadera - transform.right * 1.5f,
                        origen + direccionViento * alturaMadera + transform.right * 1.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origen + direccionViento * alturaPlastico - transform.right * 1.5f,
                        origen + direccionViento * alturaPlastico + transform.right * 1.5f);

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(origen + direccionViento * alturaMetal - transform.right * 1.5f,
                        origen + direccionViento * alturaMetal + transform.right * 1.5f);

        Gizmos.color = new Color(1, 1, 1, 0.3f);
        Gizmos.DrawLine(origen - direccionViento * 5f, origen + direccionViento * 5f);
    }
}
