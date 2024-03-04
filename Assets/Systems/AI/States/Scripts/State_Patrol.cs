using UnityEngine;

public class State_Patrol : StateBase
{
    public enum PatrolMode
    {
        Loop,
        PingPong,
    }

    [SerializeField] Transform patrolPointsParent;
    [SerializeField] float reachDistance = 1.5f;
    [SerializeField] PatrolMode patrolMode = PatrolMode.Loop;

    int currentlySeekingPatrolPointIndex;
    int nextIncrement = 1;

    void Update()
    {
        Vector3 seekPosition = patrolPointsParent.GetChild(currentlySeekingPatrolPointIndex).position;
        ai.SetDestination(seekPosition);     
        if (Vector3.Distance(seekPosition, transform.position) < reachDistance)
        {
            currentlySeekingPatrolPointIndex += nextIncrement;
            switch (patrolMode)
            {
                case PatrolMode.Loop:
                    if (currentlySeekingPatrolPointIndex >= patrolPointsParent.childCount)
                    {
                        currentlySeekingPatrolPointIndex = 0;
                    }
                    break;
                case PatrolMode.PingPong:
                    if ((nextIncrement == 1) && (currentlySeekingPatrolPointIndex >= patrolPointsParent.childCount))
                    {
                        nextIncrement *= -1;
                        currentlySeekingPatrolPointIndex = patrolPointsParent.childCount - 2;
                    }
                    else if ((nextIncrement == -1) && (currentlySeekingPatrolPointIndex < 0))
                    {
                        nextIncrement *= -1;
                        currentlySeekingPatrolPointIndex = 1;
                    }
                    break;
            }
        }
    }
}
