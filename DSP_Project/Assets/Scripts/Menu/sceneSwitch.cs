using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public void ARScan()
    {
        GameManager.instance.ARScan();
        
    }
    public void MainMenu()
    {
        GameManager.instance.MainMenu();
       
    }
    public void BarCode()
    {
        GameManager.instance.BarCode();
    }
    public void experience()
    {
        GameManager.instance.experience();
    }
    public void Customisation()
    {
        GameManager.instance.customisation();
    }
    public void Trends()
    {
        GameManager.instance.trending();

    }
    public void SignPage()
    {
        GameManager.instance.toSignPage();
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


