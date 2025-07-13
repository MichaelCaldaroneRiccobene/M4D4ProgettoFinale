using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Magic : Turret
{
    public override void TryToShoot()
    {
        if (!isShooting && Time.time - lastTimeShoot >= fireRate) StartCoroutine(Shoot());
    }

    public override IEnumerator Shoot()
    {
        isShooting = true;
        for (int i = 0; i < bulletToShoot; i++)
        {
            float factor = Random.Range(0,2) == 0 ? 1 : -1;
            Vector3 randomPos = new Vector3(Random.Range(0.5f, 3) * factor, Random.Range(0.5f, 3) * factor, Random.Range(0.5f, 3) * factor);

            Bullet b = GetBullet();
            b.gameObject.SetActive(true); b.transform.position = firePoint.position + randomPos;
            b.Dir = Player_Controller.transform.position - transform.position;
            b.Speed = speedBullet;
            yield return new WaitForSeconds(timeForSpawnBullet);
        }
        isShooting = false;
        lastTimeShoot = Time.time;
    }
}
