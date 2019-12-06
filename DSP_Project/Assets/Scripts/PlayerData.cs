using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData // here we store informations of our player
{
    public int experience;
    public int level;
    public int bodyIndex, hairIndex, kitIndex, faceIndex,currentMenu;
    public PlayerData()
    {
        level = 0;
        experience = 0;
        bodyIndex= hairIndex= kitIndex= faceIndex=currentMenu=0;
}
}