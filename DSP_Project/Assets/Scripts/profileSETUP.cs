using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class profileSETUP : MonoBehaviour
{
    
    public TMP_InputField theUserName,thePassword,theEmail,theAvatarName, loginName,loginPassword;
    public TMP_Dropdown theType, Ratio;
    private int theRatio,theDiabeticType;
    public GameObject errorname, errorAtio,errorPetName,errorPassword,errorEmail,errorType,errorLoginName,errorLoginPassword;
    public GameObject profile;
    public Toggle theCheck;
    private bool okPass, okUserName;
    // Start is called before the first frame update
    void Start()
    {
        okPass = okUserName = false;
       // PlayerPrefs.SetInt("firstLaunch", 1);
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    public void compareUserName()
    {
        if(loginName.text.Equals(PlayerPrefs.GetString("username")))
        {
            okUserName = true;

        }
        else if(loginName.text.Length<0 || !loginName.text.Equals(PlayerPrefs.GetString("username")))
        {
            errorLoginName.SetActive(true);
        }
    }
    public void comparePassword()
    {
        if (loginPassword.text.Equals(PlayerPrefs.GetString("password")))
        {
            okPass = true;

        }
        else if (loginPassword.text.Length < 0 || !loginPassword.text.Equals(PlayerPrefs.GetString("password")))
        {
            errorLoginPassword.SetActive(true);
        }
    }
    public void SetUpName()
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
    public void SetUpPassword()
    {
       
            if (thePassword.text.Length > 0)
            {
                PlayerPrefs.SetString("password", thePassword.text);
            }
            else
            {
                errorPassword.SetActive(true);
            }

        

    }
    public void SetUpEmail()
    {
            if (theEmail.text.Length > 0)
            {
                PlayerPrefs.SetString("email", theEmail.text);
            }
            else
            {
                errorEmail.SetActive(true);
            }

        

    }
    public void SetUpType()
    {
        if (theType.value>0)
            {
                theDiabeticType = theType.value;
                Debug.Log("type:"+theDiabeticType);
                PlayerPrefs.SetInt("type", theDiabeticType);
            }
            else
            {
                errorType.SetActive(true);
            }

        

    }
    public void SetUpRatio()
    {
            if (Ratio.value>0)
            {
                
                switch (Ratio.value)
                {
                    case 1:
                        theRatio = 9;
                        break;
                    case 2:
                        theRatio = 10;
                        break;
                    case 3:
                        theRatio = 11;
                        break;

                }
                PlayerPrefs.SetInt("ratio", theRatio);
                Debug.Log("ratio =" + theRatio);
            }
    
    }
    public void SetUpPetName()
    {
        if (theAvatarName.text.Length > 0)
            {
                Debug.Log("pet name set up");
                PlayerPrefs.SetString("avatarName", theAvatarName.text);
               
            }
            else
            {
                errorPetName.SetActive(true);
            }

        

    }
    public void ToMainMenu()
    {
        if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("email") && PlayerPrefs.HasKey("type") && PlayerPrefs.HasKey("password") && PlayerPrefs.HasKey("avatarName"))
        {
            if (PlayerPrefs.GetInt("type") == 1)
            {
                if (!PlayerPrefs.HasKey("ratio"))
                {
                    Debug.Log("error");
                    errorAtio.SetActive(true);
                }
            }
            else
            {
                Debug.Log("active");
                GameManager.instance.MainMenu();
                
            }
          
        }
        else if (!PlayerPrefs.HasKey("username"))
        {    
            errorname.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("password"))
        {
            errorPassword.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("email"))
        {
            errorEmail.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("type"))
        {
            errorType.SetActive(true);
        }
    }
    public void ToMenu()
    {
        if (okPass && okUserName)
        {
            GameManager.instance.MainMenu();
        }
        else if (!okPass)
        {
            errorLoginPassword.SetActive(true);
        }
        else if(!okUserName)
        {
            errorLoginName.SetActive(true);
        }
      
    }
    public void setRememberMe()
    {
        if (theCheck.isOn)
        {
            Debug.Log("checked");
            PlayerPrefs.SetString("remember", "ok");
        }
        else
        {
            Debug.Log("unchecked");
            PlayerPrefs.DeleteKey("remember");

        }
    }
}
