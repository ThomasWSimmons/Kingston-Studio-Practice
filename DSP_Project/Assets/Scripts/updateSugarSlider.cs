using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class updateSugarSlider : MonoBehaviour
{
    public TMP_Text sugarCurr;
    public Slider sugarSlider;
    
    private int current, total,sugarAmount;
    private void Start()
    {
        sugarCurr.text = Game.current.thePlayer.sugarCurrent.ToString()+"g of sugar";
        sugarAmount = Game.current.thePlayer.sugarCurrent;
        sugarSlider.value = sugarAmount / 100f;
       
    }
   
}
