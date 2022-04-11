using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodItem : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    Vector3 initialPos;


    private void Start()
    {
        initialPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {   
        transform.position = new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude + initialPos.y, 0);
    }

   
}
