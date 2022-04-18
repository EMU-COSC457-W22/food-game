using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Movement")]
    public Vector3 movement;
    public float moveSpeed = 0;
    public float walkSpeed = 0;
    public float sprintSpeed = 0;
    public float sprintTimer = 100;
    private bool isEmpty = false;
    //private bool isFull = false;
    private bool isRunning = false;
    private float movementX;
    private float movementY;
    public TextMeshProUGUI countText;
    public SprintMeter sprintMeter;
    [HideInInspector]
    public int count = 0;
    public float currentSprintTimer = 0.0f;
    public GameObject[] foodItems;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        currentSprintTimer = sprintTimer;
        sprintMeter.SetMaxValue(sprintTimer);
        moveSpeed = walkSpeed;
    }
   // void OnMove(InputValue movementValue)
   // {
   //     Vector2 movementVector = movementValue.Get<Vector2>();  

   //     movementX = movementVector.x;
    //    movementY = movementVector.y;
    //}


    void moveCharacter(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        else
        {
            isRunning = false;
        }
    }

   
    void FixedUpdate()
    {
        moveCharacter(movement);

        if(currentSprintTimer <= 0.1f)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
       
       StartCoroutine(sprintPlayer());  
    }

    IEnumerator sprintPlayer()
    {
         //Meter is not empty
        if (!isEmpty)
        {   //Player is holding shift
            if (isRunning)
            {
                if (currentSprintTimer >= 2)
                {
                    moveSpeed = sprintSpeed;
                    currentSprintTimer += -2;
                    sprintMeter.SetMeter(currentSprintTimer);
                }
                else 
                {
                    moveSpeed = walkSpeed; 
                }
            } 
            //Meter is not empty, player is not holding shift
            else
            {
                moveSpeed = walkSpeed;
                if (currentSprintTimer < sprintTimer)
                {
                currentSprintTimer += 1;
                sprintMeter.SetMeter(currentSprintTimer);
                }
            }  
        }
        //Meter is empty
        else 
        {
            moveSpeed = walkSpeed;
            if (currentSprintTimer < sprintTimer)
        {
            currentSprintTimer += 1;
            sprintMeter.SetMeter(currentSprintTimer);
           
        } 
            yield return new WaitUntil(isFull); 
        }        
    }

    private bool isFull()
    {
        if(currentSprintTimer == sprintTimer)
        {
            return true;
        }
        else
            return false;
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
