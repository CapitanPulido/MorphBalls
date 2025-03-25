using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mENU : MonoBehaviour
{

    public GameObject opciones;

    public void Start()
    {
        opciones.SetActive(false);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Salir()
    {
        Application.Quit();
    }

}
