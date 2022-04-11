using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousState : State
{
    Transform currentPosition;
    
    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the suspicious state");
        currentPosition = human.transform;
        human.agent.SetDestination(currentPosition.position);
    }

    public override void UpdateState(HumanManager human)
    {
        human.transform.Rotate(Vector3.up, human.turnSpeed * Time.deltaTime);
        // human.HandleDetection();

        Vector3 relativeNormalizedPosition = (human.player.position - human.transform.position);
        float dotProduct = Vector3.Dot(relativeNormalizedPosition, human.transform.forward);

        float angle = Mathf.Acos(dotProduct);

        if (angle < human.viewAngle && human.distanceFromPlayer <= human.attackRadius)
        {
            human.currentSuspicionTime = 0;
            human.currentTarget = human.player;
            human.SwitchState(human.attack);
        }

        if (angle < human.viewAngle && human.distanceFromPlayer > human.attackRadius)
        {
            human.currentSuspicionTime = 0;
            human.currentTarget = human.player;
            human.SwitchState(human.chase);
        }
        
        if (human.currentTarget.CompareTag("Player")) {
            human.currentSuspicionTime = 0;
            human.SwitchState(human.chase);
            
        } else {
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
}
