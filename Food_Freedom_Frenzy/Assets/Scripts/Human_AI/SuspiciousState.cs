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

        if (!human.currentTarget.CompareTag("Player")) {
            if (human.currentSuspicionTime >= human.maxSuspicionTime) {
                human.currentSuspicionTime = 0;
                human.SwitchState(human.patrol);
            } else {
                human.currentSuspicionTime += Time.deltaTime;
            }
        } else {
            human.currentSuspicionTime = 0;
            human.SwitchState(human.chase);
        }
        
    }

    IEnumerator LookAround(HumanManager human)
    {
        human.transform.Rotate(Vector3.up, human.turnSpeed * Time.deltaTime);
        if (human.currentTarget.CompareTag("Player")) {
            human.SwitchState(human.chase);
            yield return null;
        } else {
            yield return new WaitForSeconds(5);
            human.SwitchState(human.patrol);
        }
        
    }
}
