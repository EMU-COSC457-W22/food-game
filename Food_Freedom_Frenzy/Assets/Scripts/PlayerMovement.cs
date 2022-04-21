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
    public bool isRunning = false;
    private float movementX;
    private float movementY;
    public TextMeshProUGUI countText;
    public SprintMeter sprintMeter;
    [HideInInspector]
    public int count = 0;
    public float currentSprintTimer = 0.0f;
    public GameObject[] foodItems;
    public bool isRecharging = false;
    public float rechargeRate = 0;

    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        foodItems = GameObject.FindGameObjectsWithTag("PickUp_Food");

        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        currentSprintTimer = sprintTimer;
        sprintMeter.SetMaxValue(sprintTimer);
        moveSpeed = walkSpeed;
    }

    void moveCharacter(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        
        /* Makes player face direction it's traveling */
        if (movement != Vector3.zero) {
            transform.forward = movement;
        }
        
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

        if(currentSprintTimer <= 0.01f)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
       
    //    StartCoroutine(sprintPlayer());
       SprintPlayer();
    }

    void SprintPlayer()
    {
        //Meter is not empty
        if (!isEmpty && !isRecharging)
        {   //Player is holding shift
            if (isRunning)
            {
                if (currentSprintTimer >= 0)
                {
                    moveSpeed = sprintSpeed;
                    currentSprintTimer += -0.50f;
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
                    currentSprintTimer += rechargeRate * Time.deltaTime;
                    sprintMeter.SetMeter(currentSprintTimer);
                }
            }  
        }
        //Meter is empty
        else 
        {
            moveSpeed = walkSpeed;
            isRecharging = true;
            if (currentSprintTimer < sprintTimer)
            {
                currentSprintTimer += rechargeRate * Time.deltaTime;
                sprintMeter.SetMeter(currentSprintTimer);
                if (isFull()) {
                    isRecharging = false;
                }  
            } 
             
        }        
    }

    private bool isFull()
    {
        if(currentSprintTimer >= sprintTimer)
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
            if (currentScene == "Level_1")
            {
                SceneManager.LoadScene("Level_2");
            }

            if (currentScene == "Level_2")
            {
                SceneManager.LoadScene("Level_3");
            }

            if (currentScene == "Level_3")
            {
                SceneManager.LoadScene("Winning_Screen");
            }
        }
    }

    public void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / " + foodItems.Length;
    }
}
