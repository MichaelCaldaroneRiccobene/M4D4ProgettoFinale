using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Rotation_R_Or_L : PlatForm
{
    [SerializeField] private bool isX;
    [SerializeField] private bool isY;
    private void FixedUpdate()
    {
        if (isX) transform.Rotate(speed * Time.fixedDeltaTime,0, 0);

        if (isY) transform.Rotate(0, speed * Time.fixedDeltaTime, 0);
    }
}
