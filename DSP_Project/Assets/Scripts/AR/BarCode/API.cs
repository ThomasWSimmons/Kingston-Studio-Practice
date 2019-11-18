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
    public GameObject loading;
    public GameObject check;
    public GameObject cross;
    public GameObject errorNoMatch;
    public GameObject errorScanning;
    private const string endpoint = "https://world.openfoodfacts.org/api/v0/product/";
    public static string barcode;
    private string theURL;
    public static bool scanned;

    //to allow disabling
    public static bool doneAPI;

    //result section
    public TextMeshProUGUI theProductName, serving, calories, carbs, sugars, answer, description, injectionAmount, unitRatio;
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
            Debug.Log(theURL);
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
                Debug.Log("getting infos");
                //INPUT INFOS HERE
                string[] words = webRequest.downloadHandler.text.Split(',');


                getProdName(words);//1st
                Debug.Log("prod name ok");
                //get the image
                string imgURL = getProdImage(words);
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(imgURL);
                yield return request.SendWebRequest();
                if (request.isNetworkError || request.isHttpError)
                    Debug.Log(request.error);
                else
                    theImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Debug.Log("prod img ok");
                //3rd
                getProdServing(words);
                Debug.Log("prod serving ok");
                //4th nutriments
                getProdSugar(words);
                Debug.Log("prod sugar ok");
                carbs.text=getProdCarb(words);
                string carbohydrates = carbs.text;
    
                int totalCarbs = 0;
                //carbs can be a float so make sure to round it if it is a float
                Debug.Log("Carbs "+carbohydrates);
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
                Debug.Log("prod carbs ok");

                getProdCalories(words);
                Debug.Log("prod calories ok");

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
                Debug.Log("prod score ok");

                //6th units
                getInjection(totalCarbs);
                Debug.Log("prod injection ok");
                //get the impact based on the injection
                int amount = 0;
                if (!intParseCheck(injectionAmount.text, amount))
                {
                    errorActive();
                }
                amount = Int32.Parse(injectionAmount.text);
                
                getProdImpact(amount);
                Debug.Log("prod impact ok");
                getUnitRatio(totalCarbs);
                Debug.Log("prod ratio ok");
                loading.SetActive(false);
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
        loading.SetActive(false);
        cross.SetActive(true);
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
        }
        cross.SetActive(false);
        errorNoMatch.SetActive(true);
        doneAPI = true;

    }
    void errorActive()
    {
        loading.SetActive(false);
        cross.SetActive(true);
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
        }
        cross.SetActive(false);
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
    void getProdImpact(int injections)
    {
        if (injections>0 && injections<3)
        {
            answer.text = "This product fits your diet, but make sure to inject your insulin.";
        }
        else if(injections==3)
        {
            answer.text = "This product has a bit too much carbohydrates. Make sure you inject your insulin.";
        }
        else if(injections>3)
        {
            answer.text = "There is a lot of carbohydrates in this product. Make sure you inject your insulin!";
        }
    }
    void getProdDescription(string nutriscore)
    {
        switch(nutriscore)
        {
            case "a":
                description.text = "This product has a nutriscore of A !\n Good Job!";
                whichNutriscoreIMG.texture = allNutriscore[0];
                break;
            case "b":
                description.text = "This product has a nutriscore of B !\n It's okay to consume it.";
                whichNutriscoreIMG.texture = allNutriscore[1];

                break;
            case "c":
                description.text = "This product has a nutriscore of C !\n This is not the best for you.";
                whichNutriscoreIMG.texture = allNutriscore[2];

                break;
            case "d":
                description.text = "This product has a nutriscore of D !\n You probably should not eat this everyday.";
                whichNutriscoreIMG.texture = allNutriscore[3];

                break;
            case "e":
                description.text = "This product has a nutriscore of E !\n You should definitely avoid this type of food.";
                whichNutriscoreIMG.texture = allNutriscore[4];

                break;
        }

    }
    void getInjection(int carbsAmount)
    {
        float toRound = (float)(carbsAmount / 10.0);
        int theInjection = (int)Math.Round(toRound);
        injectionAmount.text = theInjection.ToString();
    }
    void getUnitRatio(int carbsAmount)
    {
        unitRatio.text = "Your current ratio is 1 unit to 10 grams of carbohydrates\n";
        float unitAmount = (float)(carbsAmount / 10.0);
        Debug.Log(unitAmount);
        int rounded = (int)Math.Round(unitAmount);
        unitRatio.text += carbsAmount.ToString() + "/10(ratio) =" + unitAmount.ToString() + " (rounded up)= " + rounded.ToString() + "units";

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
                Debug.Log("nutriScore: " + NutriScore);
            }
        }
    }
}