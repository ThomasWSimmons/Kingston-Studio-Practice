using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class profileSETUP : MonoBehaviour
{

    public TMP_InputField theUserName, thePassword, theEmail, theAvatarName, calories, loginName, loginPassword,confirmPasswordText;
    public TMP_Text avatarName;
    public TMP_Dropdown theType, Ratio;
    private int theRatio, theDiabeticType;
    public GameObject errorname, errorAtio, errorPetName, errorPassword, errorEmail, errorType, errorLoginName, errorLoginPassword,errorCalories, errorPasswordConfirmed;
    public GameObject profile, avatar, ratio;
    public Toggle theCheck;
    private bool okPass, okUserName, goodToGo, username, pass, mail, avat, cals, type;
    // Start is called before the first frame update
    void Start()
    {
        okPass = okUserName = false;
        goodToGo = username = pass = mail = avat = cals = type = false;
        
    }
    public void compareUserName()
    {
        Debug.Log(PlayerPrefs.GetString("username"));
        Debug.Log(loginName.text);
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
        username = false;
        if (theUserName.text.Length > 0)
        {
            username = true;
            Debug.Log("added " + theUserName.text) ;

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
    public void checkPasswordEntered()
    {
        if (!PlayerPrefs.HasKey("password"))
        {
            confirmPasswordText.DeactivateInputField();
            errorPassword.SetActive(true);
        }
    }
    public void checkPassword()
    {
        pass = false;
        Debug.Log(confirmPasswordText.text + " conf");
            if (confirmPasswordText.text.Length > 0)
            {

            Debug.Log(confirmPasswordText.text + " vs " + PlayerPrefs.GetString("password"));
            if (confirmPasswordText.text.Equals(PlayerPrefs.GetString("password")))
            {
                pass = true;
            }
            else
            {
                errorPasswordConfirmed.SetActive(true);
                pass = false;
            }
            }
            else
            {
                errorPasswordConfirmed.SetActive(true);
            }  
    }
    public void SetUpEmail()
    {
        mail = false;

        if (theEmail.text.Length > 0)
        {

            mail = true;
            Debug.Log("added");

            PlayerPrefs.SetString("email", theEmail.text);


        }
        else
        {
            errorEmail.SetActive(true);

        }



    }
    public void SetUpCalories()
    {
        cals = false;
        Debug.Log(goodToGo);
        if (int.TryParse(calories.text, out int theAmount))
        {

            cals = true;
            theAmount = int.Parse(calories.text);
            if (theAmount >= 1200 && theAmount<=4000)//leaving a big range for daily calories intake depending on the user profile
            {
                PlayerPrefs.SetInt("calories", theAmount);
            }
            else
            {
                cals = false;
                errorCalories.SetActive(true);
            }
        }
        else
        {

            errorCalories.SetActive(true);

        }
    }
    public void SetUpType()
    {
        type = false;
        Debug.Log(goodToGo);
        if (theType.value > 0)
        {

            type = true;
            Debug.Log("ADDED");


            theDiabeticType = theType.value;
            if (theDiabeticType == 1)
            {
                ratio.SetActive(true);
            }

            PlayerPrefs.SetInt("type", theDiabeticType);


        }
        else
        {
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

        }

    }
    public void SetUpAvatarName()
    {
        avat = false;
        Debug.Log(goodToGo);
        if (theAvatarName.text.Length > 0)
        {

            avat = true;

            theAvatarName.text.Substring(0, 1).ToUpper();
            string temp = theAvatarName.text.Substring(0, 1).ToUpper() + theAvatarName.text.Substring(1, theAvatarName.text.Length - 1);
            PlayerPrefs.SetString("avatarName", temp);
            avatarName.text = temp;

        }
        else
        {

            errorPetName.SetActive(true);
        }



    }
    public void checkGoodToGo()
    {
        if (username && mail && cals && pass && avat && type)
        {
            goodToGo = true;
            GameManager.theMenu = 1;
            ToAvatar();
        }
    }
    public void ToAvatar()
    {

        if (goodToGo)
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
