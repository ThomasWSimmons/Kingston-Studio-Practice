using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class saveSystem
{
    public static List<Game> saved = new List<Game>();
    public static int experience, level,menu,body,hair,kit,face,mainMenu,caloriesCurrent,sugarCurrent;
	public static int isSaving;
	public static void SavePlayer()
	{
		isSaving = 1;
        Game.current.thePlayer.currentMenu = GameManager.theMenu;
        Game.current.thePlayer.experience = PlayerPrefs.GetInt("experience");
        Game.current.thePlayer.level = PlayerPrefs.GetInt("level");
        Game.current.thePlayer.bodyIndex=PlayerPrefs.GetInt("bodyIndex");
        Game.current.thePlayer.hairIndex= PlayerPrefs.GetInt("hairIndex");
        Game.current.thePlayer.kitIndex= PlayerPrefs.GetInt("kitIndex");
        Game.current.thePlayer.faceIndex=PlayerPrefs.GetInt("faceIndex");
        BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/ThePlayerInfo.gd";
		FileStream file = File.Create(path);
        PlayerData data = new PlayerData
        {
            sugarCurrent = Game.current.thePlayer.sugarCurrent,
            experience = Game.current.thePlayer.experience,
            level = Game.current.thePlayer.level,
            bodyIndex = Game.current.thePlayer.bodyIndex,
            hairIndex = Game.current.thePlayer.hairIndex,
            kitIndex = Game.current.thePlayer.kitIndex,
            faceIndex = Game.current.thePlayer.faceIndex,
            currentMenu = Game.current.thePlayer.currentMenu,
            caloriesCurrent = Game.current.thePlayer.caloriesCurrent
            
        };
        formatter.Serialize(file, data);//converts player data to binary file
		file.Close();

    }
         
             
	public static void LoadPlayer()
	{
		string path = Application.persistentDataPath + "/ThePlayerInfo.gd";
		if (File.Exists(path))
		{

			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = File.Open(path, FileMode.Open);
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
			stream.Close();
            if(PlayerPrefs.HasKey("remember"))
            {
                Debug.Log("OK");
                menu = 1;
            }
            else
            {
                menu = 0;
            }
            sugarCurrent = data.sugarCurrent;
            experience = data.experience;
            level = data.level;
            body = data.bodyIndex;
            hair = data.hairIndex;
            kit = data.kitIndex;
            face = data.faceIndex;
            caloriesCurrent = data.caloriesCurrent;
            mainMenu = data.currentMenu;
            GameManager.theMenu = mainMenu;
		}
		else
		{
			Debug.LogError("Save file not found in" + path);

		}


	}
}
