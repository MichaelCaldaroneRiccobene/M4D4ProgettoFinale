using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] protected float speedMoving = 5;
    [SerializeField] protected float speedRotation = 5;

    [Header("Setting Random")]
    [SerializeField] protected bool isRandomSpeedMoving;
    [SerializeField] protected bool isRandomSpeedRotation;

    [SerializeField] protected float minSpeedMoving = 0.5f;
    [SerializeField] protected float maxSpeedMoving = 3;

    [SerializeField] protected float minSpeedRotation = 0.5f;
    [SerializeField] protected float maxSpeedRotation = 3;

    protected Vector3 lastPos;
    protected Quaternion lastRot;
    protected HashSet<Rigidbody> objInPlatform = new();

    public virtual void Start()
    {
        if (isRandomSpeedMoving)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedMoving, maxSpeedMoving) * factor;
            speedMoving = randomSpeed;
        }

        if (isRandomSpeedRotation)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedRotation, maxSpeedRotation) * factor;
            speedRotation = randomSpeed;
        }

        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.collider.transform.SetParent(null);
    }
}
