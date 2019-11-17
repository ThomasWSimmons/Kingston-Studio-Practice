using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingManager : MonoBehaviour
{
    private float timer;
    public GameObject toDeactivate;
    public GameObject toActivate;
    public static bool trackingFound;
    public static bool trackingAllowed;
    // Start is called before the first frame update
    void OnEnable()
    {
        
        trackingFound = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (!trackingFound)
            {
                if (timer > 8)
                {
                    Debug.Log("too long");
                    toActivate.SetActive(true);
                    toDeactivate.SetActive(false);
                    gameObject.SetActive(false);
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else
            {

                timer = 0;
                gameObject.SetActive(false);

        }
    }

}
