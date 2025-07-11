using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Player : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSetTPS;

    [SerializeField] float sensitivity = 5;
    [SerializeField] float minYaw = -10;
    [SerializeField] float maxYaw = 80;

    private float mouseInputX;
    private float mouseInputY;
    private float mouseY;
    private float mouseX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseInputX = Input.GetAxis("Mouse X"); mouseInputY = Input.GetAxis("Mouse Y");
    }

    private void LateUpdate() => CameraMovement();

    private void CameraMovement()
    {
        mouseY -= mouseInputY * sensitivity; mouseX += mouseInputX * sensitivity;
        mouseY = Mathf.Clamp(mouseY, minYaw, maxYaw);

        Quaternion camRotation = Quaternion.Euler(mouseY, mouseX, 0);

        transform.position = target.position + camRotation * offSetTPS;
        transform.LookAt(target.position + Vector3.up * 2);
    }
}


