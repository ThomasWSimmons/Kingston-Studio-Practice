using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class updateExperience : MonoBehaviour
{
    public Sprite[] whichNutriscore;
    public TMP_Text expTotal, Level, ExpMissing, NextLevel, expNutrigrade;
    public GameObject highlightNutri, highlightScan;
    public Slider theSlider;
    public Image nutrigrade;
    private const int totalExperience = 100;
    private const int scanExp = 10;
    private int currentMissingExp,single,toIncrease, left, expNum, theLevel, theNext;
    private bool levelUpNutri,levelUpScan;
    // Start is called before the first frame update
    void Start()
    {
        expTotal.text = Game.current.thePlayer.experience +"exp total";
        Level.text = "Level "+Game.current.thePlayer.level.ToString();
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
        currentMissingExp = totalExperience - expNum;
        ExpMissing.text = currentMissingExp+ " exp to";
        theNext = theLevel + 1;
        NextLevel.text = "Level "+theNext;
        theSlider.value = expNum / 100f;
        levelUpNutri =levelUpScan= false;
        single = toIncrease=left=0;
        expNutrigrade.text = PlayerPrefs.GetInt("nutriExp").ToString();
        updateNutrigrade();
        displayLevel();//no level up for now can display the level
        checkLevelUp();
        

    }
    private void Update()
    {
        //exp from nutri and scan added - anim - slider increase
        if (!levelUpNutri && !levelUpScan && single == 0)
        {
            Debug.Log("1 - increasing of: " + toIncrease);
            StartCoroutine(udpateSlider(toIncrease));
            ExpMissing.text = currentMissingExp + " exp to";
            
            single++;
        }
        else if (levelUpNutri && single == 0)//level up detected after nutri exp added - level up - then add rest
        {
            Debug.Log("2 - increasing of: " + toIncrease);
            StartCoroutine(updateSliderHighlight(toIncrease,"nutri"));
            updateLevel();
            Debug.Log("LEVELup - increasing of: " + (left+scanExp));
            StartCoroutine(updateSliderHighlight(left + scanExp, "scan"));
            expTotal.text = (left + scanExp) + " exp total";
            currentMissingExp = totalExperience-(left+scanExp);
            ExpMissing.text = currentMissingExp + " exp to";
            
            single++;
        }
        else if (levelUpScan && single == 0)//level up detected after scan exp added - level up then add rest to new slider
        {
            Debug.Log("3 - increasing of: " + toIncrease);
            StartCoroutine(updateTotalExp(toIncrease));
            updateLevel();
            Debug.Log("LEVELup - increasing of: " + left);
            StartCoroutine(udpateSlider(left));
            expTotal.text = left+ " exp total";
            currentMissingExp = totalExperience - left;
            ExpMissing.text = currentMissingExp + " exp to";
            single++;
        }
       
        //if level up - anim - reset slider - increase level;
    }
    void updateNutrigrade()
    {
        switch (PlayerPrefs.GetString("nutriGrade"))
        {
            case "a":
                nutrigrade.sprite = whichNutriscore[0];
                break;
            case "b":
                nutrigrade.sprite = whichNutriscore[1];
                break;
            case "c":
                nutrigrade.sprite = whichNutriscore[2];
                break;
            case "d":
                nutrigrade.sprite = whichNutriscore[3];
                break;
            case "e":
                nutrigrade.sprite = whichNutriscore[4];
                break;
        }
        
    }
  
    void checkLevelUp()
    {
        int current = Game.current.thePlayer.experience;
        Debug.Log(current + "CURRENT");
        int nutriExp = PlayerPrefs.GetInt("nutriExp");
        Debug.Log(nutriExp + "NUTRIEXP");
        int theExpAfterNutri = Game.current.thePlayer.experience + nutriExp;
        int theExpAfterScan = theExpAfterNutri + scanExp;
        currentMissingExp = totalExperience - theExpAfterScan;
        Game.current.thePlayer.experience = theExpAfterScan;
        toIncrease = theExpAfterScan-current;
        if(theExpAfterNutri>totalExperience)
        {
            levelUpNutri = true;
            toIncrease = totalExperience - current;//will increase the bar of toIncrease amount.
            left = nutriExp - toIncrease; //will increase new slider bar of left amount
        }
        else if(theExpAfterScan>totalExperience)
        {
            levelUpScan = true;
            toIncrease = totalExperience - current;
            left = scanExp - toIncrease;
        }
        
        
    }
    void updateLevel()
    {
        int current = Game.current.thePlayer.level;
        current += 1;
        Game.current.thePlayer.level = current;//update Game info
        theSlider.value = 0;
        Level.text = current.ToString();
        NextLevel.text = "Level "+(current + 1);
    }
    void displayLevel()
    {
        int current = Game.current.thePlayer.level;
        Level.text = "Level " + current;
        NextLevel.text = "Level " + (current + 1);
    }

    IEnumerator updateTotalExp(int exp)
    {
        highlightNutri.SetActive(true);
        yield return new WaitForSeconds(1);
        for(int i=0; i<exp;i++)
        {
            theSlider.value += 1/100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            yield return new WaitForSeconds(.25f);
        }
        highlightNutri.SetActive(false);
        yield return new WaitForSeconds(1f);
        highlightScan.SetActive(true);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < exp; i++)
        {
            theSlider.value += 1/100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            yield return new WaitForSeconds(.25f);
        }
        highlightScan.SetActive(false);

    }
    IEnumerator updateSliderHighlight(int exp, string highlight)
    {
        switch(highlight)
        {
            case "nutri":
                highlightNutri.SetActive(true);
                break;
            case "scan":
                highlightScan.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < exp; i++)
        {
            theSlider.value += 1 / 100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            yield return new WaitForSeconds(.25f);
        }
        switch (highlight)
        {
            case "nutri":
                highlightNutri.SetActive(false);
                break;
            case "scan":
                highlightScan.SetActive(false);
                break;
        }

    }
   
    IEnumerator udpateSlider(int exp)
    {
        for (int i = 0; i < exp; i++)
        {
            theSlider.value += 1 / 100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            Debug.Log(theSlider.value);
            yield return new WaitForSeconds(.25f);
        }
    }
}
