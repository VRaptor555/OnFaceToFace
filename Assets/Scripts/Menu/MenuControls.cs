using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game_Start");
    }
    
    public void ExitPressed()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
