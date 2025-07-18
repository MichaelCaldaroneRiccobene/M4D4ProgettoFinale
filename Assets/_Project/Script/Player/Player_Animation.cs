using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private string paramNameIsGround = "IsGround";
    [SerializeField] private string paramNameAttack = "Attack";
    [SerializeField] private string paramNameRecover = "Recover";
    [SerializeField] private string paramNameJump = "Jump";
    [SerializeField] private string paramNameJumpAir = "JumpAirSpeed"; 

    [SerializeField] private AudioSource audioGeneralAir;
    [SerializeField] private AudioSource audioGeneralWalk;
    [SerializeField] private AudioSource audioGeneralAttack;

    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip[] jumpAirClips;
    [SerializeField] private AudioClip attackClip;


    [SerializeField] private float jumpAirInterval = 0.2f;
    [SerializeField] private float footstepIntervalWalk = 0.2f;
    [SerializeField] private float footstepIntervalRun = 0.1f;
    [SerializeField] private Ground_Check groundCheck;

    public float SpeedJumpAir;

    private float footstepInterval;
    private float lastTimePlayFoots;
    private float lastTimePlayJumpAir;
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
        if (SpeedJumpAir > 0) SpeedJumpAir -= 1f * Time.deltaTime;
        if (SpeedJumpAir < 0) SpeedJumpAir = 0;

        animator.SetFloat(paramNameJumpAir, SpeedJumpAir);
        TryToPlaySoundFoots();
    }

    public void OnAttack()
    {
        animator.SetTrigger(paramNameAttack);
        audioGeneralAttack.volume = UnityEngine.Random.Range(1, 1.3f);
        audioGeneralAttack.PlayOneShot(attackClip);
    }

    public void OnJump()
    {
        animator.SetTrigger(paramNameJump);
        audioGeneralAir.volume = UnityEngine.Random.Range(1, 1.3f);
        audioGeneralAir.PlayOneShot(jumpClip);
    }

    public void OnJumpAir()
    {
        SpeedJumpAir = 1;
        TryToPlaySoundJumpAir();

    }

    public void IsOnGround(bool isGround)
    {
        animator.SetBool(paramNameIsGround, isGround);
    }

    public void Recover() => animator.SetTrigger(paramNameRecover);

    private void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude;

        if (player_Movement.isRunning) currentSpeed = Mathf.InverseLerp(0, player_Movement.MaxMovementSpeed, currentSpeed);
        else currentSpeed = Mathf.InverseLerp(0, player_Movement.MaxMovementSpeed, currentSpeed) * 0.5f;

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        float direction = Mathf.Clamp(localVelocity.x, -1f, 1f);

        animator.SetFloat("Speed", currentSpeed, 0.1f, Time.fixedDeltaTime);
        animator.SetFloat("Direction", direction, 0.1f, Time.fixedDeltaTime);
    }

    private void TryToPlaySoundFoots()
    {
        bool isMoving = rb.velocity.magnitude > 0.2f;

        if (player_Movement.isRunning) footstepInterval = footstepIntervalRun;
        else footstepInterval = footstepIntervalWalk;

        if (Time.time - lastTimePlayFoots >= footstepInterval && isMoving && groundCheck.IsOnGround()) PlaySoundFoots();
    }

    private void PlaySoundFoots()
    {
        lastTimePlayFoots = Time.time;

        AudioClip footSound = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        audioGeneralWalk.pitch = UnityEngine.Random.Range(1, 1.3f);
        audioGeneralWalk.volume = 0.1f;
        audioGeneralWalk.PlayOneShot(footSound);
    }

    private void TryToPlaySoundJumpAir()
    {
        bool isMoving = SpeedJumpAir > 0.9f;
        if (Time.time - lastTimePlayJumpAir >= jumpAirInterval && isMoving && !groundCheck.IsOnGround()) PlaySoundJumpAir();
    }

    private void PlaySoundJumpAir()
    {
        lastTimePlayJumpAir = Time.time;

        AudioClip jumpAir = jumpAirClips[UnityEngine.Random.Range(0, jumpAirClips.Length)];
        audioGeneralAir.pitch = UnityEngine.Random.Range(1.5f, 2f);
        audioGeneralAir.volume = 0.25f;
        audioGeneralAir.PlayOneShot(jumpAir);
    }
}
