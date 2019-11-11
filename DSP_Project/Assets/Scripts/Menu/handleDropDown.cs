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

    List<string> bodyColors = new List<string>() { "Standard","magenta", "blue", "red"};
    List<string> eyesColors = new List<string>() { "Standard", "black", "green", "blue" };
    private void Start()
    {

        Populate(myDropdown.name);

    }
    public void Update()
    {

        switch (myDropdown.name)
        {
            case "bodyColor":
                switch (myDropdown.value)
                {

                    case 0:
                        myChar.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        break;
                    case 1:
                        myChar.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                        break;
                    case 2:
                        myChar.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case 3:
                        myChar.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        break;
                }

                break;
            case "eyesColor":
                switch (myDropdown.value)
                {

                    case 0:
                        foreach (Transform s in myChar.transform)
                        {
                            s.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        }

                        break;
                    case 1:
                        foreach (Transform s in myChar.transform)
                        {
                            s.gameObject.GetComponent<Renderer>().material.color = Color.black;
                        }

                        break;
                    case 2:
                        foreach (Transform s in myChar.transform)
                        {
                            s.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        }

                        break;
                    case 3:
                        foreach (Transform s in myChar.transform)
                        {
                            s.gameObject.GetComponent<Renderer>().material.color = Color.blue;

                        }


                        break;

                }
                break;

        }
    }
        void Populate(string theName)
        {
            switch (theName)
            {

                case "bodyColor":

                    myDropdown.AddOptions(bodyColors);
                    break;
                case "eyesColor":


                    myDropdown.AddOptions(eyesColors);

                    break;
            }
        }

    }