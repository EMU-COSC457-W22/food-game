using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 0;
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();  

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
   
    void FixedUpdate()
    {
       Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        transform.position += movement * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("FoodItem"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
