using UnityEngine;
using UnityEngine.AI;

public class AINavigatiion : MonoBehaviour
{
    public Transform objetivo; // Referencia al objetivo (por ejemplo, el jugador)
    private NavMeshAgent agente;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false; // Desactivar rotaci�n autom�tica (2D)
        agente.updateUpAxis = false;  // Adaptar al eje 2D
    }

    private void Update()
    {
        if (objetivo != null)
        {
            // Establecer la posici�n del objetivo como destino
            agente.SetDestination(objetivo.position);
        }
    }
}
