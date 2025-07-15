using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speedWalk = 10;
    [SerializeField] private float maxWalkSpeed = 20;

    [SerializeField] private float speedRun = 30;
    [SerializeField] private float maxRunSpeed = 30;

    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float jumpForceAir = 10;
    [SerializeField] private float downForce = 10;

    public bool isRunning { get; set; }
    public Rigidbody rb {  get; set; }
    public float MaxMovementSpeed { get; set; }

    public float movementSpeed;
    public float currentMaxMovementSpeed;


    public void Movement(Vector3 direction)
    {
        movementSpeed = isRunning ? speedRun : speedWalk;
        currentMaxMovementSpeed = isRunning ? maxRunSpeed : maxWalkSpeed;
        MaxMovementSpeed = currentMaxMovementSpeed;

        Vector3 velocityRb = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (velocityRb.magnitude > currentMaxMovementSpeed)
        {
            Vector3 clampVelocity = velocityRb.normalized * currentMaxMovementSpeed;
            rb.velocity = new Vector3(clampVelocity.x, rb.velocity.y, clampVelocity.z);
        }
        else rb.AddForce(direction * movementSpeed, ForceMode.Force);
    }

    public void DownForce()
    {
        rb.velocity = Vector3.Lerp(rb.velocity,Vector3.zero,downForce * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void JumpInAir()
    {
        rb.AddForce(Vector3.up * jumpForceAir, ForceMode.Impulse);
    }
}
