using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Check : MonoBehaviour
{
    [SerializeField] float raySphere = 0.2f;
    [SerializeField] LayerMask layerGround;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raySphere);
    }

    public bool IsOnGround() => Physics.CheckSphere(transform.position, raySphere, layerGround);
}
