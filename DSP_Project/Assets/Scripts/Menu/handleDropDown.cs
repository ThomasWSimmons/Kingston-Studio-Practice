using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class handleDropDown : MonoBehaviour
{
    public TMP_Dropdown myDropdown;
    public GameObject myChar;
    private List<GameObject> eyes = new List<GameObject>();
    private GameObject body;
    public static bool reset;


    private void Start()
    {
        reset = false;

        switch (myDropdown.name)
        {
            case "bodyColor":
                myChar.gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            case "eyesColor":
                foreach (Transform s in myChar.transform)
                {
                    eyes.Add(s.gameObject);
                }
                foreach (GameObject t in eyes)
                {
                    t.gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
                break;
        }
       


    }
    public void Update()
    {
        if (!reset)
        {
            switch (myDropdown.name)
            {
                case "bodyColor":
                    switch (myDropdown.value)
                    {
                        case 0:
                            myChar.gameObject.GetComponent<Renderer>().material.color = Color.red;
                            break;
                        case 1:
                            myChar.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                            break;
                        case 2:
                            myChar.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                            break;
                    }
                    break;
                case "eyesColor":
                    switch (myDropdown.value)
                    {
                        case 0:
                            foreach (GameObject t in eyes)
                            {
                                t.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                            }
                            break;
                        case 1:
                            foreach (GameObject t in eyes)
                            {
                                t.gameObject.GetComponent<Renderer>().material.color = Color.black;
                            }
                            break;
                        case 2:
                            foreach (GameObject t in eyes)
                            {
                                t.gameObject.GetComponent<Renderer>().material.color = Color.green;
                            }
                            break;
                    }
                    break;

            }
        }
        else if(reset)
        {
      
            onReset();
        }

    }
    public void onReset()
    {
        reset = false;
        switch (myDropdown.name)
        {
            case "bodyColor":
                myChar.gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            case "eyesColor":
                foreach (GameObject s in eyes)
                {
                    s.gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
                break;

        }
        
     

    }
}