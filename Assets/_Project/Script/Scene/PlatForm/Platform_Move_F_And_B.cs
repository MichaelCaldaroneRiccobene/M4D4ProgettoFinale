using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move_F_And_B : PlatForm
{
    [SerializeField] Vector3 newPos;

    private Vector3 startPos;

    public override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 pos = Vector3.Lerp(startPos, newPos,t);
        transform.position = pos;
    }
}
