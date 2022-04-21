using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the patrol state!");
        human.randomSpot = Random.Range(0, human.patrolPoints.Length);
        human.currentTarget = human.patrolPoints[human.randomSpot];
        human.agent.SetDestination(human.currentTarget.position);
    }

    public override void UpdateState(HumanManager human)
    {
        PlayerMovement playerMovement = human.player.GetComponent<PlayerMovement>();

        /* Switch to suspicious state once player gets in the radius */
        if (human.distanceFromPlayer <= human.detectionRadius && playerMovement.isRunning) {
            human.SwitchState(human.suspicious);
        }
        
        /* Start chasing player or attack player if already in range */
        if ((human.currentTarget.CompareTag("Player")  || human.currentTarget.CompareTag("PickUp_FoodTrail"))) {
            
            float distanceToTarget = Vector3.Distance(human.transform.position, human.currentTarget.position);
            
            if (distanceToTarget > human.attackRadius && distanceToTarget < human.viewRadius) {
                human.SwitchState(human.chase);
            }

            if (distanceToTarget <= human.attackRadius) {
                human.SwitchState(human.attack);
            }
            
        }
        
        /* Human will idle for some time once they've reached there destination */
        if (human.agent.remainingDistance < 0.5f)
        {
            human.SwitchState(human.idle);
        }
    }
}
