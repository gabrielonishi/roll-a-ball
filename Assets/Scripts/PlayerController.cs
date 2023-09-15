using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    public float timer = 10;
    private int countToWin = 3; 

    private int count;
    private bool timeout = false;
    private float decelerationRate = 5.0f; 
    private float stopThreshold = 0.1f;  
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer >= 0){
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = (int) timer % 60;
            timerText.text = "Time to Next Ball: " + minutes.ToString() + ":" + seconds.ToString("00");
            countText.text = "Count: " + count.ToString();
        }
        else {
            if(count < countToWin){
                loseTextObject.SetActive(true);
            }
            timeout = true;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        if(timeout){
            Vector3 oppositeForce = -rb.velocity.normalized * decelerationRate;
            rb.AddForce(oppositeForce);
            if (rb.velocity.magnitude < stopThreshold)
                {
                    rb.velocity = Vector3.zero;
                }
        } else {
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") & !timeout) 
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= countToWin & !timeout)
        {
            winTextObject.SetActive(true);
        }
    }

}