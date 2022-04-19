using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 0;
    private float movementX;
    private float movementY;
    public TextMeshProUGUI countText;
    private int count;

    public GameObject[] foodItems;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
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

        /* Makes player face direction it's traveling */
        if (movement != Vector3.zero) {
            transform.forward = movement;
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
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / " + foodItems.Length;
        if (count == foodItems.Length)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
