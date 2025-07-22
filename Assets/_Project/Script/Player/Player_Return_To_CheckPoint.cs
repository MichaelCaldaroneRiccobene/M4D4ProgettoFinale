using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Return_To_CheckPoint : MonoBehaviour, I_ITouch_Danger
{
    [Header("Setting CheckPoint")]
    [SerializeField] private GameObject refPlayer;
    [SerializeField] private float speedSpawnCheckPoint = 1.0f;
    [SerializeField] private float heightMidTravel = 15f;

    public UnityEvent onRecoverAnimation;
    public UnityEvent<bool> onDisableInput;
    public Vector3 PosLastCheckPoint { get; set; }
    public Quaternion RotLastCheckPoint { get; set; }

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 

        PosLastCheckPoint = transform.position;
        RotLastCheckPoint = transform.rotation;
    }

    public void Touch_Danger() => StartCoroutine(GoToCheckPoint());

    private IEnumerator GoToCheckPoint()
    {
        rb.isKinematic = true;
        onDisableInput?.Invoke(true);
        refPlayer.gameObject.SetActive(false);

        Vector3 startLocation = transform.position;
        Vector3 endLocation = PosLastCheckPoint;
        endLocation.y += 2;

        Vector3 midLocation = Vector3.Lerp(startLocation, endLocation, 0.5f);
        Vector3 mid = new Vector3(midLocation.x, startLocation.y + heightMidTravel, midLocation.z);

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
        onDisableInput?.Invoke(false);
        onRecoverAnimation?.Invoke();
    }
}
