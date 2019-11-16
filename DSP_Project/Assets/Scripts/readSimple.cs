using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZXing;

public class readSimple : MonoBehaviour
{
    public Texture2D inputTxture; // Note: [x] Read/Write must be enabled from texture import settings
    public Text theText;
    private string test;
    private string test2;
    private string barcode;
    private const string endpoint = "https://world.openfoodfacts.org/api/v0/product/0613008739089.json";
    //private const string endpoint = "https://www.foodrepo.org/api/v3/products?excludes=images%2Cid%2Ccountry%2Cbarcode%2Cname_translations%2Cdisplay_name_translations%2Cingredients_translations%2Corigin_translations%2Cstatus%2Cquantity%2Cunit%2Cportion_quantity%2Cportion_unit%2Calcohol_by_volume%2Ccreated_at%2Cupdated_at%2Cnutrients-salt&barcodes=";
    private string theURL;

    private void Start()
    {
       IBarcodeReader reader = new BarcodeReader();
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
           
          StartCoroutine(GetRequest(endpoint));
            
        }
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

            theText.text = "Received: " + webRequest.downloadHandler.text;//prints results
            test = webRequest.downloadHandler.text;
            //test2 = webRequest.downloadHandler.text;
           //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            string[] words = test.Split(',');
                foreach(string s in words)
                {
                    if (s.Contains("sugars"))
                    {
                        Debug.Log(s);
                    }
                }
           // string[] otherWords = test2.Split(':');
              /*  for(int i = 25; i<32;i++)
                {
                    Debug.Log(words[i]);//sugars
                }*/
                /*for (int i = 35; i < 40; i++)
                {
                    Debug.Log(words[i]);//carbs
                }*/
                /*  foreach (string t in otherWords)
                  {
                      Debug.Log(t);
                  }*/
            }
    }

}
}
