using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject Results;
    public GameObject resultInfo;
    public GameObject checkMark;
    public GameObject loading;
    private float timer;
    public GameObject scanningPage;
    private static bool Active;
    private static string theName;
    private List<GameObject> allTheText = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in resultInfo.transform)//access result panel to get the
        {
            t.gameObject.SetActive(false);//deactivate panels
            allTheText.Add(t.gameObject);//has the text to display    
        }

        Results.SetActive(false);
        timer = 0; //loads the results of scan
    }

    // Update is called once per frame
    void Update()
    {
        if (!Results.activeSelf && Active)
        {
            trackingManager.trackingAllowed = false;
            if (timer > 2f)
            {
                timer = 0;//user will put his phone away from target so tracking active false;
                checkMark.SetActive(false);
                Results.SetActive(true);
                Active = false;
                switch (theName)
                {
                    case "sugar":
                        allTheText.Find((GameObject obj) => obj.name == "sugarInfo").SetActive(true);
                        //assign nutriments values here

                        break;
                    case "coffee":
                        allTheText.Find((GameObject obj) => obj.name == "coffeeInfo").SetActive(true);
                        break;
                }
            }
            else
            {
                if(System.Math.Abs(timer) < .01)
                {
                    scanningPage.SetActive(false);
                    loading.SetActive(true);
                }
                timer += Time.deltaTime;
                
                if(timer>1.5 && timer<1.7)
                {
                    loading.SetActive(false);
                    checkMark.SetActive(true);
                }
    
            }
        }
       /* else if (myPanel.activeSelf && !Active)
        {
            scanningPage.SetActive(true);
            myPanel.SetActive(false);
            switch (theName)
            {
                case "sugar":
                    allTheText.Find((GameObject obj) => obj.name == "sugarInfo").SetActive(false);
                    break;
                case "coffee":
                    allTheText.Find((GameObject obj) => obj.name == "coffeeInfo").SetActive(false);
                    break;

            }
        }*/ //tracking lost deactivate display
    }
    public static void Activate(string name)
    {
        Active = true;
        theName = name;

    }//tracking found
    /*
    public static void DeActivate(string name)
    {
        Active = false;
        theName = name;
    }*/ //tracking lost
}
