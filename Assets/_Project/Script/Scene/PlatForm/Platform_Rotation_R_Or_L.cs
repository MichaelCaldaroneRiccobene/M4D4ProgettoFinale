using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Rotation_R_Or_L : PlatForm
{
    [Header("Setting Platform Rotation")]
    [SerializeField] private bool rotationInX;
    [SerializeField] private bool rotationInY;
    [SerializeField] private bool rotationInZ;

    public virtual void FixedUpdate()
    {
        if (rotationInX) transform.Rotate(speedRotation * Time.fixedDeltaTime,0, 0);

        if (rotationInY) transform.Rotate(0, speedRotation * Time.fixedDeltaTime, 0);

        if (rotationInZ) transform.Rotate(0, 0 , speedRotation * Time.fixedDeltaTime);
    }
}
