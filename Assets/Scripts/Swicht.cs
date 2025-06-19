using UnityEngine;

public class Swicht : MonoBehaviour
{
    [Header("Objeto a monitorear")]
    public GameObject objetoMonitoreado;

    [Header("Objeto a activar")]
    public GameObject objetoAActivar;

    private bool activado = false;

    void Update()
    {
        if (!activado && objetoMonitoreado == null)
        {
            objetoAActivar.SetActive(true);
            activado = true;
        }
    }
}
