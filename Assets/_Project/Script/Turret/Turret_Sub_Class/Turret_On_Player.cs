using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_On_Player : Turret
{
    [Header("Logic Look Player")]
    [SerializeField] protected float lookDownForPlayer = -8;

    public override void Update()
    {
        if (Target == null) return;
        base.Update();
        transform.LookAt(Target.transform.position + Vector3.up * lookDownForPlayer);
    }

    public override void Shoot()
    {
        Bullet b = Control_Turrent.GetBullet();

        b.transform.position = firePoint.position;
        b.Dir = Target.transform.position - transform.position;
        b.Damage = damage;
        b.SpeedBullet = speedBullet;

        b.gameObject.SetActive(true);
        lastTimeShoot = Time.time;
    }
}
