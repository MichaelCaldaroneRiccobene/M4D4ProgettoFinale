using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    [SerializeField] protected float speed = 5;
    [SerializeField] protected bool isRandomSpeed;
    [SerializeField] protected float minSpeed = 0.5f;
    [SerializeField] protected float maxSpeed = 3;

    protected Vector3 lastPos;
    protected Quaternion lastRot;
    protected HashSet<Rigidbody> objInPlatform = new();

    public virtual void Start()
    {
        if (isRandomSpeed)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeed, maxSpeed) * factor;
            speed = randomSpeed;
        }

        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    public virtual void FixedUpdate()
    {
        //Vector3 movement = transform.position - lastPos;
        //Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRot);

        //foreach (var rb in objInPlatform)
        //{
        //    if (rb != null)
        //    {
        //        rb.MovePosition(rb.position + movement);
        //        rb.MoveRotation(deltaRotation * rb.rotation);
        //    }
        //}

        //lastPos = transform.position;
        //lastRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Rigidbody rb = collision.collider.attachedRigidbody;
        //if (rb != null && !objInPlatform.Contains(rb))
        //{
        //    objInPlatform.Add(rb);
        //}

        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Rigidbody rb = collision.collider.attachedRigidbody;
        //if (rb != null)
        //{
        //    objInPlatform.Remove(rb);
        //}
        collision.collider.transform.SetParent(null);

    }
}
