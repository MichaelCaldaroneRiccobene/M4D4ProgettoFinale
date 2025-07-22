using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Animation : MonoBehaviour
{
    [Header("Animation Setting")]
    [SerializeField] protected float speedAnimation = 0.1f;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 newPos;

    private void Start()
    {
        startPos = transform.position;
        newPos += startPos;
    }

    public void FixedUpdate()
    {
        float progression = Mathf.PingPong(Time.time * speedAnimation, 1f);

        float smooth = Mathf.SmoothStep(0, 1, progression);
        Vector3 pos = Vector3.Lerp(startPos, newPos, progression);

        transform.position = pos;
    }
}
