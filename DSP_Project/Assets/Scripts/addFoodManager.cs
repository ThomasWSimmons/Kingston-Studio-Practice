using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class addFoodManager : MonoBehaviour
{
    public TMP_InputField theFood,theServing,theCalperServ;
    private string theFoodName;
    public GameObject ourPanel;
    public GameObject breakfastpanel, lunchpanel, dinnerpannel;
    public TMP_Dropdown theMeal;
    private int goodToGo, whichMeal,calPerServing,serving,caloriesTotal;
    public GameObject errorMeal, errorFood, errorQuantity, errorType;
    //Insert the empty game object with the TextMesh attached
    public GameObject textToAdd;

    private void Start()
    {
       
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        
        goodToGo = 0;
        // PlayerPrefs.SetInt("firstLaunch", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetUpMealType()
    {
        if (theMeal.value > 0)
        {
            switch (theMeal.value)
            {
                case 1:
                    whichMeal = 1;
                    
                    break;//breakfast
                case 2:
                    whichMeal = 2;
                    break;//lunch
                case 3:
                    whichMeal = 3;
                    break;//diner

            }
            Debug.Log(theMeal.value);
            goodToGo++;
            
        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            Debug.Log("meal error");
            errorMeal.SetActive(true);
        }



    }
    public void SetUpFood()
    {

        if (theFood.text.Length > 0)
        {
            theFoodName = theFood.text;
            Debug.Log(theFoodName);
            goodToGo++;
        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            Debug.Log("food error");
            errorFood.SetActive(true);
        }



    }
    public void SetUpServing()
    {
        if (theServing.text.Length > 0)
        {

            serving = Int32.Parse(theServing.text);
            Debug.Log("serving =" + serving);
            goodToGo++;

        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            errorQuantity.SetActive(true);
            Debug.Log("quantity error");
        }



    }
    
    public void SetUpCalServ()
    {
        if (theCalperServ.text.Length > 0)
        {
            calPerServing = Int32.Parse(theCalperServ.text);
            Debug.Log("calperserving =" + calPerServing);
            goodToGo++;

        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            errorType.SetActive(true);
            Debug.Log("quantity error");
        }

    }

    public void ToFood()
    {
        Debug.Log(goodToGo);
        if (goodToGo == 4)
        {
                getCalories(serving,calPerServing);
                Debug.Log("Updating");
                setScrollView(whichMeal);
                updateCalorieSlider.update = true;
                ourPanel.SetActive(false);
        }
     
    }
    public void getCalories(int serving, int caloriesPerserving)
    {
        caloriesTotal = serving * caloriesPerserving;
        Game.current.thePlayer.caloriesCurrent += caloriesTotal;
    }
    public void setScrollView(int theMealAdded)
    {
        var copy = Instantiate(textToAdd);
        
        copy.GetComponent<TextMeshProUGUI>().text= theFoodName + "\n" + serving + " serving " + caloriesTotal + " calories";

        switch (theMealAdded)
        {
            case 1:
                copy.transform.SetParent(breakfastpanel.transform, false);
                copy.transform.localPosition = Vector3.zero;
                break;
            case 2:
                copy.transform.SetParent(lunchpanel.transform, false);
                copy.transform.localPosition = Vector3.zero;
                break;
            case 3:
                copy.transform.SetParent(dinnerpannel.transform, false);
                copy.transform.localPosition = Vector3.zero;
                break;
        }
    }



    
}
