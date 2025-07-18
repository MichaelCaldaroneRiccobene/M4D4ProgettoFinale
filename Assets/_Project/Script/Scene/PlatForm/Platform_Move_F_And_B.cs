using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move_F_And_B : PlatForm
{
    [Header("Setting Ping Pong")]
    [SerializeField] Vector3 newPos;

    private Vector3 startPos;

    public override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    public void FixedUpdate()
    {
        float time = Mathf.PingPong(Time.time * speedMoving, 1f);
        Vector3 pos = Vector3.Lerp(startPos, newPos,time);
        transform.position = pos;
    }
}
