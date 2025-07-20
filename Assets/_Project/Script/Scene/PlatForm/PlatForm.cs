using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    protected Vector3 lastPos;
    protected Quaternion lastRot;

    public virtual void Start()
    {
        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision) => collision.collider.transform.SetParent(transform);

    private void OnCollisionExit(Collision collision) => collision.collider.transform.SetParent(null);
}
