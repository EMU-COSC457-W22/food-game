using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    float _speedModifier = 1.25f;   // increase or decrease speed by 50%
    float _currentSpeed;

    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the chase state!");
        
        _currentSpeed = human.agent.speed;
        human.agent.speed = _currentSpeed * _speedModifier;
    }

    public override void UpdateState(HumanManager human)
    {
        human.transform.LookAt(human.currentTarget);
        human.agent.SetDestination(human.currentTarget.position);
        float distanceToTarget = Vector3.Distance(human.transform.position, human.currentTarget.position);
        _currentSpeed = human.agent.speed;

        /* Set current chase-outside-range time once player is back in range */
        if (distanceToTarget < human.viewRadius) {
            human.currentChaseOutsideRangeTime = 0;
        }

        /*  Switch to patrol state once player is out of view range, 
            but still chase for a few seconds before patroling again */
        if (distanceToTarget > human.viewRadius) {
            if (human.currentChaseOutsideRangeTime >= human.maxChaseOutsideRangeTime) {
                human.currentChaseOutsideRangeTime = 0;
                human.agent.speed = _currentSpeed / _speedModifier;
                human.SwitchState(human.patrol);
            } else {
                human.currentChaseOutsideRangeTime += Time.deltaTime;
            }
        }

        /* Switch to attack state once in attack range. Also switch back to normal speed */
        if (distanceToTarget <= human.attackRadius && distanceToTarget < human.viewRadius) {
            human.agent.speed = _currentSpeed / _speedModifier;
            human.SwitchState(human.attack);
        }
    }
}
