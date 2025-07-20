using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move_F_And_B : PlatForm
{
    [Header("Setting")]
    [SerializeField] private float speedMoving = 5;
    [SerializeField] private bool isRandomSpeedMoving;

    [SerializeField] private float minSpeedMoving = 0.5f;
    [SerializeField] private float maxSpeedMoving = 3;

    [Header("Setting Ping Pong")]
    [SerializeField] Vector3 newPos;

    private Vector3 startPos;

    public override void Start()
    {
        base.Start();

        if (isRandomSpeedMoving)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedMoving, maxSpeedMoving) * factor;
            speedMoving = randomSpeed;
        }
        startPos = transform.position;
    }

    public void FixedUpdate()
    {
        float time = Mathf.PingPong(Time.time * speedMoving, 1f);
        Vector3 pos = Vector3.Lerp(startPos, newPos,time);
        transform.position = pos;
    }
}
