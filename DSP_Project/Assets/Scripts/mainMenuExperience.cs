using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class mainMenuExperience : MonoBehaviour
{
    public TMP_Text expTotal, Level, ExpMissing, NextLevel;
    public Slider theSlider;
    private const int totalExperience = 100;
    private int currentMissingExp, expNum, theLevel,theNext;

    // Start is called before the first frame update
    private void Start()
    {
        expTotal.text = Game.current.thePlayer.experience.ToString();
        Level.text = Game.current.thePlayer.level.ToString();
        Debug.Log("EXPERIENCE" + Game.current.thePlayer.experience + " LEVEL" + Game.current.thePlayer.level);
        try
        {
            expNum = Int32.Parse(expTotal.text);
            theLevel = Int32.Parse(Level.text);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
        currentMissingExp = totalExperience-expNum;
        ExpMissing.text = currentMissingExp.ToString();
        theNext = theLevel + 1;
        NextLevel.text = theNext.ToString();
        theSlider.value = expNum/100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateTheLevel(int currentLevel)
    {
        
        Level.text = currentLevel.ToString();
        theNext = currentLevel + 1;
        NextLevel.text = theNext.ToString();
    }
    public void UpdateTheExperience(int currentExperience)
    {
        expTotal.text = currentExperience.ToString();
        currentMissingExp = totalExperience - currentExperience;
        ExpMissing.text = currentMissingExp.ToString();
        theSlider.value = currentExperience;

    }
}
