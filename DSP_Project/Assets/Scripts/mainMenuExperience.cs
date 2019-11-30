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
        Debug.Log("EXPERIENCE" + Game.current.thePlayer.experience + " LEVEL" + Game.current.thePlayer.level);
        expTotal.text = Game.current.thePlayer.experience+" exp total";
        Level.text = "Level "+Game.current.thePlayer.level;
        expNum = Game.current.thePlayer.experience;
        theLevel = Game.current.thePlayer.level;
        
        currentMissingExp = totalExperience-expNum;
        Debug.Log(currentMissingExp + "MISSING");
        ExpMissing.text = currentMissingExp+ "exp to";
        theNext = theLevel + 1;
        NextLevel.text = "level "+theNext;
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
