using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Scene1 : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;


    void OpenDoor()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement.count == playerMovement.foodItems.Length - 1)
        {
            myDoor.Play("DoorOpen", 0, 0.0f);
        }
    } 

    void FixedUpdate()
    {
        OpenDoor();
    }  
}
