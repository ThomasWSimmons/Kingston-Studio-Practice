using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class TargetManager : MonoBehaviour
{
    public string mDatabaseName = "";
    private List<TrackableBehaviour> mAllTarget = new List<TrackableBehaviour>();
    // Start is called before the first frame update
    private void Awake()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }
    private void OnDestroy()
    {
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);

    }
    // Update is called once per frame
    private void OnVuforiaStarted()
    {
        //load DB
        LoadDatabase(mDatabaseName);
        //get targets
        mAllTarget = GetTargets();
        //setup targets
        SetupTargets(mAllTarget);
    }
    private void LoadDatabase(string setName)
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        objectTracker.Stop();

        if(DataSet.Exists(setName))
        {
            DataSet dataSet = objectTracker.CreateDataSet();
            dataSet.Load(setName);
            objectTracker.ActivateDataSet(dataSet);
        }
        objectTracker.Start();

    }

    private List<TrackableBehaviour> GetTargets()
    {
        List<TrackableBehaviour> allTrackable = new List<TrackableBehaviour>();

        allTrackable = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList();


        return allTrackable;
    }

    private void SetupTargets(List<TrackableBehaviour> allTargets)
    {
        foreach(TrackableBehaviour target in allTargets)
        {
            //parent new target to target manager
            target.gameObject.transform.parent = transform;
            //rename
            target.gameObject.name = target.TrackableName;
            //add functionnality
            //done

            Debug.Log(target.TrackableName + " " + "created");
        }
    }
}
