using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearanceManager : MonoBehaviour
{
	
	[SerializeField]
	private Material[] eyesColor;
	[SerializeField]
	private Material[] bodyColor;

    private int bodyIndex;
    private int eyesIndex;
	// Start is called before the first frame update
	void Start()
    {
        Material[] matArray = gameObject.GetComponent<SkinnedMeshRenderer>().materials;
        matArray[0] = bodyColor[PlayerPrefs.GetInt("bodyColor")];
        matArray[1] = eyesColor[PlayerPrefs.GetInt("eyesColor")];
        gameObject.GetComponent<SkinnedMeshRenderer>().materials = matArray;
        Debug.Log("EYES" + PlayerPrefs.GetInt("eyesColor"));
        Debug.Log("BODY" + PlayerPrefs.GetInt("bodyColor"));

    }


    // Update is called once per frame

}

