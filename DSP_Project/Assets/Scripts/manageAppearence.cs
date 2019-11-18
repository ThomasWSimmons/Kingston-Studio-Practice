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
	int indexBody;
	int indexE;


    public TMP_Dropdown myDropdownEyes, myDropDownBody;
    List<string> bodyColors = new List<string>() { "Standard", "green" };
    List<string> eyesColors = new List<string>() { "Standard", "green" };

    private void Start()
    {

        myDropdownEyes.AddOptions(eyesColors);
        myDropDownBody.AddOptions(bodyColors);

    }
    public void BodyColorAppearence()
	{
		indexBody = myDropDownBody.value;
		Material[] matArray = colorAnchor.materials;
		matArray[0] = bodyColor[indexBody];
		colorAnchor.materials = matArray;
	}
	public void EyesColorAppearence()
	{
		indexE = myDropdownEyes.value;
		Material[] matArray = colorAnchor.materials;
		matArray[1] = eyesColor[indexE];
		colorAnchor.materials = matArray;
	}
	public void SaveAppearence()
	{
		chosenAppearence = new Dictionary<AppearenceDetail, int>();
		chosenAppearence.Add(AppearenceDetail.SKIN_COLOR, indexBody);
		chosenAppearence.Add(AppearenceDetail.EYES_COLOR, indexE);
	}
}
