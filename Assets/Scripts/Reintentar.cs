using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reintentar : MonoBehaviour
{

    public Transform Spawn;
    public GameObject Ball;
    public BolaSimple ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Reintento"))
        {
            Ball.transform.position = Spawn.transform.position;
            ball.StopMoving();
            ball.rb.velocity = Vector3.zero;
            ball.rb.angularVelocity = 0f;
        }
    }
}
