using UnityEngine;
using System.Collections;
using ZXing;

public class ReadBarcodeFromFile : MonoBehaviour
{
    public static Texture2D inputTexture; // Note: [x] Read/Write must be enabled from texture import settings
    private bool singleRead;
    public GameObject loading;
    
    private void Start()
    {
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
        var result = reader.Decode(barcodeBitmap, inputTexture.width, inputTexture.height);
        // do something with the result
        if (result != null)
        {
            //Debug.Log(result.BarcodeFormat.ToString());
            singleRead = true;
            inputTexture = null;
            Debug.Log(result.Text);
            loading.SetActive(false);

            API.scanned = true;
            API.barcode = result.Text;

        }
        else
        {
            Debug.Log("no");
        }
    }
   
}