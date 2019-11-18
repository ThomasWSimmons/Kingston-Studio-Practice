using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZXing;

public class readSimple : MonoBehaviour
{
  // public Texture2D inputTxture; // Note: [x] Read/Write must be enabled from texture import settings
    //public Text theText;
    private string test;
    private string test2;
    private string barcode;
    private const string endpoint = "https://world.openfoodfacts.org/api/v0/product/4088600084107.json";
    //private const string endpoint = "https://www.foodrepo.org/api/v3/products?excludes=images%2Cid%2Ccountry%2Cbarcode%2Cname_translations%2Cdisplay_name_translations%2Cingredients_translations%2Corigin_translations%2Cstatus%2Cquantity%2Cunit%2Cportion_quantity%2Cportion_unit%2Calcohol_by_volume%2Ccreated_at%2Cupdated_at%2Cnutrients-salt&barcodes=";
    private string theURL;


    public Text theProductName, serving, calories, carbs, sugars, answer, description,injectionAmount, unitRatio, nutriScore;
    public RawImage theImage;
    public RawImage whichNutriscoreIMG;
    public Texture[] allNutriscore;
    private void Start()
    {
      /* IBarcodeReader reader = new BarcodeReader();
        // get texture Color32 array
        var barcodeBitmap = inputTxture.GetPixels32();
        // detect and decode the barcode inside the Color32 array
        var result = reader.Decode(barcodeBitmap, inputTxture.width, inputTxture.height);
        // do something with the result
        if (result != null)
        {
            //Debug.Log(result.BarcodeFormat.ToString());
            Debug.Log(result.Text);
            //barcode = result.Text;
           // barcode = "7610848337010";
            // API.scanned = true;
            //API.barcode = result.Text;

            //theURL = endpoint + barcode;
            //the URL = endpoint + barcode + ".json";
           
      
            
        }*/

        StartCoroutine(GetRequest(endpoint));
    }
IEnumerator GetRequest(string uri)
{
    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    {
            //webRequest.SetRequestHeader("Accept", "application/json");
            //webRequest.SetRequestHeader("Authorization", "Token token=80ccc867e09892aa5f00e194fe1b2d75");
            // Request and wait for the desired page.
            //webRequest.SetRequestHeader("UserAgent", "DSP project - Android - Version 1.0");
           yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        if (webRequest.isNetworkError)
        {
            Debug.Log(pages[page] + ": Error: " + webRequest.error);
        }
        else
        {
            //responseText.text = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;//prints query + results

            //theText.text = "Received: " + webRequest.downloadHandler.text;//prints results
            test = webRequest.downloadHandler.text;
            //test2 = webRequest.downloadHandler.text;
           //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            string[] words = test.Split(',');
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
                carbs.text = getProdCarb(words);
                string carbohydrates = carbs.text;
                int totalCarbs = 0;
                //carbs can be a float so make sure to round it if it is a float
                if(carbohydrates.Contains("."))
                {
                    float tempo = float.Parse(carbohydrates);
                    totalCarbs = (int)Math.Round(tempo);
                }
                else
                {
                   totalCarbs = Int32.Parse(carbohydrates);
                }
                Debug.Log("CARBS"+totalCarbs);
                carbs.text += "g";

                getProdCalories(words);
                //5th nutriscore
                getNutriScore(words);

                if (nutriScore.text!=null)
                {
                    string theNutrScore = nutriScore.text;
                    getProdDescription(theNutrScore);//get input based on nutriscore
                }
                else
                {
                    whichNutriscoreIMG.gameObject.SetActive(false);
                }
                //6th units
                getInjection(totalCarbs);

                //get the impact based on the injection
                int amount = Int32.Parse(injectionAmount.text);
                getProdImpact(amount);
                getUnitRatio(totalCarbs);
          
            }

        }
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
                Debug.Log("carbs "+s);
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
        int joul = Int32.Parse(Kjoul);
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
        if (injections > 0 && injections < 3)
        {
            answer.text = "This product fits your diet, but make sure to inject your insulin.";
        }
        else if (injections == 3)
        {
            answer.text = "This product has a bit too much carbohydrates. Make sure you inject your insulin.";
        }
        else if (injections > 3)
        {
            answer.text = "There is a lot of carbohydrates in this product. Make sure you inject your insulin!";
        }
    }
    void getProdDescription(string nutriscore)
    {
        switch (nutriscore)
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
                description.text = "This product has a nutriscore of D !\n You probably should not have this everyday.";
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
        Debug.Log("UNIT"+unitAmount);
        int rounded = (int)Math.Round(unitAmount);
        unitRatio.text += carbsAmount.ToString() + "/10(ratio) =" + unitAmount.ToString() + " (rounded up)= " + rounded.ToString() + "units";

    }
    void getNutriScore(string[] theAnswer)
    {
        foreach (string s in theAnswer)
        {
            if (s.Contains("nutriscore_grade"))
            {
                //Debug.Log("nutriScore: "+s);
                string[] temporary = s.Split(':');
                string[] theName = temporary[1].Split('"');
                nutriScore.text = (string)theName.GetValue(1);
            }
           
        }
    }
}
