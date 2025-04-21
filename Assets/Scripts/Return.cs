using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Regreso());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Regreso()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuNiveles");
    }
}
