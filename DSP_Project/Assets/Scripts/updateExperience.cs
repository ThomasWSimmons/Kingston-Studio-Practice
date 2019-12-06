using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class updateExperience : MonoBehaviour
{
    public Sprite[] whichNutriscore;
    public TMP_Text expTotal, Level, ExpMissing, NextLevel, expNutrigrade,youAreNow;
    public GameObject highlightNutri, highlightScan,plusOne,plusLevel,levelUp,exit;
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
        expNum = Game.current.thePlayer.experience;
        theLevel = Game.current.thePlayer.level;


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
            Debug.Log("LEVELup - increasing of: " + (left + scanExp));
            StartCoroutine(updateSliderHighlight(toIncrease,(left+scanExp)));
            expTotal.text = (left + scanExp) + " exp total";
            currentMissingExp = totalExperience-(left+scanExp);
            ExpMissing.text = currentMissingExp + " exp to";
            
            single++;
        }
        else if (levelUpScan && single == 0)//level up detected after scan exp added - level up then add rest to new slider
        {
            Debug.Log("3 - increasing of: " + toIncrease);
            Debug.Log("LEVELup - increasing of: " + left);
            StartCoroutine(updateSliderHighlight(toIncrease,left));
           
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
                containerGraph.updateTheList(100);
                nutrigrade.sprite = whichNutriscore[0];
                break;
            case "b":
                containerGraph.updateTheList(80);
                nutrigrade.sprite = whichNutriscore[1];
                break;
            case "c":
                containerGraph.updateTheList(60);
                nutrigrade.sprite = whichNutriscore[2];
                break;
            case "d":
                containerGraph.updateTheList(40);
                nutrigrade.sprite = whichNutriscore[3];
                break;
            case "e":
                containerGraph.updateTheList(20);
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
        PlayerPrefs.SetInt("experience", theExpAfterScan);//used to save correctly
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
            left = toIncrease-scanExp;
        }
        
        
    }
    void updateLevel()
    {
        int current = Game.current.thePlayer.level;
        current += 1;
        plusLevel.SetActive(true);
        Game.current.thePlayer.level = current;//update Game info
        PlayerPrefs.SetInt("Level", current);//used to save correctly
        theSlider.value = 0;
        Game.current.thePlayer.experience = 0;
        PlayerPrefs.SetInt("experience", 0);//used to save correctly
        Level.text = "Level " + current.ToString();
        NextLevel.text = "Level "+(current + 1);
        youAreNow.text = "You are now Level" + current.ToString();
        levelUp.SetActive(true);
        mainMenuExperience.active = true;//display notif on main menu page
        
    }
    void displayLevel()
    {
        int current = Game.current.thePlayer.level;
        Level.text = "Level " + current;
        NextLevel.text = "Level " + (current + 1);
    }

    IEnumerator updateSliderHighlight(int exp1,int exp2)//slider update with level up
    {
        
        highlightNutri.SetActive(true);
               
        yield return new WaitForSeconds(1);
        for (int i = 0; i < exp1; i++)
        {
            plusOne.SetActive(true);
            theSlider.value += 1 / 100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            yield return new WaitForSeconds(.15f);
            plusOne.SetActive(false);
        }
        highlightNutri.SetActive(false);
        Math.Round(theSlider.value);
        updateLevel();
        yield return new WaitForSeconds(1);
        plusLevel.SetActive(false);
        highlightScan.SetActive(true);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < exp2; i++)
        {
            plusOne.SetActive(true);
            theSlider.value += 1 / 100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            yield return new WaitForSeconds(.15f);
            plusOne.SetActive(false);
        }
        highlightScan.SetActive(false);
        Math.Round(theSlider.value);
        Game.current.thePlayer.experience = exp2;
        exit.SetActive(true);

    }
   
    IEnumerator udpateSlider(int exp)//slider update without level up
    {
        int exp1 = exp - scanExp;
        highlightNutri.SetActive(true);

        yield return new WaitForSeconds(1);
        for (int i = 0; i < exp1; i++)
        {
            plusOne.SetActive(true);
            theSlider.value += 1 / 100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            yield return new WaitForSeconds(.1f);
            plusOne.SetActive(false);
        }
        Math.Round(theSlider.value);
        highlightNutri.SetActive(false);
        yield return new WaitForSeconds(1);
        highlightScan.SetActive(true);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < scanExp; i++)
        {
            plusOne.SetActive(true);
            theSlider.value += 1 / 100f;
            expTotal.text = (int)(theSlider.value * 100f) + " exp total points";
            yield return new WaitForSeconds(.1f);
            plusOne.SetActive(false);
        }
        highlightScan.SetActive(false);
        Math.Round(theSlider.value);
        exit.SetActive(true);
    }
}
