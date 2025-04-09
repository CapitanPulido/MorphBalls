using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    public PolyMorph morph;

    public Rigidbody2D Rb;
    public Rigidbody2D Rb2;

    public float Masa1;

    // Start is called before the first frame update
    void Start()
    {
        Rb.mass = Masa1;
        Rb2.mass = Masa1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (morph.Metal && collision.gameObject.CompareTag("Pelota"))
        {
            Rb.mass = 1;
            Rb2.mass = 1;
            Debug.Log("choque");
        }
    }
}
