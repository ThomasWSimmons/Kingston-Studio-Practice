using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.UI;                   //Allows us to use UI.

public class GameManager : MonoBehaviour
{
    
    public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
                                                            //public int playerFoodPoints = 100;                      //Starting value for Player food points.
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject login, register;

    //Awake is always called before any Start functions
    void Awake()
    {
        /*
         *
         *
         *
         *
         * A EFFACER A LA FIN
         *
         *
         *
         *
         *
         */
        PlayerPrefs.DeleteAll();
       /* if(PlayerPrefs.HasKey("firsLog"))
        {
            if (!PlayerPrefs.HasKey("remember"))
            {
                login.SetActive(true);
            }
            else
            {
                MainMenu();
            }
        }
        else
        {
            PlayerPrefs.SetString("firstLog","ok");
            register.SetActive(true);
        }*/

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

    public void ARScan()
    {

        SceneManager.LoadScene("ARscene");
    }
    public void MainMenu()
    {
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




}

