using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 8.0f;
    private float terminalVelocity = 5.0f;
    private Rigidbody rb;
    private Vector3 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector2 randomPointInCircle = UnityEngine.Random.insideUnitCircle;
        movement = new Vector3(randomPointInCircle.x, 0, randomPointInCircle.y);
        Debug.Log(movement);
    }

    private void FixedUpdate()
    {

        if (rb.velocity.magnitude < 3.0f)
            {
                float forceMagnitude = (terminalVelocity - rb.velocity.magnitude) * speed;
                rb.AddForce(movement*forceMagnitude);
            }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Atingiu!");
            Vector2 randomPointInCircle = UnityEngine.Random.insideUnitCircle;
            movement = new Vector3(randomPointInCircle.x, 0, randomPointInCircle.y);
            Debug.Log(movement);
        }
    }

    void Update()
    {
        
    }
}
