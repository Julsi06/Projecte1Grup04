using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Added for SceneManager

public class LogicReturn : MonoBehaviour 
{
    public void OnReturnSettingClick()  
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnReturnCreditsClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}