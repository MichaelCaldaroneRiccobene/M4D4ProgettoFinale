using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move_F_And_B : PlatForm
{
    [SerializeField] Vector3 newPos;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 pos = Vector3.Lerp(startPos, newPos + startPos,t);
        transform.position = pos;
    }
}
