using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class updateCalorieSlider : MonoBehaviour
{
    public TMP_Text caloriesTotal, caloriesGoal;
    public Slider caloriesSlider;
    public static bool update;
    private int current, total;
    private void Start()
    {
        caloriesTotal.text = Game.current.thePlayer.caloriesCurrent.ToString();
        current = Game.current.thePlayer.caloriesCurrent;
        caloriesGoal.text = PlayerPrefs.GetInt("calories").ToString();
        total = PlayerPrefs.GetInt("calories");
        caloriesSlider.value = (float)current / total;
        Debug.Log("CURRR "+current+"/"+total+"= "+current / total);
    }
    private void Update()
    {
        if(update)
        {
            Debug.Log("UPDD");
            caloriesTotal.text = Game.current.thePlayer.caloriesCurrent.ToString();
            current = Game.current.thePlayer.caloriesCurrent;
            caloriesSlider.value = (float)current / total;
            update = false;
        }
    }
}
