using UnityEngine;

public class PolyMorph : MonoBehaviour
{
    public Rigidbody2D rb;                // Referencia al Rigidbody2D
    public Texture2D[] texturas;          // Arreglo de texturas (arrástralas en el inspector)
    public Renderer render;               // Renderer del objeto (para cambiar la textura)

    private int contador = 0;             // Cuenta las veces que se presiona la tecla

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (render == null)
            render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            contador++;

            // Ajustar el valor entre 1 y 3
            contador = Mathf.Clamp(contador, 1, 3);

            // Cambiar la masa
            rb.mass = contador;

            // Cambiar la textura si está disponible
            if (texturas.Length >= contador)
            {
                render.material.mainTexture = texturas[contador - 1];
            }

            Debug.Log("Masa actual: " + rb.mass);
        }
    }
}
