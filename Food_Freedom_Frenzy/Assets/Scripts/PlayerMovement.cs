using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 0;
    public float walkSpeed = 0;
    public float sprintSpeed = 0;
    public float currentSprintTimer = 0.0f;
    public float sprintTimer = 100;
    private float movementX;
    private float movementY;
    public TextMeshProUGUI countText;
    public SprintMeter sprintMeter;
    [HideInInspector]
    public int count = 0;

    public GameObject[] foodItems;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        currentSprintTimer = sprintTimer;
        sprintMeter.SetMaxValue(sprintTimer);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();  

        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }

        else
        {
            moveSpeed = walkSpeed;
        }
    }
   
    void FixedUpdate()
    {
       Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        transform.position += movement * moveSpeed;

        if (moveSpeed == sprintSpeed)
         {
            if (currentSprintTimer >= 2)
             {
                currentSprintTimer += -2;
                sprintMeter.SetMeter(currentSprintTimer);
             }
            else 
            {
                moveSpeed = walkSpeed; 
            }
        }
        //if player is not Sprinting
        else if (currentSprintTimer < sprintTimer)
        {
            currentSprintTimer += 1;
            sprintMeter.SetMeter(currentSprintTimer);
        }          
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp_Food"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

         if(other.gameObject.CompareTag("GoalZone"))
        {
            SceneManager.LoadScene("NextLevel");
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / " + foodItems.Length;
    }
}
