using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manageAppearence : MonoBehaviour
{
    public enum AppearenceDetail
    {
        BODY_MODEL,
        KIT_MODEL,
        HAIR_MODEL,
        FACE_MODEL
    }

    [SerializeField]
    private Texture[] bodyModels;
    [SerializeField]
    private Texture[] hairModels;
    [SerializeField]
    private Texture[] kitModels;
    [SerializeField]
    private Texture[] faceModels;
    [SerializeField]
    private RawImage[] faceAnchor;
    [SerializeField]
    private RawImage[] bodyAnchor;
    [SerializeField]
    private RawImage[] hairAnchor;
    [SerializeField]
    private RawImage[] kitAnchor;

    Texture activeBody, activeHair, activeKit,activeFace;
    private int theLevel;
	int hairSize, kitSize,bodyIndex,hairIndex,kitIndex,faceIndex;
    public TMP_Text[] titles;
    public static bool ChangeEmotion;
    private void Start()
    {
        if (titles.Length > 0)
        {
            titles[0].text = "Body";
            titles[1].text = "Hair";
            titles[2].text = "Kit";
        }
        
        if (PlayerPrefs.HasKey("saved"))
        {
            ApplyModification(AppearenceDetail.BODY_MODEL, PlayerPrefs.GetInt("bodyIndex"));
            ApplyModification(AppearenceDetail.HAIR_MODEL, PlayerPrefs.GetInt("hairIndex"));
            ApplyModification(AppearenceDetail.KIT_MODEL, PlayerPrefs.GetInt("kitIndex"));
            ApplyModification(AppearenceDetail.FACE_MODEL, PlayerPrefs.GetInt("faceIndex"));
        }
        else
        {

            ApplyModification(AppearenceDetail.BODY_MODEL, 0);
            ApplyModification(AppearenceDetail.HAIR_MODEL, 0);
            ApplyModification(AppearenceDetail.KIT_MODEL, 0);
            ApplyModification(AppearenceDetail.FACE_MODEL,0);
        }
        theLevel = Game.current.thePlayer.level;
        setLists(theLevel);
    }
    private void Update()
    {
        if (ChangeEmotion)
        {
            ChangeEmotion = false;
            switch (PlayerPrefs.GetString("nutriGrade"))
            {
                case "a":
                    ApplyModification(AppearenceDetail.FACE_MODEL,0);
                    break;
                case "b":

                    ApplyModification(AppearenceDetail.FACE_MODEL, 1);
                    break;
                case "c":
                    ApplyModification(AppearenceDetail.FACE_MODEL, 1);
                    break;
                case "d":

                    ApplyModification(AppearenceDetail.FACE_MODEL, 2);
                    break;
                case "e":

                    ApplyModification(AppearenceDetail.FACE_MODEL, 2);
                    break;
            }
        }
    }
    void setLists(int currentLevel)
    {
        int currentSize= 3 + currentLevel;
        if(currentSize<hairModels.Length)
        {
            hairSize = currentSize;
        }
        else
        {
            hairSize = hairModels.Length;
        }
        if (currentSize < kitModels.Length)
        {
            kitSize = currentSize;
        }
        else
        {
            kitSize = kitModels.Length;
        }
         
    }
    public void PlusIndex(int index)//when click right arrow increases list index
    {
       
        switch(index)
        {
            case 1://body menu
                if (bodyIndex == 2)
                {
                    bodyIndex = 0;
                }
                else
                {
                    bodyIndex++;
                }
                titles[0].text = "Body " + (bodyIndex+1);
                ApplyModification(AppearenceDetail.BODY_MODEL,bodyIndex);
                break;
            case 2://hair menu
                if (hairIndex == (hairSize-1))
                {
                    hairIndex = 0;
                }
                else
                {
                    hairIndex++;
                }
                titles[1].text = "Hair " + (hairIndex+1);
                ApplyModification(AppearenceDetail.HAIR_MODEL, hairIndex);
                break;
            case 3://kit menu
                if (kitIndex == (kitSize-1))
                {
                    kitIndex = 0;
                }
                else
                {
                    kitIndex++;
                }
                titles[2].text = "Kit " + (kitIndex+1);
                ApplyModification(AppearenceDetail.KIT_MODEL, kitIndex);
                break;

        }
    }
    public void MinusIndex(int index)//when click left arrow decrease list index
    {
        switch (index)
        {
            case 1://body menu
                if (bodyIndex == 0)
                {
                    bodyIndex = 2;
                }
                else
                {
                    bodyIndex--;
                }
                titles[0].text = "Body " + (bodyIndex+1);
                ApplyModification(AppearenceDetail.BODY_MODEL, bodyIndex);
                break;
            case 2://hair menu
                if (hairIndex == 0)
                {
                    hairIndex = (hairSize - 1);
                }
                else
                {
                    hairIndex--;
                }
                titles[1].text = "Hair " + (hairIndex+1);
                ApplyModification(AppearenceDetail.HAIR_MODEL, hairIndex);
                break;
            case 3://kit menu
                if (kitIndex == 0)
                {
                    kitIndex = (kitSize - 1);
                }
                else
                {
                    kitIndex--;
                }
                titles[2].text = "Kit " + (kitIndex+1);
                ApplyModification(AppearenceDetail.KIT_MODEL, kitIndex);
                break;

        }
    }
    void ApplyModification(AppearenceDetail detail, int id)
    {
        switch (detail)
        {

            case AppearenceDetail.BODY_MODEL:

                activeBody = bodyModels[id];
                bodyIndex = id;
                foreach (RawImage t in bodyAnchor)
                {
                    t.texture = activeBody;
                }
                break;
            case AppearenceDetail.HAIR_MODEL:
                activeHair = hairModels[id];
                hairIndex = id;
                foreach (RawImage s in hairAnchor)
                {
                    s.texture = activeHair;
                }

                break;
            case AppearenceDetail.KIT_MODEL:
                activeKit = kitModels[id];
                kitIndex = id;
                foreach (RawImage u in kitAnchor)
                {
                    u.texture = activeKit;
                }
                break;
            case AppearenceDetail.FACE_MODEL:
                activeFace = faceModels[id];
                foreach (RawImage v in faceAnchor)
                {
                    v.texture = activeFace;
                }
                faceIndex = id;
                Save();
                break;

        }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("bodyIndex", bodyIndex);
        PlayerPrefs.SetInt("hairIndex", hairIndex);
        PlayerPrefs.SetInt("kitIndex", kitIndex);
        PlayerPrefs.SetInt("faceIndex", faceIndex);
        PlayerPrefs.SetString("saved", "true");

    }
    public void Revert()
    {
        PlayerPrefs.SetInt("bodyIndex", 0);
        PlayerPrefs.SetInt("hairIndex", 0);
        PlayerPrefs.SetInt("kitIndex", 0);
        PlayerPrefs.SetInt("faceIndex", 0);
        ApplyModification(AppearenceDetail.BODY_MODEL, 0);
        ApplyModification(AppearenceDetail.HAIR_MODEL, 0);
        ApplyModification(AppearenceDetail.KIT_MODEL, 0);
        ApplyModification(AppearenceDetail.FACE_MODEL, 0);


    }
}
