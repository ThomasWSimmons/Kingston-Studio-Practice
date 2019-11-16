using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class EvenHandler : MonoBehaviour, ITrackableEventHandler
{
    //private int firstRound=0;


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
               /* if (!EventManager.panelActive)
                {
                    print("activating");
                    EventManager.Activate(transform.name);
                }
                else
                {
                    print("DOING NOTHING");
                }*/
                print("tracking found");
                EventManager.Activate(transform.name);
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
                print("tracking lost");
                EventManager.DeActivate(transform.name);
                /*
                if (firstRound > 0)
                {
                    EventManager.DeActivate(transform.name);
                }
                else
                {
                    print("not yet");
                    firstRound++;
                }*/
                return;
            }
        }
     
    }
  
 
    

    


}
