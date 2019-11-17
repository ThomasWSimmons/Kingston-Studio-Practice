using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject Results;
    public GameObject checkMark;
    public GameObject loading;
    private float timer;
    public GameObject scanningPage;
    private static bool Active;
    private static string theName;


    //result section
    public Text theProductName,serving,calories,carbs,sugars,answer,description,injectionAmount,unitRatio;
    
    public Image theImage;
   
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
                answer.text = "don't eat too much sugar!";
                description.text = "I don't think it's a good idea";
                injectionAmount.text = "5";
                unitRatio.text = "Your unit ratio is";
                //assign nutriments values here
                break;
            case "coffee":
                theProductName.text = "Coffee";
                serving.text = "20g";
                calories.text = "30 kCal";
                carbs.text = "0g";
                sugars.text = "0g";
                answer.text = "don't drink too much coffee!";
                description.text = "Wake up!";
                injectionAmount.text = "2";
                unitRatio.text = "Your unit ratio is";
                break;
        }
    }
}
