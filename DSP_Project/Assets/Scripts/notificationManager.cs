using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notificationManager : MonoBehaviour
{
	public GameObject notif;
   public void updateNotif()
	{
        if(notif.activeInHierarchy)
		{
			notif.SetActive(false);
		}
	}
}
