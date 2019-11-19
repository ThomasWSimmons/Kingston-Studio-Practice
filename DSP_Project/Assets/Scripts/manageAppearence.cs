using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class manageAppearence : MonoBehaviour
{
	enum AppearenceDetail
	{
		SKIN_COLOR,
		EYES_COLOR
	}

	[SerializeField]
	private SkinnedMeshRenderer colorAnchor;
	[SerializeField]
	private Material[] eyesColor;
	[SerializeField]
	private Material[] bodyColor;


    Dictionary<AppearenceDetail, int> chosenAppearence;
    private appearanceManager theManager;
	int indexBody=0;
	int indexE=0;


    public TMP_Dropdown myDropdownEyes, myDropDownBody;
    List<string> bodyColors = new List<string>() { "Standard", "green" };
    List<string> eyesColors = new List<string>() { "Standard", "green" };

    private void Start()
    {
       
        if (!PlayerPrefs.HasKey("bodyColor")&& !PlayerPrefs.HasKey("bodyColor"))
        {
            //standard color
            PlayerPrefs.SetInt("bodyColor", indexBody);
            PlayerPrefs.SetInt("eyesColor", indexE);
        }
        else if(PlayerPrefs.HasKey("bodyColor") && PlayerPrefs.HasKey("bodyColor"))
        {

            Material[] matArray = colorAnchor.materials;
            matArray[0] = bodyColor[PlayerPrefs.GetInt("bodyColor")];
            matArray[1] = eyesColor[PlayerPrefs.GetInt("eyesColor")];
            colorAnchor.materials = matArray;
        }
 
        myDropdownEyes.AddOptions(eyesColors);
        myDropDownBody.AddOptions(bodyColors);

    }
    public void BodyColorAppearence()
	{
		indexBody = myDropDownBody.value;
		Material[] matBody = colorAnchor.materials;
		matBody[0] = bodyColor[indexBody];
		colorAnchor.materials = matBody;
     
    }
	public void EyesColorAppearence()
	{
		indexE = myDropdownEyes.value;
		Material[] matEyes = colorAnchor.materials;
		matEyes[1] = eyesColor[indexE];
		colorAnchor.materials = matEyes;
        
    }
	public void SaveAppearence()
	{
		chosenAppearence = new Dictionary<AppearenceDetail, int>();
		chosenAppearence.Add(AppearenceDetail.SKIN_COLOR, indexBody);
		chosenAppearence.Add(AppearenceDetail.EYES_COLOR, indexE);
        PlayerPrefs.SetInt("bodyColor", indexBody);
        Debug.Log("body color set to"+indexBody);

        PlayerPrefs.SetInt("eyesColor", indexE);
        Debug.Log("eyes color set to" + indexE);

    }
    public void Revert()
    {
        PlayerPrefs.SetInt("bodyColor", 0);
        PlayerPrefs.SetInt("eyesColor", 0);

        Material[] matArray = colorAnchor.materials;
        matArray[0] = bodyColor[PlayerPrefs.GetInt("bodyColor")];
        matArray[1] = eyesColor[PlayerPrefs.GetInt("eyesColor")];
        colorAnchor.materials = matArray;
    }
}
