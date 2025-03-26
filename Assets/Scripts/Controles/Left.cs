using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    public BolaSimple ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void MLeft1()
    {
        ball.left = true;
        ball.MoveLeft();
        Debug.Log("Mooviendose"); 
    }

    public void MLeft2()
    {
        ball.left = false;
        Debug.Log("Quieto");
        ball.StopMoving();
    }
}