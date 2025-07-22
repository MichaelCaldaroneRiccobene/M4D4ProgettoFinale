using UnityEngine;
using UnityEngine.Events;

public class Player_Movement : MonoBehaviour
{
    [Header("Setting For Walk/Run")]
    [SerializeField] private float speedWalk = 10;
    [SerializeField] private float maxWalkSpeed = 20;

    [SerializeField] private float speedRun = 30;
    [SerializeField] private float maxRunSpeed = 30;

    [SerializeField] private float downForce = 10;
    [SerializeField] private float maxSpeedForWalkAnimation = 0.5f;

    [Header("Setting Rotation Player")]
    [SerializeField] private float speedRotatingNormal = 5;
    [SerializeField] private float speedRotatingFocus = 10;

    [Header("Setting For Jump/JumpAir")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float jumpForceAir = 10;

    public UnityEvent<float, float> onAnimationWalkAndDirection;

    private bool isRunning;
    private float movementSpeed;
    private float currentMaxMovementSpeed;

    private Rigidbody rb;

    private Vector3 direction;
    private float horizontal;
    private float vertical;

    private bool isOnGround;
    private bool isFocusMode;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void FixedUpdate() => LogicMovement();

    private void LogicMovement()
    {
        if (Camera.main == null) return;
        if(rb.isKinematic) return;

        Vector3 forward = Camera.main.transform.forward; Vector3 right = Camera.main.transform.right;
        forward.y = 0; right.y = 0;

        direction = forward * vertical + right * horizontal;

        if (direction.sqrMagnitude >= 0.01f)
        {
            direction.Normalize();
            Movement(direction);

            if (isOnGround) DownForce();
        }

        if (isFocusMode)
        {
            Vector3 lookDirection = forward;
            transform.forward = Vector3.Slerp(transform.forward, lookDirection, speedRotatingFocus * Time.fixedDeltaTime);
        }
        else 
        {
            if (direction.sqrMagnitude > 0.1f) transform.forward = Vector3.Slerp(transform.forward, direction, speedRotatingNormal * Time.fixedDeltaTime);
        }

        SettingForWalkAnimation();
    }

    public void Movement(Vector3 direction)
    {
        movementSpeed = isRunning ? speedRun : speedWalk;
        currentMaxMovementSpeed = isRunning ? maxRunSpeed : maxWalkSpeed;

        Vector3 velocityRb = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (velocityRb.magnitude > currentMaxMovementSpeed)
        {
            Vector3 clampVelocity = velocityRb.normalized * currentMaxMovementSpeed;
            rb.velocity = new Vector3(clampVelocity.x, rb.velocity.y, clampVelocity.z);
        }
        else rb.AddForce(direction * movementSpeed, ForceMode.Force);
    }

    private void SettingForWalkAnimation()
    {
        float currentSpeed = rb.velocity.magnitude;

        if (isRunning) currentSpeed = Mathf.InverseLerp(0, currentMaxMovementSpeed, currentSpeed);
        else currentSpeed = Mathf.InverseLerp(0, currentMaxMovementSpeed, currentSpeed) * maxSpeedForWalkAnimation;

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

        float direction = Mathf.Clamp(localVelocity.x, -1f, 1f);
        onAnimationWalkAndDirection?.Invoke(currentSpeed, direction);
    }

    public void TakeDirection(float horizontal,float vertical)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
    }

    public void IsOnFocus(bool isFocusMode) => this.isFocusMode = isFocusMode;

    public void IsOnGround(bool isOnGround) => this.isOnGround = isOnGround;

    public void IsRunnig(bool isRunning) => this.isRunning = isRunning;

    public void DownForce() => rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, downForce * Time.fixedDeltaTime);

    public void Jump() { if (isOnGround) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); }

    public void JumpInAir() { if (!isOnGround) rb.AddForce(Vector3.up * jumpForceAir, ForceMode.Impulse); }
}
