using System;
using UnityEngine;
using TMPro;
public class changeDay : MonoBehaviour
{
    private TMP_Text theText;
    string toConvert;
    private int num;
    // Start is called before the first frame update
    void Start()
    {
        theText = gameObject.GetComponent<TMP_Text>();
        theText.text = DateTime.Today.DayOfWeek.ToString();
        toConvert = theText.text;
        ConvertDayToNUmber(toConvert);
        
    }
    private void ConvertDayToNUmber(string toConvertString)
    {
        switch (toConvertString)
        {
            case "Monday":
                num = 1;
                break;
            case "Tuesday":
                num = 2;
                break;
            case "Wednesday":
                num = 3;
                break;
            case "Thursday":
                num = 4;
                break;
            case "Friday":
                num = 5;
                break;
            case "Saturday":
                num =6;
                break;
            case "Sunday":
                num = 7;
                break;
        }
}
   public void ChangetheDayBack()
    {
        num--;
        if (num == 0)
        {
            num = 7;
        }
        Debug.Log(num);
        switch (num)
        {
            case 1:
                theText.text = "Monday";
                break;
            case 2:
                theText.text = "Tuesday";
                break;
            case 3:
                theText.text = "Wednesday";
                break;
            case 4:
                theText.text = "Thursday";
                break;
            case 5:
                theText.text = "Friday";
                break;
            case 6:
                theText.text = "Saturday";
                break;
            case 7:
                theText.text = "Sunday";
                break;
        }
       

    }
    public void ChangetheDayForward()
    {
        num++;
        if (num == 8)
        {
            num = 1;
        }
        Debug.Log(num);
        switch (num)
        {
            case 1:
                theText.text = "Monday";
                break;
            case 2:
                theText.text = "Tuesday";
                break;
            case 3:
                theText.text = "Wednesday";
                break;
            case 4:
                theText.text = "Thursday";
                break;
            case 5:
                theText.text = "Friday";
                break;
            case 6:
                theText.text = "Saturday";
                break;
            case 7:
                theText.text = "Sunday";
                break;
        }
    }
}
