using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour,I_IDamage
{
    [SerializeField] private float speedSpawnCheckPoint = 1.0f;
    [SerializeField] private GameObject refPlayer;
    [SerializeField] private Transform refCamera;
    [SerializeField] private Vector3 refCameraFocus;
    [SerializeField] private float sensitivity = 5;

    public Vector3 refCameraStandard;
    public Vector3 refCameraStandards;
    public Vector3 refCameraFocuss;

    private Camera_Controller playerCamera;
    public Vector3 Direction {  get; private set; }
    public Vector3 PosLastCheckPoint { get; set; }
    private float x;
    private float z;
    private bool isFocusMode;

    private Player_Movement player_Movement;
    private Player_Shooter player_Shooter;
    private Ground_Check ground_Check;
    private Life_Controller life;
    private Rigidbody rb;
    private bool pasue;

    private void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        rb = GetComponent<Rigidbody>();
        player_Shooter = GetComponent<Player_Shooter>();
        ground_Check = GetComponentInChildren<Ground_Check>();
        life = GetComponent<Life_Controller>();
        PosLastCheckPoint = transform.position;
        playerCamera = GetComponentInChildren<Camera_Controller>();
        player_Movement.rb = rb;

        playerCamera.Sensitivity = sensitivity;

        //refCamera.transform.position = refCameraStandard + transform.position;
        //refCameraStandards = refCameraStandard;
        //refCameraFocuss = refCameraFocus;

    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal"); z = Input.GetAxis("Vertical");

        InputPlayer();

        if (pasue) Time.timeScale = 0.01f;
        else Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void InputPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ground_Check.IsOnGround()) player_Movement.Jump();

        if (Input.GetMouseButtonDown(1))
        {
            //refCameraFocus = refCameraFocuss;
            //refCamera.transform.position = refCameraFocus + transform.position;
            playerCamera.Sensitivity = sensitivity / 2;
            Camera.main.fieldOfView = 30;
            isFocusMode = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            //refCameraStandard = refCameraStandards;
            //refCamera.transform.position = refCameraStandard + transform.position;
            playerCamera.Sensitivity = sensitivity;
            Camera.main.fieldOfView = 80;
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
            if (ground_Check.IsOnGround()) player_Movement.DownForce();
        }

        if (isFocusMode)
        {
            Vector3 lookDirection = forward;
            transform.forward = Vector3.Slerp(transform.forward, lookDirection, 5 * Time.fixedDeltaTime);
        }
        else { if (Direction.sqrMagnitude > 0.1f) transform.forward = Vector3.Slerp(transform.forward, Direction, 5 * Time.fixedDeltaTime);}
    }
    public void CheckPoint()
    {
        StartCoroutine(GoToCheckPoint());
    }

    private IEnumerator GoToCheckPoint()
    {
        rb.isKinematic = true;
        refPlayer.gameObject.SetActive(false);
        Vector3 startLocation = transform.position;
        Vector3 endLocation = PosLastCheckPoint;

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
        transform.rotation = Quaternion.Euler(0, 90, 0);
        refPlayer.gameObject.SetActive(true);
    }

    public void Damage(int ammount)
    {
        life.UpdateHp(ammount);
    }
}
