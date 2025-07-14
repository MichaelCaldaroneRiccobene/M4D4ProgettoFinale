using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private int damage;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float speedBullet = 1;
    [SerializeField] private Transform parentBulletPlayer;

    private float lastTimeShoot;
    private List<Bullet> bulletsPool = new List<Bullet>();

    public void TryToShoot()
    {
        if(Time.time - lastTimeShoot >= fireRate) Shoot();
    }

    private void Shoot()
    {
        //Vector3 dir = Physics.Linecast(firePoint.position, cam.transform.forward * 100);

        Bullet b = GetBullet();
        b.gameObject.SetActive(true); b.transform.position = firePoint.position;
        b.Dir = Camera.main.transform.forward + Camera.main.transform.right * 0.05f; // Camera.main.transform.up * 0.1f;//+ Camera.main.transform.right * 0.3f;
        b.Speed = speedBullet;

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
}
