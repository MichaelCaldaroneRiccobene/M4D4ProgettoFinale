using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    [Header("OnMenu")]
    public UnityEvent goOnMenu;

    [Header("OnLooking")]
    public UnityEvent<float, float> onDirectionLook;

    [Header("OnMoving")]
    public UnityEvent<float,float> onDirectionMove;
    public UnityEvent <bool> isOnFocus;
    public UnityEvent<bool> isRunning;

    [Header("OnJumping")]
    public UnityEvent onJump;
    public UnityEvent onJumpAir;

    [Header("OnAttak")]
    public UnityEvent onShoot;

    private float horizontal;
    private float vertical;

    private float mouseInputX;
    private float mouseInputY;

    private bool isFocusMode;
    private bool onDisableInput;

    private void Update() => InputPlayer();

    private void FixedUpdate()
    {
        if (onDisableInput) return;
        if (Input.GetKey(KeyCode.Space)) onJumpAir?.Invoke();
    }

    private void InputPlayer()
    {
        //Menu
        if (Input.GetKeyDown(KeyCode.Escape)) goOnMenu?.Invoke();

        if (onDisableInput) return;

        //Move
        horizontal = Input.GetAxis("Horizontal"); vertical = Input.GetAxis("Vertical");
        onDirectionMove?.Invoke(horizontal, vertical);;

        //Look
        mouseInputX = Input.GetAxis("Mouse X"); mouseInputY = Input.GetAxis("Mouse Y");
        onDirectionLook?.Invoke(mouseInputX, mouseInputY);;

        // Jump And JumpAir
        if (Input.GetKeyDown(KeyCode.Space)) onJump?.Invoke();

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
        if (Input.GetMouseButtonDown(0)) onShoot?.Invoke();

        //Runnig
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning?.Invoke(true);
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning?.Invoke(false);

    }

    public void OnDisableInput(bool onDisableInput)
    {
        isFocusMode = false;
        isOnFocus?.Invoke(isFocusMode);
        this.onDisableInput = onDisableInput;
    }
}
