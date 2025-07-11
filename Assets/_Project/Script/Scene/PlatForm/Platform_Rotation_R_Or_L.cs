using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Rotation_R_Or_L : PlatForm
{
    private void FixedUpdate()
    {
        transform.Rotate(0, speed, 0);   
    }
}
