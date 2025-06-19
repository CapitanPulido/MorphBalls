using UnityEngine;

public class CambiarEstadoRigidBody : MonoBehaviour
{
    [Header("Objeto a cambiar")]
    public Rigidbody2D objetoACambiar;
    public PolyMorph morph;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (morph.Metal && other.CompareTag("Pelota") && objetoACambiar != null)
        {
            objetoACambiar.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("¡Pelota detectada! Estado cambiado a Dynamic.");
        }
    }
}
