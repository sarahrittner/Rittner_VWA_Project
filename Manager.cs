using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    void StartGame()
    {
        SceneManager.LoadScene("Start");
    }


    void QuitGame()
    {
        Application.Quit();
    }
}
