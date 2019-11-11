using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class EvenHandler : MonoBehaviour, ITrackableEventHandler
{
    private GameObject thePanel;
    private List<GameObject> allTheInfos = new List<GameObject>();

    public UnityAction OntrackingFound;
    public UnityAction OntrackingLost;

    private TrackableBehaviour mtrackableBehaviour = null;

    private readonly List<TrackableBehaviour.Status> mTrackingFound = new List<TrackableBehaviour.Status>()
    { TrackableBehaviour.Status.DETECTED,
    TrackableBehaviour.Status.TRACKED,

        //device positionning
    TrackableBehaviour.Status.EXTENDED_TRACKED
    };
    private readonly List<TrackableBehaviour.Status> mTrackingLost = new List<TrackableBehaviour.Status>()
    { 
    TrackableBehaviour.Status.TRACKED,
    TrackableBehaviour.Status.NO_POSE,


    };
    private void Awake()
    {
        thePanel = GameObject.Find("Canvas/Panel");
        Debug.Log(thePanel.name);
        foreach(RectTransform t in thePanel.transform)
        {
            print(t.name);
            t.gameObject.SetActive(false);
            allTheInfos.Add(t.gameObject);//has the text to display    
        }
        Debug.Log("transform:" + transform.name);

        mtrackableBehaviour = GetComponent<TrackableBehaviour>();
        mtrackableBehaviour.RegisterTrackableEventHandler(this);
       // Debug.Log("I'm a " + transform.name);
    }
    private void OnDestroy()
    { 
        mtrackableBehaviour.UnregisterTrackableEventHandler(this);
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {

       //if tracking found
       foreach(TrackableBehaviour.Status trackedStatus in mTrackingFound)
        {
            if(newStatus == trackedStatus)
            {
                if(OntrackingFound != null)
                {
                    OntrackingFound();

                }
                if (!thePanel.activeSelf)
                {
                    DisplayInfo();
                    thePanel.SetActive(true);

                }
                print("tracking found");
                return;
            }
        }
        foreach (TrackableBehaviour.Status trackedStatus in mTrackingLost)
        {
            if (newStatus == trackedStatus)
            {
                if (OntrackingLost != null)
                {
                    OntrackingLost();

                }
                if (thePanel.activeSelf)
                {
                    hideInfo();
                    thePanel.SetActive(false);

                }
                print("tracking lost");
                return;
            }
        }
     
    }
    public void DisplayInfo()
    {
        if (transform.name == "sugar")
        {
            allTheInfos.Find((GameObject obj) => obj.name == "sugarInfo").SetActive(true);
        }
        else if(transform.name == "coffee")
        {
            allTheInfos.Find((GameObject obj) => obj.name == "coffeeInfo").SetActive(true);
        }
  
    }
    public void hideInfo()
    {

        if (transform.name == "sugar")
        {
            allTheInfos.Find((GameObject obj) => obj.name == "sugarInfo").SetActive(false);
        }
        else if (transform.name == "coffee")
        {
            allTheInfos.Find((GameObject obj) => obj.name == "coffeeInfo").SetActive(false);
        }
    }
 
    

    


}
