using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja2 : MonoBehaviour
{
    private Vector3 originalScale; // Almacena la escala original de la caja
    public float crushTime = 0.2f; // Tiempo en que se aplasta la caja
    public float destroyDelay = 0.5f; // Tiempo antes de destruir la caja

    void Start()
    {
        originalScale = transform.localScale; // Guarda la escala inicial
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pelota")) // Verifica si es la pelota
        {
            BolaSimple bola = collision.gameObject.GetComponent<BolaSimple>();

            if (bola != null && bola.col.sharedMaterial == bola.metalMaterial)
            {
                StartCoroutine(CrushAndDestroy());
            }
        }
    }

    IEnumerator CrushAndDestroy()
    {
        // Aplastar la caja reduciendo su escala en Y
        float elapsedTime = 0f;
        Vector3 targetScale = new Vector3(originalScale.x, originalScale.y * 0.2f, originalScale.z);

        while (elapsedTime < crushTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / crushTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Asegura que llegue al tamaño final

        // Esperar un poco y luego destruir la caja
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);

    }
}
