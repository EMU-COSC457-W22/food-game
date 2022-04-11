using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private int _randomSpot;

    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the patrol state!");
        _randomSpot = Random.Range(0, human.patrolPoints.Length);
        human.maxWaitingTime = Random.Range(2, 5);
        human.currentTarget = human.patrolPoints[_randomSpot];
    }

    public override void UpdateState(HumanManager human)
    {
        if (human.distanceFromPlayer <= human.detectionRadius) {
            human.SwitchState(human.suspicious);
        }
        
        /* Start chasing player or attack player if already in range */
        if ((human.currentTarget.CompareTag("Player") /* || human.currentTarget.CompareTag("TrailingFoodItem") */)) {
            
            float distanceToTarget = Vector3.Distance(human.transform.position, human.currentTarget.position);
            
            if (distanceToTarget > human.attackRadius && distanceToTarget < human.viewRadius) {
                human.SwitchState(human.chase);
            }

            if (distanceToTarget <= human.attackRadius) {
                human.SwitchState(human.attack);
            }
            
        }
        
        if (human.agent.remainingDistance < 0.5f)
        {
            if (human.maxWaitingTime == 0)
            {
                human.maxWaitingTime = Random.Range(2, 5);
            }

            if (human.currentWaitingTime >= human.maxWaitingTime)
            {
                human.maxWaitingTime = 0;
                human.currentWaitingTime = 0;
                _randomSpot = Random.Range(0, human.patrolPoints.Length);
                human.currentTarget = human.patrolPoints[_randomSpot];
                human.agent.SetDestination(human.currentTarget.position);
            }
            else
            {
                human.currentWaitingTime += Time.deltaTime;
            }
        }
    }

    private void SearchNextPosition(HumanManager human)
    {
        if (human.agent.remainingDistance < 0.5f)
        {
            if (human.maxWaitingTime == 0)
            {
                human.maxWaitingTime = Random.Range(2, 5);
            }

            if (human.currentWaitingTime >= human.maxWaitingTime)
            {
                human.maxWaitingTime = 0;
                human.currentWaitingTime = 0;
                _randomSpot = Random.Range(0, human.patrolPoints.Length);
                human.currentTarget = human.patrolPoints[_randomSpot];
                human.agent.SetDestination(human.patrolPoints[_randomSpot].position);
            }
            else
            {
                human.currentWaitingTime += Time.deltaTime;
            }
        }
    }
}
