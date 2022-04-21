using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousState : State
{
    PlayerMovement player;
    Transform currentPosition;
    
    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the suspicious state");
        player = human.player.GetComponent<PlayerMovement>();
        currentPosition = human.transform;
        human.agent.SetDestination(currentPosition.position);
    }

    public override void UpdateState(HumanManager human)
    {
        human.transform.Rotate(Vector3.up, human.turnSpeed * Time.deltaTime);

        Vector3 relativePosition = (human.player.position - human.transform.position);
        float dotProduct = Vector3.Dot(relativePosition, human.transform.forward);

        float angle = Mathf.Acos(dotProduct);

        /* If the player is detected, switch to the attack state */
        if (human.currentTarget == human.player && !player.isSafe && angle < human.viewAngle && human.distanceFromPlayer <= human.attackRadius)
        {
            human.currentSuspicionTime = 0;
            human.SwitchState(human.attack);
        }

        /* If the player is within the FOV, but outside of attack radius, switch to the chase state */
        if (angle < human.viewAngle && human.distanceFromPlayer > human.attackRadius && !player.isSafe && human.currentTarget == human.player)
        {
            human.currentSuspicionTime = 0;
            human.currentTarget = human.player;
            human.SwitchState(human.chase);
        }

        /* Duration of suspicious state if uninterrupted */
        if (human.currentSuspicionTime >= human.maxSuspicionTime)
        {
            human.currentSuspicionTime = 0;
            human.SwitchState(human.patrol);
        }
        else
        {
            human.currentSuspicionTime += Time.deltaTime;
        }
    }
}
