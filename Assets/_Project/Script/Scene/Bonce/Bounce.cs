using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float forceImpulce = 10;
    [SerializeField] protected bool isRandomForce;
    [SerializeField] protected float minForce = 0.5f;
    [SerializeField] protected float maxForce = 3;

    private void Start()
    {
        if (isRandomForce)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomForce = Random.Range(minForce, maxForce) * factor;
            forceImpulce = randomForce;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb == null) return;

        Vector3 dir = rb.position - transform.position;
        rb.AddForce(dir *  forceImpulce,ForceMode.Impulse);
    }
}
