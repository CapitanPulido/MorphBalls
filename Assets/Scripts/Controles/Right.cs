using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    // Start is called before the first frame update
    public BolaSimple ball;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MRight1()
    {
        ball.right = true;
        ball.MoveRight();
        Debug.Log("Mooviendose");
    }

    public void MRight2()
    {
        ball.right = false;
        Debug.Log("Quieto");
        ball.StopMoving();
    }
}
