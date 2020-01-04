using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData // here we store informations of our player
{
    public int experience;
    public int level;
    public int caloriesCurrent,sugarCurrent;
    public int bodyIndex, hairIndex, kitIndex, faceIndex,currentMenu;
    public PlayerData()
    {
        caloriesCurrent = sugarCurrent=0;
        level = 1;//experience starts at level 1
        experience = 0;
        bodyIndex= hairIndex= kitIndex= faceIndex=currentMenu=0;
}
}