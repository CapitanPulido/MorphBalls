using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    bool isPaused = false; 

    public void pauseGame()
    { 
       if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }

        else

        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void Continuar()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
