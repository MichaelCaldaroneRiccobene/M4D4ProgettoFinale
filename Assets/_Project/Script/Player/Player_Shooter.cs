using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Shooter : MonoBehaviour
{
    [Header("Bullet Setting")]
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage;
    [SerializeField] private float speedBullet = 1;

    [Header("Logic Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform parentBulletPlayer;
    [SerializeField] private float fireRate = 0.5f;

    public UnityEvent OnAttack;

    private float lastTimeShoot;
    private bool isOnFocus;
    private List<Bullet> bulletsPool = new List<Bullet>();

    public void TryToShoot() { if (Time.time - lastTimeShoot >= fireRate && isOnFocus) Shoot(); }

    private void Shoot()
    {
        Bullet b = GetBullet();

        b.gameObject.SetActive(true); b.transform.position = firePoint.position;
        b.Dir = Camera.main.transform.forward;
        b.Speed = speedBullet;
        OnAttack?.Invoke();

        lastTimeShoot = Time.time;
    }

    public virtual Bullet GetBullet()
    {
        foreach (Bullet b in bulletsPool)
        {
            if (!b.gameObject.activeInHierarchy) return b;
        }
        return SpawnBullet();
    }

    public virtual Bullet SpawnBullet()
    {
        Bullet b;
        if (parentBulletPlayer != null) b = Instantiate(bullet, parentBulletPlayer);
        else b = Instantiate(bullet);

        bulletsPool.Add(b);
        b.Damage = damage;
        b.gameObject.SetActive(false);
        return b;
    }

    public void IsOnFocus(bool isOnFocus) => this.isOnFocus = isOnFocus;
}
