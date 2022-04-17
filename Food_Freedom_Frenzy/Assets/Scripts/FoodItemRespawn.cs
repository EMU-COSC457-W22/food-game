using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodItemRespawn : MonoBehaviour
{
    private Vector3 Min;
    private Vector3 Max;
    private float _xAxis;
    private float _zAxis;
    private Vector3 _randomPosition;
    public GameObject TrailingPrefab;
    public GameObject FoodPickup;

    private void Start()
    {
        SetRanges();
    }

    private void FixedUpdate()
    {
        _xAxis = UnityEngine.Random.Range(Min.x, Max.x);
        _zAxis = UnityEngine.Random.Range(Min.z, Max.z);

        _randomPosition = new Vector3(_xAxis, 1, _zAxis);
    }

    private void SetRanges()
    {
        Min = new Vector3(-50, 1, -10);
        Max = new Vector3(50, 1, 40);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Human"))
        {
            TrailingPrefab.SetActive(false);
            Instantiate(FoodPickup, _randomPosition, Quaternion.identity);
            
            GameObject player = GameObject.Find("Player");
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.count -= 1;
        }
    } 
}
