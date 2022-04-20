using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable_Goal_Zone : MonoBehaviour
{
    public GameObject GoalPrefab;
    
    void Start()
    {
        GoalPrefab.SetActive(false);
    }

    void EnableGoal()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement.count == playerMovement.foodItems.Length)
        {
            GoalPrefab.SetActive(true);
        }

        if (playerMovement.count <= playerMovement.foodItems.Length / 2)
        {
            GoalPrefab.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        EnableGoal();
    }
}
