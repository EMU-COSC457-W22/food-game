using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackState : State
{
    public override void EnterState(HumanManager human)
    {
        Debug.Log("I am in the attack state");

        /* Temporary way of triggering correct game over screen to restart current level */
        if ((human.currentTarget.CompareTag("Player") /* || human.currentTarget.CompareTag("TrailingFoodItem") */)) {
            if (human.currentScene == "Release_2")
            {
                SceneManager.LoadScene("GameOver1");
            }
            else
            {
                SceneManager.LoadScene("GameOver2");
            }
        }  
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
