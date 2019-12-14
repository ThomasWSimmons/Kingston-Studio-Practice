using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EventManager : MonoBehaviour
{
    public GameObject Results;
    public GameObject checkMark;
    private float timer;
    public GameObject scanningPage,errorCalories;
    private static bool Active;
    private static string theName;
    private int caloriesAmount, sugarsAmount;
    
    //result section
    public TMP_Text theProductName,serving,calories,carbs,sugars,injectionAmount1,injectionAmount2,unitRatio;
    public TMP_InputField servings;
    public RawImage theImage, theNutriscore;
    public Texture[] nutriscores,product;
    
   
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
    public void updateCalories()
    {
        int theServing;
        if(servings.text.Length>0)
        {
            theServing = Int32.Parse(servings.text);
            Game.current.thePlayer.caloriesCurrent += (caloriesAmount * theServing);
            Game.current.thePlayer.sugarCurrent += (sugarsAmount * theServing);
            Debug.Log("serving =" + serving);

        }
        else
        {
            errorCalories.SetActive(true);
        }
    }
    public void whichResult(string name)
    {
        switch (name)
        {
            case "grappes":
                theProductName.text = "Grappes";
                serving.text = "100g";
                calories.text = "70 kCal";
                caloriesAmount = 70;
                carbs.text = "17g";
                sugars.text = "16g";
                sugarsAmount = 16;
                injectionAmount1.text = injectionAmount2.text = "2";
                theNutriscore.texture = nutriscores[0];
                theImage.texture = product[2];
                GameManager.instance.holdNutriscore("a");
                unitRatio.text = PlayerPrefs.GetInt("ratio").ToString();


                //assign nutriments values here
                break;
            case "popcorn":
                theProductName.text = "PopCorn";
                serving.text = "30g";
                calories.text = "142 kCal";
                caloriesAmount = 142;
                carbs.text = "19g";
                sugars.text = "12g";
                sugarsAmount = 12;
                injectionAmount1.text = injectionAmount2.text = "2";
                theNutriscore.texture = nutriscores[2];
                theImage.texture = product[3];
                GameManager.instance.holdNutriscore("d");
                unitRatio.text = PlayerPrefs.GetInt("ratio").ToString();
                break;
            case "sugar":
                theProductName.text = "sugar";
                serving.text = "8g";
                calories.text = "31 kCal";
                caloriesAmount = 31;
                carbs.text = "8g";
                sugars.text = "8g";
                sugarsAmount = 8;
                injectionAmount1.text = injectionAmount2.text= "0";
                theNutriscore.texture = nutriscores[1];
                theImage.texture = product[1];
                GameManager.instance.holdNutriscore("d");
                unitRatio.text = PlayerPrefs.GetInt("ratio").ToString();
                
                
                //assign nutriments values here
                break;
            case "coffee":
                theProductName.text = "Coffee";
                serving.text = "20g";
                calories.text = "30 kCal";
                caloriesAmount = 30;
                carbs.text = "0g";
                sugars.text = "0g";
                sugarsAmount = 0;
                injectionAmount1.text = injectionAmount2.text= "1";
                theNutriscore.texture = nutriscores[0];
                theImage.texture = product[0];
                GameManager.instance.holdNutriscore("b");
                unitRatio.text = PlayerPrefs.GetInt("ratio").ToString();
                break;
        }
    }
}
