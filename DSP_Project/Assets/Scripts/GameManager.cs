using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.UI;                   //Allows us to use UI.
using System.IO;

public class GameManager : MonoBehaviour
{
    
                                                            //public int playerFoodPoints = 100;                      //Starting value for Player food points.
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public static int indexB, indexH, indexK, indexF;
    public int playerExperience, playerLevel,playerCurrentCal,playerCurrentSug;
    public static int theMenu;
    //Awake is always called before any Start functions
    void Awake()
    {

       
      if (!File.Exists(Application.persistentDataPath + "/ThePlayerInfo.gd"))
        { 
               
            
            PlayerPrefs.DeleteAll();
            Debug.Log("new game");
            Game.current = new Game();
            Game.current.thePlayer.experience = playerExperience;
            Game.current.thePlayer.level = playerLevel;
            Game.current.thePlayer.bodyIndex = indexB;
            Game.current.thePlayer.hairIndex = indexH;
            Game.current.thePlayer.kitIndex = indexK;
            Game.current.thePlayer.faceIndex = indexF;
            playerExperience = Game.current.thePlayer.experience;
            playerLevel = Game.current.thePlayer.level;
            playerCurrentCal = Game.current.thePlayer.caloriesCurrent;
            playerCurrentSug = Game.current.thePlayer.sugarCurrent;
            Debug.Log(playerExperience + " " + playerLevel);
            containerGraph.valList = new List<int>(); 

        }
        else
        {
           
            saveSystem.LoadPlayer();
            Game.current = new Game();
            playerExperience = saveSystem.experience;
            playerLevel = saveSystem.level;
            indexB = saveSystem.body;
            indexH = saveSystem.hair;
            indexK = saveSystem.kit;
            indexF = saveSystem.face;
            theMenu = saveSystem.mainMenu;
            playerCurrentSug = saveSystem.sugarCurrent;
            playerCurrentCal = saveSystem.caloriesCurrent;
            Debug.Log(theMenu);
            Game.current.thePlayer.sugarCurrent = playerCurrentSug;
            Game.current.thePlayer.caloriesCurrent = playerCurrentCal;
            Game.current.thePlayer.experience = playerExperience;
            Game.current.thePlayer.level = playerLevel;
            Game.current.thePlayer.bodyIndex = indexB;
            Game.current.thePlayer.hairIndex = indexH;
            Game.current.thePlayer.kitIndex = indexK;
            Game.current.thePlayer.faceIndex = indexF;
            containerGraph.valList = new List<int>();//for now list of values re initialised
            if(saveSystem.menu==1)//if user checked remember me
            {
                MainMenu();
            }
        }

        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        //SET UP PLAYER EXPERIENCE AMOUNT HERE
        //SET UP PLAYER LEVEL HERE


    }
    void OnApplicationQuit()
    {
        saveSystem.SavePlayer();

        Debug.Log("Application ending after " + Time.time + " seconds");
    }

    //this is called only once, and the paramter tell it to be called only after the scene was loaded
    //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

        instance.InitApp();
    }

    //Initializes the game for each level.
    public void InitApp()
    {
       
    }
    public void Resume()
    {
       
        Time.timeScale = 1f;
    }
    public void Pause()
    {
       
        Time.timeScale = 0f;

    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void ARScan()
    {

        SceneManager.LoadScene("ARscene");
    }
    public void MainMenu()
    {
        if(theMenu==0)
        {
            theMenu = 1;
            PlayerPrefs.SetInt("hasLoggedIn", 1);
            Debug.Log("the MENU:"+theMenu);
        }
        else
        {
            Debug.Log("the menu is " + theMenu);
        }
        SceneManager.LoadScene("Menu");
    }
    public void BarCode()
    {
        SceneManager.LoadScene("BarCodeScanner");
    }
    public void experience()
    {
        SceneManager.LoadScene("experience");
    }
    public void customisation()
    {
        SceneManager.LoadScene("AvatarCustomisation");
    }
    public void trending()
    {
        SceneManager.LoadScene("Chart");
    }
    public void toSignPage()
    {
        PlayerPrefs.DeleteKey("remember");
        SceneManager.LoadScene("profileSetUp");
    }
    public void holdNutriscore(string score)
    {
        switch(score)
        {
            case "a":
                PlayerPrefs.SetString("nutriGrade", "a");
                PlayerPrefs.SetInt("nutriExp", 25);
                break;
            case "b":
              
                PlayerPrefs.SetString("nutriGrade", "b");
                PlayerPrefs.SetInt("nutriExp", 20);
                break;
            case "c":
                PlayerPrefs.SetString("nutriGrade", "c");
                PlayerPrefs.SetInt("nutriExp", 15);
                break;
            case "d":
                
                PlayerPrefs.SetString("nutriGrade", "d");
                PlayerPrefs.SetInt("nutriExp", 10);
                break;
            case "e":
              
                PlayerPrefs.SetString("nutriGrade", "e");
                PlayerPrefs.SetInt("nutriExp", 5);
                break;
        }
        manageAppearence.ChangeEmotion=true;
    }
 

}

