using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    public GameObject Results;
    public GameObject checkMark;
    private float timer;
    public GameObject scanningPage;
    private static bool Active;
    private static string theName;


    //result section
    public TMP_Text theProductName,serving,calories,carbs,sugars,injectionAmount1,injectionAmount2,unitRatio;
    
    public Image theImage, theNutriscore;
    public Sprite[] nutriscores;
   
    // Start is called before the first frame update
    void Start()
    {
        Results.SetActive(false);
        timer = 0; //loads the results of scan
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Results.activeSelf && Active)
        {
            trackingManager.trackingFound = true;
            if (timer > 2f)
            {
                timer = 0;//user will put his phone away from target so tracking active false;
                checkMark.SetActive(false);
                Active = false;
                whichResult(theName);
                Results.SetActive(true);

            }
            else
            {
                if(System.Math.Abs(timer) < .01)
                {
                    scanningPage.SetActive(false);
                }
                timer += Time.deltaTime;
                
                if(timer>1.5 && timer<1.7)
                {
  
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
    public void whichResult(string name)
    {
        switch (name)
        {
            case "sugar":
                theProductName.text = "sugar";
                serving.text = "50g";
                calories.text = "100 kCal";
                carbs.text = "6.4g";
                sugars.text = "6.4g";
                injectionAmount1.text = injectionAmount2.text= "5";
                theNutriscore.sprite = nutriscores[0];
                GameManager.instance.holdNutriscore("a");
                unitRatio.text = PlayerPrefs.GetInt("ratio").ToString();
                
                //assign nutriments values here
                break;
            case "coffee":
                theProductName.text = "Coffee";
                serving.text = "20g";
                calories.text = "30 kCal";
                carbs.text = "0g";
                sugars.text = "0g";
                injectionAmount1.text = injectionAmount2.text= "2";
                theNutriscore.sprite = nutriscores[1];
                GameManager.instance.holdNutriscore("b");
                unitRatio.text = PlayerPrefs.GetInt("ratio").ToString();
                break;
        }
    }
}
