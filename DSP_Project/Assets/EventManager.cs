using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject myPanel;
    private static bool Active;
    private static string theName;
    private List<GameObject> allTheText = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in myPanel.transform)
        {
            t.gameObject.SetActive(false);//deactivate panels
            allTheText.Add(t.gameObject);//has the text to display    
        }

        myPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!myPanel.activeSelf && Active)
        {
            myPanel.SetActive(true);
            switch (theName)
            {
                case "sugar":
                    allTheText.Find((GameObject obj) => obj.name == "sugarInfo").SetActive(true);

                    break;
                case "coffee":
                    allTheText.Find((GameObject obj) => obj.name == "coffeeInfo").SetActive(true);
                    break;
            }
        }
        else if( myPanel.activeSelf && !Active)
        {
            myPanel.SetActive(false);
            switch (theName)
            {
                case "sugar":
                    allTheText.Find((GameObject obj) => obj.name == "sugarInfo").SetActive(false);
                    break;
                case "coffee":
                    allTheText.Find((GameObject obj) => obj.name == "coffeeInfo").SetActive(false);
                    break;

            }
        }
    }
    public static void Activate(string name)
    {
        Active = true;
        theName = name;
            
    }
    public static void DeActivate(string name)
    {
        Active = false;
        theName = name;
    }
}
