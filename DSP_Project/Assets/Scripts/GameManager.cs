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



    //Awake is always called before any Start functions
    void Awake()
    {
        PlayerPrefs.DeleteAll();
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
        if(PlayerPrefs.HasKey("firstLaunch"))
        {
            Debug.Log("to menu");
            loadMenu();
        }
        else
        {
            Debug.Log("to setup");
            loadPlayerProfile();
        }
        InitApp();
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



    //Update is called every frame.
    void Update()
    {
        

          
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.

    public void AddWandToList()
    {
    }
    void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void loadPlayerProfile()
    {
        SceneManager.LoadScene("profileSetUp");
    }





}

