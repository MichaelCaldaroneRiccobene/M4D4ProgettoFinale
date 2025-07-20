using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Look_Camera : MonoBehaviour
{
    private void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform.position);
            transform.Rotate(0, 180, 0);
        }       
    }
}
