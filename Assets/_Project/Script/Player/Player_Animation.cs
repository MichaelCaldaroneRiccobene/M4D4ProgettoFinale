using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private string paramNameSpeed = "Speed";
    [SerializeField] private string paramNameDirection = "Direction";
    [SerializeField] private string paramNameIsGround = "IsGround";
    [SerializeField] private string paramNameAttack = "Attack";
    [SerializeField] private string paramNameRecover = "Recover";
    [SerializeField] private string paramNameJump = "Jump";
    [SerializeField] private string paramNameJumpAir = "JumpAirSpeed";

    [SerializeField] private float smoothLerpAnimation = 0.01f;
    [SerializeField] private float maxSpeedJumpAir = 1f;

    private Animator animator;
    private float currentSpeed;
    private float direction;
    private float SpeedJumpAir;
    private bool isOnGround;

    private void Start() => animator = GetComponent<Animator>();

    private void Update()
    {
        if (SpeedJumpAir > 0) SpeedJumpAir -= maxSpeedJumpAir * Time.deltaTime;
        if (SpeedJumpAir < 0) SpeedJumpAir = 0;
    }

    private void FixedUpdate()
    {
        animator.SetFloat(paramNameSpeed, currentSpeed, smoothLerpAnimation, Time.fixedDeltaTime);
        animator.SetFloat(paramNameDirection, direction);

        animator.SetFloat(paramNameJumpAir, SpeedJumpAir, smoothLerpAnimation, Time.fixedDeltaTime);
    }

    public void TakeSpeedAndDirection(float currentSpeed,float direction)
    {
        this.currentSpeed = currentSpeed;
        this.direction = direction; 
    }

    public void OnAttack()
    {
        animator.SetTrigger(paramNameAttack);
    }

    public void OnJump()
    {
        if (!isOnGround) return;
        animator.SetTrigger(paramNameJump);
    }

    public void OnJumpAir() => SpeedJumpAir = maxSpeedJumpAir;

    public void IsOnGround(bool isOnGround)
    {
        animator.SetBool(paramNameIsGround, isOnGround);
        this.isOnGround = isOnGround;;
    }

    public void Recover() => animator.SetTrigger(paramNameRecover);
}
