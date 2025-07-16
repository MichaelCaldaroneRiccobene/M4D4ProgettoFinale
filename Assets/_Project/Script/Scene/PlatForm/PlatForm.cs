using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    [SerializeField] protected float speed = 5;
    [SerializeField] protected bool isRandomSpeed;
    [SerializeField] protected float minSpeed = 0.5f;
    [SerializeField] protected float maxSpeed = 3;
    [SerializeField] protected bool isOnCollision;

    private void Start()
    {
        if(isRandomSpeed)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeed, maxSpeed) * factor;
            speed = randomSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isOnCollision) return;
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOnCollision) return;
        other.transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isOnCollision) return;
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isOnCollision) return;
        collision.collider.transform.SetParent(null);
    }
}
