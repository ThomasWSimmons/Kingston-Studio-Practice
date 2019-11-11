using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class handleDropDown : MonoBehaviour
{
    public TMP_Dropdown myDropdown;
    public GameObject myChar;
    private List<GameObject> objects = new List<GameObject>();
    private void Start()
    {
        if (myDropdown.name == "eyesColor")
        {
           
            foreach (Transform t in myChar.transform)
            {
                Debug.Log(t.name);
                if (t.name == "eye1")
                {
                    objects.Add(t.gameObject);
                    Debug.Log(objects[0].name);
                }// Do something to child one
                else if (t.name == "eye2")
                {
                    objects.Add(t.gameObject);
                    Debug.Log(objects[1].name);
                }// Do something to child two
            }
        }

    }
    private void Update()
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
                        foreach (GameObject t in objects)
                        {
                          t.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                        }
                        break;
                    case 1:
                        foreach (GameObject t in objects)
                        {
                            t.gameObject.GetComponent<Renderer>().material.color = Color.black;
                        }
                        break;
                    case 2:
                        foreach (GameObject t in objects)
                        {
                            t.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        }
                        break;
                }
                break;

        }

    }
}
