using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData // here we store informations of our player
{
    public int experience;
    public int level;

    public PlayerData()
    {
        level = 0;
        experience = 0;
    }
}