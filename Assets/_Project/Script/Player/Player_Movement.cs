using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float runSpeed = 30;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float downForce = 10;

    public bool isRunning { get; set; }
    public Rigidbody rb {  get; set; }

    private float movementSpeed;
    private float maxMovementSpeed;


    public void Movement(Vector3 direction)
    {
        movementSpeed = isRunning ? runSpeed : speed;
        maxMovementSpeed = isRunning ? runSpeed : maxSpeed;

        if (rb.velocity.magnitude > maxMovementSpeed) rb.velocity = rb.velocity.normalized * maxMovementSpeed;
        else rb.AddForce(direction * movementSpeed, ForceMode.Force);

        

        Debug.Log(rb.velocity.magnitude);
    }

    public void DownForce()
    {
        rb.velocity = Vector3.Lerp(rb.velocity,Vector3.zero,downForce * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
