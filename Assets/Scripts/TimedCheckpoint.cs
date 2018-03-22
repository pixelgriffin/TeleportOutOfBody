using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedCheckpoint : MonoBehaviour {

    public enum CHECKPOINT_TYPE
    {
        UPSTAIRS,
        DOWNSTAIRS,
        FIRST_OBSTACLE,
        SECOND_OBSTACLE
    }

    public TimedCheckpoint reliesOn;

    public CHECKPOINT_TYPE checkpointType;

    public UnityEvent OnHitCheckpoint;

    public float finishTime = 0f;

    public bool hit = false;

	void Update () {
        if(!hit)
            finishTime += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CompleteCheckpoint();
        }
    }

    public float GetFinishTime()
    {
        return finishTime;
    }

    public bool CompleteCheckpoint()
    {
        if (reliesOn == null || reliesOn.hit)
        {
            if (!hit)
            {
                switch (checkpointType)
                {
                    case CHECKPOINT_TYPE.DOWNSTAIRS:
                        Debug.Log("Set downsatirs time to " + GetFinishTime());
                        Statistics.Instance.data.downstairsCheckpointTime = GetFinishTime();
                        break;

                    case CHECKPOINT_TYPE.UPSTAIRS:
                        Statistics.Instance.data.upstairsCheckpointTime = GetFinishTime();
                        break;

                    case CHECKPOINT_TYPE.FIRST_OBSTACLE:
                        Statistics.Instance.data.firstObstacleCheckpointTime = GetFinishTime();
                        break;

                    case CHECKPOINT_TYPE.SECOND_OBSTACLE:
                        Statistics.Instance.data.secondObstacleCheckpointTime = GetFinishTime();
                        break;
                }

                OnHitCheckpoint.Invoke();
                hit = true;

                return true;
            }
        }

        return false;
    }
}
