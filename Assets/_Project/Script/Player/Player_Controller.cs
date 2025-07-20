using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    [Header("OnMenu")]
    public UnityEvent goOnMenu;

    [Header("OnMoving")]
    public UnityEvent<Vector3,float,float> onDirection;
    public UnityEvent <bool> isOnFocus;
    public UnityEvent<bool> isRunning;

    [Header("OnJumping")]
    public UnityEvent onJump;
    public UnityEvent onJumpAir;

    [Header("OnAttak")]
    public UnityEvent onShoot;

    private Vector3 direction;
    private float horizontal;
    private float vertical;

    private bool isFocusMode;

    private void Update() => InputPlayer();


    private void FixedUpdate() => onDirection?.Invoke(direction, horizontal,vertical);

    private void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal"); vertical = Input.GetAxis("Vertical");

        //Menu
        if (Input.GetKeyDown(KeyCode.Escape)) goOnMenu?.Invoke();

        // Jump And JumpAir
        if (Input.GetKeyDown(KeyCode.Space)) onJump?.Invoke();

        if (Input.GetKey(KeyCode.Space)) onJumpAir?.Invoke();
        //

        // Camera
        if (Input.GetMouseButtonDown(1))
        {   
            isFocusMode = true;
            isOnFocus?.Invoke(isFocusMode);
        }

        if (Input.GetMouseButtonUp(1))
        {
            isFocusMode = false;
            isOnFocus?.Invoke(isFocusMode);
        }
        //

        //Shooting
        if (Input.GetMouseButtonUp(0)) onShoot?.Invoke();

        //Runnig
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning?.Invoke(true);
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning?.Invoke(false);
    }
}
