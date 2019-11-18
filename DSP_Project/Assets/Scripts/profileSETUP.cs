using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class profileSETUP : MonoBehaviour
{
    public TMP_InputField theUserName;
    public TMP_InputField theUserRatio;
    private int theRatio;
    public GameObject errorname, errorAtio;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("firstLaunch", 1);
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    public void setUpName()
    {
        if (!PlayerPrefs.HasKey("username"))
        {
            if (theUserName.text.Length > 0)
            {
                PlayerPrefs.SetString("username", theUserName.text);
            }
            else
            {
                errorname.SetActive(true);
            }
       
        }
    
    }
    public void setUpRatio()
    {
        if (!PlayerPrefs.HasKey("ratio"))
        {
            if (theUserRatio.text.Length > 0)
            {
                theRatio = int.Parse(theUserRatio.text);
                PlayerPrefs.SetInt("ratio", theRatio);
                Debug.Log("ratio =" + theRatio);
            }
            else
            {
                errorAtio.SetActive(true);
            }
           
        }
    
    }
    public void toMenu()
    {
        if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("ratio"))
        {
            SceneManager.LoadScene("Menu");
        }
        else if (!PlayerPrefs.HasKey("username"))
        {    
            errorname.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("ratio"))
        {
            errorAtio.SetActive(true);
        }
    }
}
