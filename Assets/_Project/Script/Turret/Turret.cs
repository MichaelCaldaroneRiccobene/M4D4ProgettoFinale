using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour,I_IDamage
{
    [Header("Bullet Setting")]
    [SerializeField] protected Bullet bulletPreFab;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speedBullet = 10;
    [SerializeField] protected int bulletToShoot = 1;

    [Header("Logic Shooting")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate = 1;
    [SerializeField] protected float distanceToShoot = 10;
    [SerializeField] protected float timeForNextBulletToShootInSequence = 0;

    [Header("Animation Setting")]
    [SerializeField] protected float speedAnimation = 0.1f;

    [Header("Logic Life")]
    [SerializeField] protected int hitForDeth = 3;

    public Transform ParentBulletTurret {  get; set; }
    public Player_Controller Player_Controller { get; set; }

    protected Vector3 startPos;
    protected Vector3 endPos = new Vector3(0,3,0);

    protected float lastTimeShoot;

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
        if(Player_Controller == null) return;
        TryToShoot();

        transform.LookAt(Player_Controller.transform);
    }

    IEnumerator AnimationUpDown()
    {
        float progress = 0;

        while (true)
        {
            while (progress < 1)
            {
                progress += Time.fixedDeltaTime * speedAnimation;
                float smooth = Mathf.SmoothStep(0, 1, progress);
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
        }
    }

    public virtual void TryToShoot()
    {
        float distance = Vector3.Distance(transform.position,Player_Controller.transform.position);
        if (distance >= distanceToShoot) return;

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

            yield return new WaitForSeconds(timeForNextBulletToShootInSequence);
        }
        isShooting = false; 
        lastTimeShoot = Time.time;
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
        if (ParentBulletTurret != null) b = Instantiate(bulletPreFab,ParentBulletTurret);
        else b = Instantiate(bulletPreFab);

        bulletsPool.Add(b);
        b.Damage = damage;
        b.gameObject.SetActive(false);

        return b;   
    }

    public void Damage(int ammount)
    {
        hitForDeth--;
        if (hitForDeth <= 0) Destroy(gameObject);
    }
}
