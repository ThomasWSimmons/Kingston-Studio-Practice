using UnityEngine;
using System.Collections;

public class readCamera : MonoBehaviour
{
    private Camera _camera;
    private Texture2D _screenShot;
    //to allow disabling
    public static bool doneCamera;
    private void OnEnable()
    {
        doneCamera = false;
        _camera = GameObject.Find("/ARCamera").GetComponent<Camera>();
        if(_camera!= null)
        {
            Debug.Log("found");
            Debug.Log("theScreenShot:"+_screenShot);
        }
    }
    public void Screenshot()
    {
        StartCoroutine(TakeScreenShot());
    }
    public IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        _camera.targetTexture = rt;
        _screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        _camera.Render();
        RenderTexture.active = rt;
        _screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _screenShot.Apply();
        _camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        ReadBarcodeFromFile.inputTexture = _screenShot;

        Debug.Log("screenshotTaken" + _screenShot);
        doneCamera = true;
       
    }

}
