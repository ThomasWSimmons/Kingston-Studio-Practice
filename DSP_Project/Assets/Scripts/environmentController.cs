 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentController : MonoBehaviour
{
    private InstantTrackingController trackerScript;
    private GameObject ButtonParent;

    // Start is called before the first frame update
    void Start()
    {
       
        trackerScript = GameObject.Find("Controller").gameObject.GetComponent<InstantTrackingController>();
        ButtonParent = GameObject.Find("Buttons Parent");

        trackerScript._gridRenderer.enabled = false;
        ButtonParent.SetActive(false);
    }
    private void OnEnable()
    {
        trackerScript._gridRenderer.enabled = false;
        ButtonParent.SetActive(false);
    }
    private void OnDisable()
    {
        ButtonParent.SetActive(true);
    }
    
    // Update is called once per frame

}
