using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appearanceManager : MonoBehaviour
{

    public Texture[] Faces;
    public Transform[] FaceAnchor;
    Texture activeFace;
    public static bool go=false;

    private void Update()
    {
        if(go)
        {
            ChangeFace(PlayerPrefs.GetString("nutriGrade"));
            go = false;
        }
    }
    // Start is called before the first frame update
    public void ChangeFace(string score)
    {
       switch(score)
        {
            case "a":
                activeFace = Faces[0];
                foreach (Transform t in FaceAnchor)
                {
                    t.GetComponent<RawImage>().texture = activeFace;
                }
                break;
            case "b":
                activeFace = Faces[1];
                foreach (Transform t in FaceAnchor)
                {
                    t.GetComponent<RawImage>().texture = activeFace;
                }
                break;
            case "c":
                activeFace = Faces[1];
                foreach (Transform t in FaceAnchor)
                {
                    t.GetComponent<RawImage>().texture = activeFace;
                }
                break;
            case "d":
                activeFace = Faces[2];
                foreach (Transform t in FaceAnchor)
                {
                    t.GetComponent<RawImage>().texture = activeFace;
                }
                break;
            case "e":
                activeFace = Faces[2];
                foreach (Transform t in FaceAnchor)
                {
                    t.GetComponent<RawImage>().texture = activeFace;
                }
                break;

        }

    }


    // Update is called once per frame

}

