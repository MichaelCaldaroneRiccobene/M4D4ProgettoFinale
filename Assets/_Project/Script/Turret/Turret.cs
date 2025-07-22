using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour,I_IDamage
{
    [Header("Bullet Setting")]
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speedBullet = 0.5f;

    [Header("Logic Shooting")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate = 1;
    [SerializeField] protected float distanceToShoot = 10;

    [Header("Logic Life")]
    [SerializeField] protected int hitForDeth = 3;

    public Transform Target {  get; set; }
    public Control_Turrent Control_Turrent { get; set; }

    protected float lastTimeShoot;
    protected bool isShooting;

    public virtual void Update() => TryToShoot();

    public virtual void TryToShoot()
    {
        float distance = Vector3.Distance(transform.position,Target.position);
        if (distance >= distanceToShoot) return;

        if (!isShooting && Time.time - lastTimeShoot >= fireRate) Shoot();
    }

    public virtual void Shoot()
    {
        Bullet b = Control_Turrent.GetBullet();
        
        b.transform.position = firePoint.position;
        b.Damage = damage;
        b.Dir = transform.forward;
        b.SpeedBullet = speedBullet;

        b.gameObject.SetActive(true);
        lastTimeShoot = Time.time;
    }

    public void Damage(int ammount)
    {
        hitForDeth--;
        if (hitForDeth <= 0) Destroy(gameObject);
    }
}
