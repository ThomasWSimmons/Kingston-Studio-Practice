using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class avatarNameManager : MonoBehaviour
{
	public TextMeshProUGUI theAvatarName;
	// Start is called before the first frame update
	void Start()
    {
		theAvatarName.text = PlayerPrefs.GetString("avatarName");
	}

  
}
