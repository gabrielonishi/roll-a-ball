using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    private float timer = 5;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private int count;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
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
            timerText.text = "Timer: " + minutes.ToString() + ":" + seconds.ToString("00");
            countText.text = "Count: " + count.ToString();
        }
        else{
            loseTextObject.SetActive(true);
        }
    }

    private void OnMove(InputValue movementValue)
    {
        if(timer >= 0){
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 2)
        {
            winTextObject.SetActive(true);
        }
    }
}