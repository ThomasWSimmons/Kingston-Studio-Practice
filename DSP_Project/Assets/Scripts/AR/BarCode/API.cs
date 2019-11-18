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
    public GameObject error;
    private const string endpoint = "https://world.openfoodfacts.org/api/v0/product/";
    public static string barcode;
    private string theURL;
    public static bool scanned;

    //to allow disabling
    public static bool doneAPI;

    //result section
    public Text theProductName, serving, calories, carbs, sugars, answer, description, injectionAmount, unitRatio;
    private Text nutriScore;
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
                cross.SetActive(true);
                float timer = 0;
                while (timer < 1f)
                {
                    timer += Time.deltaTime;
                }
                cross.SetActive(false);
                error.SetActive(true);
                doneAPI = true;
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
                if (carbohydrates.Contains("."))
                {
                    float tempo = float.Parse(carbohydrates);
                    totalCarbs = (int)Math.Round(tempo);
                }
                else
                {
                    totalCarbs = Int32.Parse(carbohydrates);
                }
                Debug.Log("CARBS" + totalCarbs);
                carbs.text += "g";


                getProdCalories(words);
                Debug.Log("prod calories ok");

                //5th nutriscore
                getNutriScore(words);
              
                if (nutriScore.text != null)
                {
                    string theNutrScore = nutriScore.text;
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
                int amount = Int32.Parse(injectionAmount.text);
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
                //Debug.Log("carbs "+s);
                string[] temporary = s.Split(':');
                theResultCarbs = (string)temporary.GetValue(1) + "g";
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
                //Debug.Log("nutriScore: "+s);
                string[] temporary = s.Split(':');
                string[] theName = temporary[1].Split('"');
                nutriScore.text = (string)theName.GetValue(1);
            }
        }
    }
}

/* BACKUP CODE
 *using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class API : MonoBehaviour
{
public Button theButton;
public Text responseText;
private string[] sugarAmount;
private const string endpoint = "https://world.openfoodfacts.org/api/v0/product/";
//private const string endpoint = "https://www.foodrepo.org/api/v3/products?excludes=images%2Cid%2Ccountry%2Cdisplay_name_translations%2Cingredients_translations%2Corigin_translations%2Cstatus%2Cquantity%2Cunit%2Cportion_quantity%2Cportion_unit%2Calcohol_by_volume%2Ccreated_at%2Cupdated_at&barcodes=";
public static string barcode;
private string theURL;
public static bool scanned;
void Start()
{
    scanned = false;

    // A correct website page.


    // A non-existing page.
    //StartCoroutine(GetRequest("https://error.html"));
}
public void Update()
{
    if(scanned)
    {
        scanned = false;
        //theURL = endpoint + barcode;
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
        //webRequest.SetRequestHeader("Accept", "application/json");
        //webRequest.SetRequestHeader("Authorization", "Token token=80ccc867e09892aa5f00e194fe1b2d75");
        webRequest.SetRequestHeader("UserAgent", "project - Android - Version 1.0");
        // Request and wait for the desired page.
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
            //responseText.text = "Received: " + webRequest.downloadHandler.text;//prints results
            //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            string[] words = webRequest.downloadHandler.text.Split(',');
            foreach (string s in words)
            {
                if (s.Contains("sugars") && s.Contains("100"))
                {
                     sugarAmount = s.Split(':');
                     responseText.text = (string)sugarAmount.GetValue(1);
                }
            }

        }
    }
}
}
/*
private const string API_KEY = "80ccc867e09892aa5f00e194fe1b2d75";
public Text responseText;
// Start is called before the first frame update
void Start()
{
theURL = endpoint + barcode;
}

// Update is called once per frame
void Update()
{

}


public void Request()
{
UnityWebRequest request = new UnityWebRequest("www.google.com");
//

StartCoroutine(OnResponse(request));
}

private IEnumerator OnResponse(UnityWebRequest req)
{
yield return req;
responseText.text = req.downloadHandler.text;
}

}

/*

*
*
*using (var httpClient = new HttpClient())
{
using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://www.foodrepo.org/api/v3/products/_search"))
{
request.Headers.TryAddWithoutValidation("Accept", "application/json");
request.Headers.TryAddWithoutValidation("Authorization", "Token token=80ccc867e09892aa5f00e194fe1b2d75"); 

request.Content = new StringContent("{\n  \"_source\": {\n    \"includes\": [\n      \"name_translations.en\",\n      \"nutrients.sugars.name_translations.en\",\n      \"nutrients.sugars.per_day\",\n      \"nutrients.sugars.per_hundred\"\n    ]\n  },\n  \"size\": 1,\n  \"query\": {\n    \"query_string\": {\n      \"fields\" : [\n        \"barcode\"\n      ],\n      \"query\" : \"7610848337010\"\n    }\n  }\n}\n\n");
request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.api+json"); 

var response = await httpClient.SendAsync(request);
}
}
*
* 
*
*/


