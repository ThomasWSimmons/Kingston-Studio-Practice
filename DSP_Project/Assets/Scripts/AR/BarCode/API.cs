using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class API : MonoBehaviour
{
    public GameObject result;
    public GameObject check;
    public GameObject cross;
    public GameObject error;
    public Text responseText;
    private string[] sugarAmount;
    private const string endpoint = "https://world.openfoodfacts.org/api/v0/product/";
    public static string barcode;
    private string theURL;
    public static bool scanned;

    //to allow disabling
    public static bool doneAPI;
   
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
                check.SetActive(true);
                string[] words = webRequest.downloadHandler.text.Split(',');
                foreach (string s in words)
                {
                    if (s.Contains("sugars") && s.Contains("100"))
                    {
                        sugarAmount = s.Split(':');
                        responseText.text = (string)sugarAmount.GetValue(1);
                    }
                }
                yield return new WaitForSeconds(1);
                check.SetActive(false);
                result.SetActive(true);
                doneAPI = true;
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

     
