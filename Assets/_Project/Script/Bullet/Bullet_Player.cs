using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Player : Bullet
{
    [SerializeField] private float  radius = 15;
    [SerializeField] private float force = 1;

    public override void OnCollisionEnter(Collision collision)
    {
       base.OnCollisionEnter(collision);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                Vector3 posCollider = collider.attachedRigidbody.position - transform.position;

                rb.AddForce(posCollider * force, ForceMode.Impulse);
            }
        }

        Debug.DrawRay(transform.position, Vector3.up * 0.1f, Color.yellow, 1f); // punto centrale
        DrawExplosionDebugSphere(transform.position, radius, 1f);

        gameObject.SetActive(false);
    }

    private void DrawExplosionDebugSphere(Vector3 center, float radius, float duration)
    {
        int steps = 20;
        for (int i = 0; i < steps; i++)
        {
            float angle = i * Mathf.PI * 2 / steps;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Vector3 start = center + offset;
            Vector3 end = center + Quaternion.Euler(0, 360f / steps, 0) * offset;
            Debug.DrawLine(start, end, Color.yellow, duration);
        }
    }

}
