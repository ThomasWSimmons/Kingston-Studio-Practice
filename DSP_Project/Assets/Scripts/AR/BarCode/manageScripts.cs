using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageScripts : MonoBehaviour
{
    public GameObject cameraRead;
    public GameObject reader;
    public GameObject requester;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraRead.activeSelf)
        {
            if(readCamera.doneCamera)
            {
                cameraRead.SetActive(false);
            }
        }
        if(reader.activeSelf)
        {
            if(ReadBarcodeFromFile.doneReader)
            {
                reader.SetActive(false);
            }
        }
        if(requester.activeSelf)
        {
            if(API.doneAPI)
            {
                requester.SetActive(false);
            }
        }

    }
}
