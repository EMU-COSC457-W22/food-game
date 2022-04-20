using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodItemRespawn : MonoBehaviour
{
    private int randomNum;
    public GameObject TrailingPrefab;
    public GameObject FoodPickup;
    private GameObject[] spawnPoints;

    private void Start()
    {
        if (spawnPoints == null)
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        }
    }

    private void FixedUpdate()
    {
       randomNum = Random.Range(0, spawnPoints.Length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Human"))
        {
            TrailingPrefab.SetActive(false);
            Instantiate(FoodPickup, spawnPoints[randomNum].transform.position, Quaternion.identity);
            

            GameObject player = GameObject.Find("Player");
            FoodTrail trail = player.GetComponent<FoodTrail>();
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.count -= 1;
            playerMovement.SetCountText();
            trail.TrailList.Remove(this.gameObject);
            
        }
    } 
}
