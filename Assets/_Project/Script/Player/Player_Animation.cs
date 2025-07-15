using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private string paramNameIsGround = "IsGround";
    [SerializeField] private string paramNameAttack = "Attack";
    [SerializeField] private string paramNameJump = "Jump";
    [SerializeField] private string paramNameJumpAir = "JumpAirSpeed";

    public float SpeedJump;

    
    private Player_Movement player_Movement;
    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        player_Movement = GetComponentInParent<Player_Movement>();
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (SpeedJump > 0) SpeedJump -= 1f * Time.deltaTime;
        if (SpeedJump < 0) SpeedJump = 0;

        animator.SetFloat(paramNameJumpAir, SpeedJump);
    }

    public void OnAttack()
    {
        animator.SetTrigger(paramNameAttack);
    }

    public void OnJump()
    {
        animator.SetTrigger(paramNameJump);
    }

    public void OnJumpAir()
    {
        SpeedJump = 1;
    }

    public void IsOnGround(bool isGround)
    {
        animator.SetBool(paramNameIsGround, isGround);
    }

    private void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude;

        if (player_Movement.isRunning) currentSpeed = Mathf.InverseLerp(0, player_Movement.MaxMovementSpeed, currentSpeed);
        else currentSpeed = Mathf.InverseLerp(0, player_Movement.MaxMovementSpeed, currentSpeed) * 0.5f;

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        float direction = Mathf.Clamp(localVelocity.x, -1f, 1f);

        animator.SetFloat("Speed", currentSpeed);
        animator.SetFloat("Direction", direction);
    }
}
