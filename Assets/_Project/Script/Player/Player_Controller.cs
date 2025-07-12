using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speedSpawnCheckPoint = 1.0f;
    [SerializeField] private GameObject refPlayer;
    private  Camera playerCamera;
    public Vector3 Direction {  get; private set; }
    public Vector3 PosLastCheckPoint { get; set; }
    private float x;
    private float z;
    private bool isFocusMode;

    private Player_Movement player_Movement;
    private Ground_Check ground_Check;
    private Rigidbody rb;

    private void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        rb = GetComponent<Rigidbody>();
        ground_Check = GetComponentInChildren<Ground_Check>();
        PosLastCheckPoint = transform.position;
        playerCamera = Camera.main;
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal"); z = Input.GetAxis("Vertical");

        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ground_Check.IsOnGround()) player_Movement.Jump();

        if(Input.GetMouseButtonDown(1)) isFocusMode = true;
        if(Input.GetMouseButtonUp(1))   isFocusMode = false;
    }

    private void Movement()
    {
        Vector3 forward = playerCamera.transform.forward; Vector3 right = playerCamera.transform.right;
        forward.y = 0; right.y = 0;

        Direction = forward * z + right * x;
        Direction.Normalize();

        player_Movement.Movement(Direction);

        if (isFocusMode)
        {
            Vector3 lookDirection = forward;
            transform.forward = Vector3.Slerp(transform.forward, lookDirection, 5 * Time.fixedDeltaTime);
        }
        else { if (Direction.sqrMagnitude > 0.1f) transform.forward = Vector3.Slerp(transform.forward, Direction, 5 * Time.fixedDeltaTime); }
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
}
