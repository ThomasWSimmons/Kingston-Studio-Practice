using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class API : MonoBehaviour
{
    public GameObject result;

    public GameObject check;

    public GameObject errorNoMatch;
    public GameObject errorScanning;
    private const string endpoint = "https://world.openfoodfacts.org/api/v0/product/";
    public static string barcode;
    private string theURL;
    public static bool scanned;

    //to allow disabling
    public static bool doneAPI;

    //result section
    public TextMeshProUGUI theProductName, serving, calories, carbs, sugars, unitRatio,carbcalculation,unroundedunit;
    public TextMeshProUGUI[] injections;
    private string NutriScore;
    public RawImage theImage;
    public RawImage whichNutriscoreIMG;
    public Texture[] allNutriscore;

    void OnEnable()
    {
        doneAPI = false;
        scanned = false;

    }
    public void Update()
    {
        if (scanned)
        {
            scanned = false;
            theURL = endpoint + barcode + ".json";

            if (theURL != null)
            {
                StartCoroutine(GetRequest(theURL));

            }
        }

    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("UserAgent", "project - Android - Version 1.0");
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                errorNoMatchActive();
            }
            else
            {
        
                //INPUT INFOS HERE
                string[] words = webRequest.downloadHandler.text.Split(',');


                getProdName(words);//1st
                //get the image
                string imgURL = getProdImage(words);
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(imgURL);
                yield return request.SendWebRequest();
                if (request.isNetworkError || request.isHttpError)
                    Debug.Log(request.error);
                else
                    theImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                //3rd
                getProdServing(words);
                //4th nutriments
                getProdSugar(words);
                carbs.text=getProdCarb(words);
                string carbohydrates = carbs.text;
    
                int totalCarbs = 0;
                //carbs can be a float so make sure to round it if it is a float
                if (carbohydrates.Contains("."))
                {

                    float tempo = 0;
                    if (!floatParseCheck(carbohydrates, tempo))
                    {
                        errorActive();
                    }
                     tempo = float.Parse(carbohydrates);
                     totalCarbs = (int)Math.Round(tempo);
                    


                }
                else
                {

                    if (!intParseCheck(carbohydrates, totalCarbs))
                    {
                        errorActive();
                    }
                    totalCarbs = Int32.Parse(carbohydrates);
                    
                }
                carbs.text += "g";

                getProdCalories(words);

                //5th nutriscore
                getNutriScore(words);
              
                if (NutriScore != null)
                {
                    string theNutrScore = NutriScore;
                    getProdDescription(theNutrScore);//get input based on nutriscore
                }
                else
                {
                    //if no nutriscore image not displayed
                    whichNutriscoreIMG.gameObject.SetActive(false);
                }

                //6th units
                getInjection(totalCarbs);
               
           
                getUnitRatio(totalCarbs);
                check.SetActive(true);
                yield return new WaitForSeconds(1);
                check.SetActive(false);
                result.SetActive(true);
                doneAPI = true;
            }

        }
    }
    void errorNoMatchActive()
    {

        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
        }
        errorNoMatch.SetActive(true);
        doneAPI = true;

    }
    void errorActive()
    {

        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
        }
        errorScanning.SetActive(true);
        doneAPI = true;
    
    }
    bool floatParseCheck(string toparse, float toparse2)
    {
        return float.TryParse(toparse, out toparse2);
    }
    bool intParseCheck(string toparse, int toparse2)
    {
        return int.TryParse(toparse, out toparse2);
    }
    void getProdName(string[] theAnswer)
    {
        foreach (string s in theAnswer)
        {
            if (s.Contains("\"product_name\""))
            {
                string[] temporary = s.Split(':');
                string[] theName = temporary[1].Split('"');
                theProductName.text = (string)theName.GetValue(1);

            }
        }
    }
    void getProdServing(string[] theAnswer)
    {
        foreach (string s in theAnswer)
        {
            if (s.Contains("serving_size"))
            {
                // Debug.Log("serving: "+s);
                string[] temporary = s.Split(':');
                string[] theName = temporary[1].Split('"');
                serving.text = (string)theName.GetValue(1);
            }
        }

    }
    string getProdCarb(string[] theAnswer)
    {
        string theResultCarbs = "";
        foreach (string s in theAnswer)
        {
            if (s.Contains("carbohydrates_serving"))
            {
                string[] temporary = s.Split(':');
                theResultCarbs = (string)temporary.GetValue(1);
            }
        }
        return theResultCarbs;
    }
    void getProdCalories(string[] theAnswer)
    {
        string Kjoul = "";
        foreach (string s in theAnswer)
        {
            if (s.Contains("energy-kcal_serving"))
            {

                string[] temporary = s.Split(':');
                Kjoul = (string)temporary.GetValue(1);
            }
        }
        int joul = 0;
        if (!intParseCheck(Kjoul, joul))
        {
            errorActive();
        }
     
           
        joul = Int32.Parse(Kjoul);
        int cals = (int)Math.Round(joul / 4.18f);
        calories.text = cals.ToString() + "kCal";
    }
    void getProdSugar(string[] theAnswer)
    {
        foreach (string s in theAnswer)
        {
            if (s.Contains("sugars_serving"))
            {
                string[] temporary = s.Split(':');
                sugars.text = (string)temporary.GetValue(1) + "g";
            }
        }
    }
    string getProdImage(string[] theAnswer)
    {
        string MediaUrl = "";
        foreach (string s in theAnswer)
        {
            if (s.Contains("image_front_url"))
            {

                string[] temporary = s.Split('"');
                MediaUrl = (string)temporary.GetValue(3);
            }
        }
        return MediaUrl;

    }
    
    void getProdDescription(string nutriscore)
    {
        switch(nutriscore)
        {
            case "a":
                whichNutriscoreIMG.texture = allNutriscore[0];
                break;
            case "b":
                whichNutriscoreIMG.texture = allNutriscore[1];

                break;
            case "c":
                whichNutriscoreIMG.texture = allNutriscore[2];

                break;
            case "d":
                whichNutriscoreIMG.texture = allNutriscore[3];

                break;
            case "e":
                whichNutriscoreIMG.texture = allNutriscore[4];

                break;
        }

    }
    void getInjection(int carbsAmount)
    {
        float toRound = (float)(carbsAmount / 10.0);
        int theInjection = (int)Math.Round(toRound);
        for (int i = 0; i < 3; i++)
        {
            injections[i].text = theInjection.ToString();
        }
    }
    void getUnitRatio(int carbsAmount)
    {
        int playerRatio;
        if (PlayerPrefs.HasKey("ratio"))
        {
            playerRatio= PlayerPrefs.GetInt("ratio");
            unitRatio.text = playerRatio.ToString();
        }
        else
        {
            playerRatio = 10;
            unitRatio.text = "";
        }
        float unitAmount = (float)(carbsAmount / playerRatio);
        Debug.Log(carbsAmount+"/"+playerRatio+"="+unitAmount);
        carbcalculation.text = carbsAmount.ToString();
        unroundedunit.text = unitAmount.ToString();

    }
    void getNutriScore(string[] theAnswer)
    {
        foreach (string s in theAnswer)
        {
            if (s.Contains("nutriscore_grade"))
            {
               
                string[] temporary = s.Split(':');
                string[] theName = temporary[1].Split('"');
                NutriScore = theName[1];
            }
        }
    }
}