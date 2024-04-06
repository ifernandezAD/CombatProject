using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Panic : StateBase
{
    [SerializeField] RuntimeAnimatorController panicAnimationController;

    public enum PatrolMode
    {
        Loop,
        PingPong,
    }

    [SerializeField] private Transform patrolPointsParent; 
    [SerializeField] float reachDistance = 1.5f;
    [SerializeField] PatrolMode patrolMode = PatrolMode.Loop;

    int currentlySeekingPatrolPointIndex;
    int nextIncrement = 1;

    private void OnEnable()
    {
        Debug.Log("The Civilian is panicking");
        ai.animator.runtimeAnimatorController = panicAnimationController;

        currentlySeekingPatrolPointIndex = Random.Range(0, patrolPointsParent.childCount);
        //Patrol a los 4 puntos cardinales de forma aleatoria ( panicPoints en el gameplay commons)
        //Aumentar la velocidad de los jambos
    }

    private void Update()
    {
        UpdatePatrol();
    }

    public void InitParent(Transform parent)
    {
        patrolPointsParent = parent;        
    }

    private void UpdatePatrol()
    {
        Vector3 seekPosition = patrolPointsParent.GetChild(currentlySeekingPatrolPointIndex).position;
        

        ai.SetDestination(seekPosition);
        if (Vector3.Distance(seekPosition, transform.position) < reachDistance)
        {
            // Choose a random patrol point index
            int randomIndex = Random.Range(0, patrolPointsParent.childCount);
            // Make sure it's not the same as the current one
            while (randomIndex == currentlySeekingPatrolPointIndex)
            {
                randomIndex = Random.Range(0, patrolPointsParent.childCount);
            }
            currentlySeekingPatrolPointIndex = randomIndex;
        }
    }
}
