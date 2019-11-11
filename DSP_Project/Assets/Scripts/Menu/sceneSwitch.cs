using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public void ARScan()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void QuitApp()
    {
        //if click on settings
        Application.Quit();
    }
    public void Switch()
    {
    
    }
    public void resetChar()
    {
        Debug.Log("reset");
        handleDropDown.reset = true;
    }

}


