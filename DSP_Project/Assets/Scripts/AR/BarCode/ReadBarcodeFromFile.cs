using UnityEngine;
using System.Collections;
using ZXing;
using UnityEngine.UI;

public class ReadBarcodeFromFile : MonoBehaviour
{
    public static Texture2D inputTexture; // Note: [x] Read/Write must be enabled from texture import settings
    private bool singleRead;
    public GameObject loading;
    public GameObject cross;
    public GameObject error;
    public Result result;

    //to allow disabling
    public static bool doneReader;
    private void OnEnable()
    {
        doneReader = false;
        singleRead = true;
    }
    private void Update()
    {
      
        if (inputTexture != null && singleRead)
        {
            singleRead = false; 
            read();
        }
       

    }
    void read()
    {
        IBarcodeReader reader = new BarcodeReader();
        // get texture Color32 array
        var barcodeBitmap = inputTexture.GetPixels32();
        // detect and decode the barcode inside the Color32 array
        result = reader.Decode(barcodeBitmap, inputTexture.width, inputTexture.height);
        
        // do something with the result
        if (result != null)
        {
          
            inputTexture = null;
            Debug.Log(result.Text);
            API.scanned = true;
            API.barcode = result.Text;
            doneReader = true;

        }
        else
        {
            loading.SetActive(false);
            cross.SetActive(true);
            float timer = 0;
            while(timer<1f)
             {
              timer += Time.deltaTime;
             }
            cross.SetActive(false);
            error.SetActive(true);
            doneReader = true;
            Debug.Log("no");
        }
    }
   

}