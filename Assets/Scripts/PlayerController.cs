using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using System.Threading;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
    public GameObject loseTextObject;


    public AudioSource screamSound;
    public AudioSource coinSound;
    public AudioSource loseSound;
    public AudioSource winSound;
    public AudioSource backgroundSound;

    private float timer = 240;
    private int countToWin = 50;

    private int count;
    private bool timeout = false;
    private float decelerationRate = 5.0f;
    private float stopThreshold = 0.1f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool gameOver = false;
    private bool playedSong = false;

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
        if (timer >= 0 & !gameOver)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = (int)timer % 60;
            timerText.text = "Tempo até Amanhecer: " + minutes.ToString() + ":" + seconds.ToString("00");
        }
        else
        {
            timeout = true;
            if (count < countToWin)
            {
                
                loseTextObject.SetActive(true);
                if (!playedSong)
                {
                    loseSound.Play();
                    backgroundSound.Stop();
                    playedSong = true;
                }
                loseScreen();
            }
            else
            {
                
                winTextObject.SetActive(true);
                if (!playedSong)
                {
                    winSound.Play();
                    backgroundSound.Stop();
                    playedSong = true;
                }
                winScreen();
            }
        }

        if (transform.position.y < -2)
        {
            screamSound.Play();
            if (!playedSong)
            {
                loseSound.Play();
                backgroundSound.Stop();
                playedSong = true;
            }
            loseScreen();
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

        if (timeout || gameOver)
        {
            Vector3 oppositeForce = -rb.velocity.normalized * decelerationRate;
            rb.AddForce(oppositeForce);

            if (rb.velocity.magnitude < stopThreshold)
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && !timeout)
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
            coinSound.Play();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameOver = true;
            count = 0;
            screamSound.Play();
            backgroundSound.Stop();
        }
    }

    void setCountText()
    {
        countText.text = "$: " + count.ToString();
    }

    public void loseScreen()
    {
        if (!loseSound.isPlaying)
        {
            SceneManager.LoadScene("Lose Screen");
        }
    }

    public void winScreen()
    {
        if (!winSound.isPlaying)
        {
            SceneManager.LoadScene("Win Screen");
        }
    }
}
