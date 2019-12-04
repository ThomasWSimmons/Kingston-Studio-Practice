using System;
using System.Linq;
using UnityEngine;
using TMPro;
public class changeDay : MonoBehaviour
{
    private TMP_Text theText;
    private DateTime theDate;
    private int num;
    // Start is called before the first frame update
    void Start()
    {
        theText = gameObject.GetComponent<TMP_Text>();
        theText.text = DateTime.Today.DayOfWeek.ToString();
        theDate = DateTime.Today.Date;
        num = Convert.ToInt32(theDate.Day.ToString());
      
    }

   public void changetheDayBack()
    {
        num--;
        if (num == 0)
        {
            num = 7;
        }
        Debug.Log(num);
        switch (num)
        {
            case 2:
                theText.text = "Monday";
                break;
            case 3:
                theText.text = "Tuesday";
                break;
            case 4:
                theText.text = "Wednesday";
                break;
            case 5:
                theText.text = "Thursday";
                break;
            case 6:
                theText.text = "Friday";
                break;
            case 7:
                theText.text = "Saturday";
                break;
            case 1:
                theText.text = "Sunday";
                break;
        }
       

    }
    public void changetheDayForward()
    {
        num++;
        if (num == 8)
        {
            num = 1;
        }
        Debug.Log(num);
        switch (num)
        {
            case 2:
                theText.text = "Monday";
                break;
            case 3:
                theText.text = "Tuesday";
                break;
            case 4:
                theText.text = "Wednesday";
                break;
            case 5:
                theText.text = "Thursday";
                break;
            case 6:
                theText.text = "Friday";
                break;
            case 7:
                theText.text = "Saturday";
                break;
            case 1:
                theText.text = "Sunday";
                break;
        }
    }
}
