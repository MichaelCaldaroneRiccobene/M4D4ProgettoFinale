using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float lifeTime = 5;

    public float Speed {  get; set; }
    public Vector3 Dir {  get; set; }
    public int Damage;

    protected Rigidbody rb;
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public virtual void OnEnable()
    {
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        Invoke("Disable", lifeTime);
    }

    public virtual void FixedUpdate()
    {
        rb.AddForce(Dir * Speed, ForceMode.Force);
    }

    public virtual void OnDisable()
    {
        CancelInvoke();
    }

    public virtual void Disable()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        I_IDamage damage = collision.collider.GetComponent<I_IDamage>();
        if (damage != null) damage.Damage(-Damage);
    }
}
