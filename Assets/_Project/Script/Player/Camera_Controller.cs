using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSetTPS;

    [SerializeField] float minPitch = -10;
    [SerializeField] float maxPitch = 80;

    [Header("Setting Camera Specifics")]
    [SerializeField] private float sensitivityNormal = 5;
    [SerializeField] private float sensitivityFocus = 2.5f;

    [SerializeField] private float maxFov = 50;
    [SerializeField] private float minFov = 30;

    private float sensitivity;

    private float mouseInputX;
    private float mouseInputY;

    private float pitch;
    private float yaw;

    private bool isOnFocus = false;

    private void Awake()
    {
        if (target == null)
        {
            Player_Controller player = FindObjectOfType<Player_Controller>();
            target = player.transform;
        }

        SetSensitivity(isOnFocus);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate() => CameraMovement();

    public void SetSensitivity(bool onFocus)
    {
        isOnFocus = onFocus;

        if (isOnFocus)
        {
            sensitivity = sensitivityFocus;
            Camera.main.fieldOfView = minFov;
        }
        else
        {
            sensitivity = sensitivityNormal;
            Camera.main.fieldOfView = maxFov;
        }
    }

    private void CameraMovement()
    {
        pitch -= mouseInputY * sensitivity * Time.deltaTime; 
        yaw += mouseInputX * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion camRotation = Quaternion.Euler(pitch, yaw, 0);

        transform.position = target.position + camRotation * offSetTPS;
        transform.LookAt(target.position);
    }

    public void TakeInput(float mouseInputX,float mouseInputY)
    {
        this.mouseInputX = mouseInputX;
        this.mouseInputY = mouseInputY;
    }
         
}


