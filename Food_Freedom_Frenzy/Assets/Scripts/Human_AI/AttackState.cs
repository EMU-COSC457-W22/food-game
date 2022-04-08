using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the attack state");
    }

    public override void UpdateState(HumanManager human)
    {
        float distanceToTarget = Vector3.Distance(human.transform.position, human.currentTarget.position);

        if (distanceToTarget > human.attackRadius && distanceToTarget < human.viewRadius) {
            human.SwitchState(human.chase);
        }

        if (distanceToTarget > human.attackRadius && distanceToTarget > human.viewRadius) {
            human.SwitchState(human.patrol);
        }
    }
}
