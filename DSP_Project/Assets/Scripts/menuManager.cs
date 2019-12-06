using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{

    public GameObject firstLoadPanel,panelNormal,quitPanel;
   
    private int choice;
  
    void Start()
    {
        choice = GameManager.theMenu;
        Debug.Log("THE MENU IN MANAGER" + choice);

        switch (choice)
        {
            case 0:
                Debug.Log("CHOICE " + choice);
                firstLoadPanel.SetActive(true);
                break;
            case 1:
                Debug.Log("CHOICE " + choice);
                panelNormal.SetActive(true);
                break;
            
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { quitPanel.SetActive(true); }
    }

}


