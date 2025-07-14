using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;

    public float Speed {  get; set; }
    public Vector3 Dir {  get; set; }
    public int Damage;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        Invoke("Disable", lifeTime);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Dir * Speed, ForceMode.Force);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Disable()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        I_IDamage damage = collision.collider.GetComponent<I_IDamage>();
        if (damage != null) damage.Damage(-Damage);
    }
}
