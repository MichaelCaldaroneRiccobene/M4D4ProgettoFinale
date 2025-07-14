using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSetTPS;
    [SerializeField] float minPitch = -10;
    [SerializeField] float maxPitch = 80;

    public float Sensitivity {  get;  set; }

    private float mouseInputX;
    private float mouseInputY;
    private float pitch;
    private float yaw;

    private void Awake()
    {
        if (target == null)
        {
            Player_Controller player = FindObjectOfType<Player_Controller>();
            target = player.transform;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate() => CameraMovement();

    private void CameraMovement()
    {
        mouseInputX = Input.GetAxis("Mouse X"); mouseInputY = Input.GetAxis("Mouse Y");

        pitch -= mouseInputY * Sensitivity; yaw += mouseInputX * Sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion camRotation = Quaternion.Euler(pitch, yaw, 0);

        transform.position = target.position + camRotation * offSetTPS;
        transform.LookAt(target.position);
    }
}


