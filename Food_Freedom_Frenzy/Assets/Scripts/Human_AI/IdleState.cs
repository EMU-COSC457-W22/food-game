using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    PlayerMovement player;

    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the Idle State");
        player = human.player.GetComponent<PlayerMovement>();
        human.maxWaitingTime = Random.Range(2, 5);
        human.agent.SetDestination(human.transform.position);
    }

    public override void UpdateState(HumanManager human)
    {
        if (human.distanceFromPlayer <= human.detectionRadius && (!player.isSafe || player.isRunning)) {
            human.SwitchState(human.suspicious);
        }

        /* Start chasing player or attack player if already in range */
        if ((human.currentTarget.CompareTag("Player") || human.currentTarget.CompareTag("PickUp_FoodTrail")) && !player.isSafe)
        {

            float distanceToTarget = Vector3.Distance(human.transform.position, human.currentTarget.position);

            // if (distanceToTarget > human.attackRadius && distanceToTarget < human.viewRadius && !player.isSafe)
            // {
                human.SwitchState(human.chase);
            // }

            if (distanceToTarget <= human.attackRadius && !player.isSafe)
            {
                human.SwitchState(human.attack);
            }
        }

        /* Main Idle logic */
        if (human.maxWaitingTime == 0)
        {
            human.maxWaitingTime = Random.Range(2, 5);
        }

        if (human.currentWaitingTime >= human.maxWaitingTime)
        {
            human.maxWaitingTime = 0;
            human.currentWaitingTime = 0;
            human.SwitchState(human.patrol);
        }
        else
        {
            human.currentWaitingTime += Time.deltaTime;
        }
    }
}
