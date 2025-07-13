using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] protected float speedAnimation = 0.1f;
    [SerializeField] protected float fireRate = 1;
    [SerializeField] protected int bulletToShoot = 1;
    [SerializeField] protected float timeForSpawnBullet = 0;
    [SerializeField] protected Bullet bulletPreFab;
    [SerializeField] protected float speedBullet = 10;
    [SerializeField] protected Transform firePoint;

    public Transform ParentWepon {  get; set; }
    public Player_Controller Player_Controller {  get; set; }
    protected float lastTimeShoot;
    protected Vector3 startPos;
    protected Vector3 endPos = new Vector3(0,3,0);
    protected List<Bullet> bulletsPool = new List<Bullet>();
    protected bool isShooting;

    public virtual void Start()
    {
        startPos = transform.position;
        endPos += startPos;
        StartCoroutine(AnimationUpDown());

        if(Player_Controller == null ) Player_Controller = FindAnyObjectByType<Player_Controller>();
    }

    public virtual void Update()
    {
        TryToShoot();

        transform.LookAt(Player_Controller.transform);
    }

    public virtual void TryToShoot()
    {
        if (!isShooting && Time.time - lastTimeShoot >= fireRate) StartCoroutine(Shoot());
    }

    public virtual IEnumerator Shoot()
    {
        isShooting = true;
        for (int i = 0; i < bulletToShoot; i++)
        {
            Bullet b = GetBullet();
            b.gameObject.SetActive(true); b.transform.position = firePoint.position;
            b.Dir = Player_Controller.transform.position - transform.position;
            b.Speed = speedBullet;
            yield return new WaitForSeconds(timeForSpawnBullet);
        }
        isShooting = false; 
        lastTimeShoot = Time.time;
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

    public virtual Bullet GetBullet()
    {
        foreach(Bullet b in bulletsPool)
        {
            if(!b.gameObject.activeInHierarchy) return b;
        }
        return SpawnBullet();
    }

    public virtual Bullet SpawnBullet()
    {
        Bullet b;
        if (ParentWepon != null) b = Instantiate(bulletPreFab,ParentWepon);
        else b = Instantiate(bulletPreFab);

        bulletsPool.Add(b);
        b.gameObject.SetActive(false);
        return b;   
    }
}
