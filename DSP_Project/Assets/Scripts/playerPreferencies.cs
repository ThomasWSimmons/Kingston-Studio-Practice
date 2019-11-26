using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class playerPreferencies : MonoBehaviour
{
    public TextMeshProUGUI theUserName,theUserDiabeteType, theUserRatio,thePetName;
    public TMP_InputField theNewUserName, theNewRatio;
   
    private string currentName;
    private int currentRatio;
    // Start is called before the first frame update
    void Start()
    {
        currentName= PlayerPrefs.GetString("username");
        currentRatio = PlayerPrefs.GetInt("ratio");
        theUserRatio.text = currentRatio.ToString();
        theUserDiabeteType.text = PlayerPrefs.GetString("diabeteType");
        theUserName.text = PlayerPrefs.GetString("username");
        thePetName.text = PlayerPrefs.GetString("petName");

    }
    public void changeUserName()
    {

        if (PlayerPrefs.HasKey("username"))
        {
            if(theNewUserName.text.Length>0)
            { 
                PlayerPrefs.SetString("username", theNewUserName.text);
                theUserName.text = PlayerPrefs.GetString("username");
                Debug.Log("changed name to " + theNewUserName.text);
            }
        
        }

    }
    public void changeTheRatio()
    {
        if (PlayerPrefs.HasKey("ratio"))
        {
            if (theNewRatio.text.Length > 0)
            {
                int theRatio = int.Parse(theNewRatio.text);
                if (theRatio != 0)
                {
                    PlayerPrefs.SetInt("ratio", theRatio);
                    Debug.Log("changed ratio to " + theRatio);
                }
                else
                {
                    Debug.Log("unchanged ratio to " + theRatio);
               }

            }
        }

    }
   

}
