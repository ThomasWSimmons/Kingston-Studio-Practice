using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public void ARScan()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ARscene");
    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void BarCode()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BarCodeScanner");
    }
    public void QuitApp()
    {
        //if click on settings
        Application.Quit();
    }
    public void Switch()
    {
    
    }

   

}


