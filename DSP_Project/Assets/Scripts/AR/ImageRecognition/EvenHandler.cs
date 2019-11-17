using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class EvenHandler : MonoBehaviour, ITrackableEventHandler
{
    //ONE EVENT HANDLER PER TARGET GO TO TRACKING MANAGER


    
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
                if (OntrackingFound != null)
                {
                    OntrackingFound();
                }
                print("tracking found");
                trackingManager.trackingFound = true;
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
               // EventManager.DeActivate(transform.name);
               
                return;
            }
        }//tracking,lost
     
    }
  
 
    

    


}
