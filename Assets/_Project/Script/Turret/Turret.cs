using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float speedAnimation = 0.1f;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private Bullet bulletPreFab;
    [SerializeField] private float speedBullet = 10;
    [SerializeField] private Transform firePoint;

    public Player_Controller Player_Controller {  get; set; }
    private float lastTimeShoot;
    private Vector3 startPos;
    private Vector3 endPos = new Vector3(0,3,0);
    private List<Bullet> bulletsPool = new List<Bullet>();

    private void Start()
    {
        startPos = transform.position;
        endPos += startPos;
        StartCoroutine(AnimationUpDown());

        if(Player_Controller == null ) Player_Controller = FindAnyObjectByType<Player_Controller>();
    }

    private void Update()
    {
        TryToShoot();

        transform.LookAt(Player_Controller.transform);
    }

    private void TryToShoot()
    {
        if (Time.time - lastTimeShoot >= fireRate) Shoot();
    }

    private void Shoot()
    {
        lastTimeShoot = Time.time;

        Bullet b = GetBullet();
        b.gameObject.SetActive(true); b.transform.position = firePoint.position;
        b.Dir = Player_Controller.transform.position - transform.position;
        b.Speed = speedBullet;
    }

    IEnumerator AnimationUpDown()
    {
        float progress = 0;

        while (progress < 1)
        {
            progress += Time.fixedDeltaTime * speedAnimation;
            float smooth = Mathf.SmoothStep(0,1,progress);
            Vector3 interpolate = Vector3.Lerp(startPos, endPos, smooth);

            transform.position = interpolate;
            yield return null;
        }

        progress = 0;

        while (progress < 1)
        {
            progress += Time.fixedDeltaTime * speedAnimation;
            float smooth = Mathf.SmoothStep(0, 1, progress);
            Vector3 interpolate = Vector3.Lerp(endPos, startPos, smooth);

            transform.position = interpolate;
            yield return null;
        }

        StartCoroutine(AnimationUpDown());
    }

    private Bullet GetBullet()
    {
        foreach(Bullet b in bulletsPool)
        {
            if(!b.gameObject.activeInHierarchy) return b;
        }
        return SpawnBullet();
    }

    private Bullet SpawnBullet()
    {
        Bullet b = Instantiate(bulletPreFab);
        bulletsPool.Add(b);
        b.gameObject.SetActive(false);
        return b;   
    }
}
