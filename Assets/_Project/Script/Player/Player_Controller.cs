using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour,I_IDamage,I_Touch_Water
{
    [SerializeField] private float speedSpawnCheckPoint = 1.0f;
    [SerializeField] private GameObject refPlayer;
    [SerializeField] private Transform refCamera;
    [SerializeField] private Vector3 refCameraFocus;
    [SerializeField] private float sensitivity = 5;

    [SerializeField] private float maxFov = 50;
    [SerializeField] private float minFov = 30;


    private Camera_Controller playerCamera;
    public Vector3 Direction {  get; private set; }
    public Vector3 PosLastCheckPoint { get; set; }
    public Quaternion RotLastCheckPoint { get; set; }

    private float x;
    private float z;
    private bool isFocusMode;

    private Player_Movement player_Movement;
    private Player_Animation player_Animation;
    private Player_Shooter player_Shooter;
    private Ground_Check ground_Check;
    private Life_Controller life;
    private Rigidbody rb;

    private void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        player_Animation = GetComponentInChildren<Player_Animation>();
        rb = GetComponent<Rigidbody>();
        player_Shooter = GetComponent<Player_Shooter>();
        ground_Check = GetComponentInChildren<Ground_Check>();
        life = GetComponent<Life_Controller>();
        PosLastCheckPoint = transform.position;
        playerCamera = GetComponentInChildren<Camera_Controller>();
        player_Movement.rb = rb;

        //playerCamera.Sensitivity = sensitivity;
        RotLastCheckPoint = Quaternion.Euler(0, 90, 0);

    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal"); z = Input.GetAxis("Vertical");

        InputPlayer();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void InputPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ground_Check.IsOnGround())
        {
            player_Movement.Jump();
            player_Animation.OnJump();
        }
        if (Input.GetKey(KeyCode.Space) && !ground_Check.IsOnGround())
        {
            player_Movement.JumpInAir();
            player_Animation.OnJumpAir();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(Camera.main == null) return;
            playerCamera.Sensitivity = sensitivity / 2;
            Camera.main.fieldOfView = minFov;
            //refPlayer.transform.rotation = Quaternion.Euler(0,0,0); 
            //refPlayer.transform.position = new Vector3(0,0,0);
            isFocusMode = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (Camera.main == null) return;
            playerCamera.Sensitivity = sensitivity;
            Camera.main.fieldOfView = maxFov;
            isFocusMode = false;
        }


        if (Input.GetMouseButtonUp(0) && isFocusMode) player_Shooter.TryToShoot();
        //if (Input.GetMouseButtonUp(0)) pasue = !pasue;

        if (Input.GetKeyDown(KeyCode.LeftShift)) player_Movement.isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) player_Movement.isRunning = false;
    }

    private void Movement()
    {
        if(playerCamera == null) return;
        if(rb.isKinematic) return;

        Vector3 forward = playerCamera.transform.forward; Vector3 right = playerCamera.transform.right;
        forward.y = 0; right.y = 0;

        if (x != 0 || z != 0)
        {
            Direction = forward * z + right * x;
            Direction.Normalize();

            player_Movement.Movement(Direction);
        }
        else
        {
            if (Direction.sqrMagnitude > 0.1f)
            {
                if (ground_Check.IsOnGround()) player_Movement.DownForce();
            }               
        }

        if (isFocusMode)
        {
            Vector3 lookDirection = forward;
            transform.forward = Vector3.Slerp(transform.forward, lookDirection, 5 * Time.fixedDeltaTime);
        }
        else { if (Direction.sqrMagnitude > 0.1f) transform.forward = Vector3.Slerp(transform.forward, Direction, 5 * Time.fixedDeltaTime);}
    }

    public void Water()
    {
        if(life.isDead()) return;
        StartCoroutine(GoToCheckPoint());
    }

    private IEnumerator GoToCheckPoint()
    {
        rb.isKinematic = true;
        refPlayer.gameObject.SetActive(false);

        Vector3 startLocation = transform.position;
        Vector3 endLocation = PosLastCheckPoint;
        endLocation.y += 2;

        Vector3 midLocation = Vector3.Lerp(startLocation, endLocation,0.5f);
        Vector3 mid = new Vector3(midLocation.x, startLocation.y + 20, midLocation.z);

        float progression = 0;
        while (progression < 1)
        {
            progression += Time.deltaTime * speedSpawnCheckPoint;
            float smooth = Mathf.SmoothStep(0, 1, progression);
            Vector3 interpolate = Vector3.Lerp(startLocation, mid, smooth);

            transform.position = interpolate;
            yield return null;
        }

        progression = 0;
        while (progression < 1)
        {
            progression += Time.deltaTime * speedSpawnCheckPoint;
            float smooth = Mathf.SmoothStep(0, 1, progression);
            Vector3 interpolate = Vector3.Lerp(mid, endLocation, smooth);

            transform.position = interpolate;
            yield return null;
        }
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        transform.rotation = RotLastCheckPoint;

        refPlayer.gameObject.SetActive(true);
        player_Animation.Recover();
    }

    public void Damage(int ammount)
    {
        life.UpdateHp(ammount);
    }
}
