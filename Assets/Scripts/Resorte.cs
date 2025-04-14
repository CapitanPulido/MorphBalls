using UnityEngine;

public class Spring : MonoBehaviour
{
    public float fuerzaMetal = -15f;
    public float fuerzaMadera = -10f;
    public float fuerzaPlastico = -5f;

    public PolyMorph morph;
    public float fuerza;

    public void Update()
    {
        if (morph.Madera)
        {
            fuerza = fuerzaMadera;
        }
        if (morph.Plastico)
        {
            fuerza = fuerzaPlastico;
        }
        if (morph.Metal)
        {
            fuerza = fuerzaMetal;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        BolaSimple bola = other.GetComponent<BolaSimple>();

        if (bola != null)
        {
            Rigidbody2D rb = bola.GetComponent<Rigidbody2D>(); 
            rb.velocity = new Vector2(rb.velocity.x, fuerza);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(2, .5f, 0));
    }
}