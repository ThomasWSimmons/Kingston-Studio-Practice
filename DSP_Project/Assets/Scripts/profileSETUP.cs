using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class profileSETUP : MonoBehaviour
{

    public TMP_InputField theUserName, thePassword, theEmail, theAvatarName,calories, loginName, loginPassword;
    public TMP_Text avatarName;
    public TMP_Dropdown theType, Ratio;
    private int theRatio, theDiabeticType, goodToGo;
    public GameObject errorname, errorAtio, errorPetName, errorPassword, errorEmail, errorType, errorLoginName, errorLoginPassword;
    public GameObject profile, avatar,ratio;
    public Toggle theCheck;
    private bool okPass, okUserName;
    // Start is called before the first frame update
    void Start()
    {
        okPass = okUserName = false;
        goodToGo = 0;
        // PlayerPrefs.SetInt("firstLaunch", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void compareUserName()
    {
        if (loginName.text.Equals(PlayerPrefs.GetString("username")))
        {
            okUserName = true;

        }
        else if (loginName.text.Length < 0 || !loginName.text.Equals(PlayerPrefs.GetString("username")))
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
            Debug.Log(PlayerPrefs.GetString("username"));
            goodToGo++;
        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            errorname.SetActive(true);
            Debug.Log(" name error");
        }



    }
    public void SetUpPassword()
    {

        if (thePassword.text.Length > 0)
        {
            PlayerPrefs.SetString("password", thePassword.text);
            Debug.Log(PlayerPrefs.GetString("password"));
            goodToGo++;
        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            Debug.Log("password error");
            errorPassword.SetActive(true);
        }



    }
    public void SetUpEmail()
    {
        if (theEmail.text.Length > 0)
        {
            PlayerPrefs.SetString("email", theEmail.text);
            Debug.Log(PlayerPrefs.GetString("email"));
            goodToGo++;

        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            errorEmail.SetActive(true);
            Debug.Log("email error");
        }



    }
    public void SetUpCalories()
    {
        int theAmount;
        if (int.TryParse(calories.text, out theAmount))
        {
            theAmount = int.Parse(calories.text);
            PlayerPrefs.SetInt("calories", theAmount);
            Debug.Log(PlayerPrefs.GetInt("calories"));
            goodToGo++;

        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            errorEmail.SetActive(true);
            Debug.Log("email error");
        }



    }
    public void SetUpType()
    {
        if (theType.value > 0)
        {
            theDiabeticType = theType.value;
            if(theDiabeticType==1)
            {
                ratio.SetActive(true);
            }
            Debug.Log("type:" + theDiabeticType);
            PlayerPrefs.SetInt("type", theDiabeticType);
            goodToGo++;
        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            Debug.Log("type error");
            errorType.SetActive(true);
        }



    }
    public void SetUpRatio()
    {
        if (Ratio.value > 0)
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
    public void SetUpAvatarName()
    {
        if (theAvatarName.text.Length > 0)
        {
            Debug.Log("pet name set up");
            goodToGo++;
            theAvatarName.text.Substring(0, 1).ToUpper();
            string temp = theAvatarName.text.Substring(0, 1).ToUpper() + theAvatarName.text.Substring(1, theAvatarName.text.Length-1);
            PlayerPrefs.SetString("avatarName", temp);
            avatarName.text = temp;
            Debug.Log(avatarName.text);
        }
        else
        {
            if (goodToGo > 0)
            {
                goodToGo--;
            }
            Debug.Log("pet name error");
            errorPetName.SetActive(true);
        }



    }
    public void ToAvatar()
    {

        if (goodToGo == 6)
        {

            if (PlayerPrefs.GetInt("type") == 1 && !PlayerPrefs.HasKey("ratio"))
            {
                Debug.Log("error");
                errorAtio.SetActive(true);
            }
            else
            {

                Debug.Log("active");
                avatar.SetActive(true);
                GameManager.theMenu = 1;

            }


        }
        else if (!PlayerPrefs.HasKey("username"))
        {
            Debug.Log("in1");
            errorname.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("password"))
        {
            Debug.Log("2");
            errorPassword.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("email"))
        {
            Debug.Log("3");
            errorEmail.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("type"))
        {
            Debug.Log("4");
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
        else if (!okUserName)
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
