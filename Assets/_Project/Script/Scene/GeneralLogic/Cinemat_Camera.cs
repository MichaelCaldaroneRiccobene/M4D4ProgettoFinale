using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Cinemat_Camera : MonoBehaviour
{
    [SerializeField] private float speed = 0.8f;
    [SerializeField] private Vector3[] points;
    [SerializeField] private Camera_Controller camController;

    private void Start()
    {
        StartCoroutine(Camera());
        Invoke("OffCamera", 0.01f);
    }

    private void OffCamera()
    {
        camController.gameObject.SetActive(false);
    }

    private IEnumerator Camera()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 startLocation = transform.position;
            Vector3 endLocation = points[i];

            float progression = 0;
            while (progression < 1)
            {
                progression += Time.deltaTime * speed;
                Vector3 interpolate = Vector3.Lerp(startLocation, endLocation, progression);

                transform.position = interpolate;
                yield return null;
            }
        }

        Vector3 startLoca = transform.position;
        Vector3 endLoca = camController.transform.position;

        float progre = 0;
        while (progre < 1)
        {
            progre += Time.deltaTime * speed;
            float smooth = Mathf.SmoothStep(0, 1, progre);
            Vector3 interpolate = Vector3.Lerp(startLoca, endLoca, smooth);
            Quaternion interpolateRot = Quaternion.Lerp(transform.rotation, camController.transform.rotation, smooth);

            transform.position = interpolate;
            transform.rotation = interpolateRot;
            yield return null;
        }
        camController.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
