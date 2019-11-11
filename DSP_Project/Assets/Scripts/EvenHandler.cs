using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class EvenHandler : MonoBehaviour, ITrackableEventHandler
{
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
                print("tracking lost");
                return;
            }
        }
    }

   

}
