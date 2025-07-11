using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{

    [SerializeField] Transform playerCamera;
    public UnityEvent<float, float> onDirectionChange;
    public Vector3 Direction {  get; private set; }
    private float x;
    private float z;
    private bool isFocusMode;

    private Player_Movement player_Movement;
    private Ground_Check ground_Check;

    private void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        ground_Check = GetComponentInChildren<Ground_Check>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal"); z = Input.GetAxis("Vertical");

        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ground_Check.IsOnGround()) player_Movement.Jump();

        if(Input.GetMouseButtonDown(1)) isFocusMode = true;
        if(Input.GetMouseButtonUp(1))   isFocusMode = false;
    }

    private void Movement()
    {
        Vector3 forward = playerCamera.forward; Vector3 right = playerCamera.right;
        forward.y = 0; right.y = 0;

        Direction = forward * z + right * x;
        Direction.Normalize();

        player_Movement.Movement(Direction);

        if (isFocusMode)
        {
            Vector3 lookDirection = forward;
            transform.forward = Vector3.Slerp(transform.forward, lookDirection, 5 * Time.fixedDeltaTime);
        }
        else { if (Direction.sqrMagnitude > 0.1f) transform.forward = Vector3.Slerp(transform.forward, Direction, 5 * Time.fixedDeltaTime); }
    }
}
