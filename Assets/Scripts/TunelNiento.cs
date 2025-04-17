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

                // Detectamos estado según las booleans
                if (morph.Madera == true)
                {
                    alturaDeseada = alturaMadera;
                    fuerzaVertical = fuerzaMadera;
                }
                else if (morph.Plastico == true)
                {

                        alturaDeseada = alturaPlastico;
                        fuerzaVertical = fuerzaPlastico;

                         diferenciaAltura = alturaDeseada - rb.position.y;

                        if (diferenciaAltura > margen)
                        {
                            // Acumulamos energía si está debajo de la altura deseada
                            energiaAcumulada += Time.deltaTime * velocidadAcumulacion;
                            energiaAcumulada = Mathf.Clamp(energiaAcumulada, 0f, maxEnergiaAcumulada);

                            // En lugar de fuerza proporcional, aplicamos una fuerza constante
                            rb.AddForce(new Vector2(0f, fuerzaVertical));
                        }
                        else if (energiaAcumulada > 0)
                        {
                            // Liberamos el impulso hacia arriba de golpe
                            rb.AddForce(new Vector2(0f, impulsoLiberado * energiaAcumulada), ForceMode2D.Impulse);
                            energiaAcumulada = 0f;
                        }
                    

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
                rb.gravityScale = 10f; // Restaurar gravedad normal
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Mostrar las alturas por tipo de material
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x - 1.5f, alturaMadera, 0), new Vector3(transform.position.x + 1.5f, alturaMadera, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x - 1.5f, alturaPlastico, 0), new Vector3(transform.position.x + 1.5f, alturaPlastico, 0));

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(new Vector3(transform.position.x - 1.5f, alturaMetal, 0), new Vector3(transform.position.x + 1.5f, alturaMetal, 0));
    }
}

