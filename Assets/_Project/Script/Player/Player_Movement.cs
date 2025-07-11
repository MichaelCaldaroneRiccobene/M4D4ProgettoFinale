using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float jumpForce = 10;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction)
    {
        if(rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
        else rb.AddForce(direction * speed, ForceMode.Force);
    }

    public void Jump() => rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
}
