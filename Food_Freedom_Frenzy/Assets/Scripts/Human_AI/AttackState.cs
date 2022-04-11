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
        if (human.currentTarget.CompareTag("Player")) {
            
        }
        SceneManager.LoadScene("GameOver1");
        
       
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

        Vector3 relativeNormalizedPosition = (human.player.position - human.transform.position);
        float dotProduct = Vector3.Dot(relativeNormalizedPosition, human.transform.forward);

        float angle = Mathf.Acos(dotProduct);

        if (angle < human.viewAngle && human.distanceFromPlayer <= human.attackRadius)
        {
            SceneManager.LoadScene("GameOver1");
        }

        // if (angle < human.viewAngle && human.distanceFromPlayer > human.attackRadius)
        // {
        //     human.currentTarget = human.player;
        //     human.SwitchState(human.chase);
        // }
    }
}
