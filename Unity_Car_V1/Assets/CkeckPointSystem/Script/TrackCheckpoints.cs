using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{

    private List<CheckpointSingle> checkpointSingleList;
    private int nextCheckpointSingleIndex;
    

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkpointSingleList = new List<CheckpointSingle>();

        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            // Debug.Log(checkpointSingleTransform);
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);

            checkpointSingleList.Add(checkpointSingle);

        }
        nextCheckpointSingleIndex = 0;
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        // Debug.Log(checkpointSingle.transform.name);
        // Debug.Log(checkpointSingleList.IndexOf(checkpointSingle));

        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            // Correct
            Debug.Log("correct");
            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
        }
        else
        {
            Debug.Log("wrong");
        }
    }
}
