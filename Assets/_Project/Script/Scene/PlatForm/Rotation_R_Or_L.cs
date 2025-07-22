using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_R_Or_L : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speedRotation = 5;
    [SerializeField] private bool isRandomSpeedRotation;

    [SerializeField] private float minSpeedRotation = 0.5f;
    [SerializeField] private float maxSpeedRotation = 3;

    [Header("Setting Platform Rotation")]
    [SerializeField] private bool rotationInX;
    [SerializeField] private bool rotationInY;
    [SerializeField] private bool rotationInZ;
    private void Start()
    {
        if (isRandomSpeedRotation)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedRotation, maxSpeedRotation) * factor;
            speedRotation = randomSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (rotationInX) transform.Rotate(speedRotation * Time.fixedDeltaTime,0, 0);

        if (rotationInY) transform.Rotate(0, speedRotation * Time.fixedDeltaTime, 0);

        if (rotationInZ) transform.Rotate(0, 0 , speedRotation * Time.fixedDeltaTime);
    }
}
