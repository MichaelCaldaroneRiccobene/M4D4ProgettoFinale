using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cinemat_Camera : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speed = 0.8f; 
    [SerializeField] private Camera_Controller camController;
    [SerializeField] private float timeForGoToMainCamera;
    [SerializeField] private Image[] imageUiPlayer;
    [SerializeField] private TextMeshProUGUI[] textUiPlayer;

    public UnityEvent<bool> onDisableInput;

    //[SerializeField] private Vector3[] points; Old

    private void Start()
    {
        if (camController == null) gameObject.SetActive(false);
        else
        {
            StartCoroutine(GoToMainCamera());
            Invoke("OffMainCamera", 0.01f);
        }

        if (imageUiPlayer != null)
        {
            for (int i = 0; i < imageUiPlayer.Length; i++)
            {
                Color color = imageUiPlayer[i].color;
                color.a = 0f;
                imageUiPlayer[i].color = color;
            }
        }

        if (textUiPlayer != null)
        {
            for (int i = 0; i < textUiPlayer.Length; i++)
            {
                if (textUiPlayer[i] != null)
                {
                    Color color = textUiPlayer[i].color;
                    color.a = 0f;
                    textUiPlayer[i].color = color;
                }
            }

        }
    }

    private void OffMainCamera() => camController.gameObject.SetActive(false);

    private IEnumerator GoToMainCamera()
    {
        onDisableInput?.Invoke(true);
        yield return new WaitForSeconds(timeForGoToMainCamera);

        Animator anim = GetComponent<Animator>();
        if (anim != null) anim.enabled = false;

        Vector3 startLocation = transform.position;
        Vector3 endLocation = camController.transform.position;
        Quaternion startRot = transform.rotation;

        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime * speed;
            float smooth = Mathf.SmoothStep(0, 1, progress);
            Vector3 interpolate = Vector3.Lerp(startLocation, endLocation, smooth);
            Quaternion interpolateRot = Quaternion.Lerp(startRot, camController.transform.rotation, progress);

            transform.position = interpolate;
            transform.rotation = interpolateRot;
            yield return null;
        }
        camController.gameObject.SetActive(true);
        gameObject.SetActive(false);
        RestorImage();
        RestorText();
        onDisableInput?.Invoke(false);
    }

    private void RestorImage()
    {
        if(imageUiPlayer == null) return;
        foreach (Image image in imageUiPlayer)
        {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
    }
    private void RestorText()
    {
        if (imageUiPlayer == null) return;
        foreach (TextMeshProUGUI text in textUiPlayer)
        {
            Color color = text.color;
            color.a = 1f;
            text.color = color;
        }
    }

    // Vecchio Sistema di Cinematic

    //private IEnumerator Camera()
    //{
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        Vector3 startLocation = transform.position;
    //        Vector3 endLocation = points[i];

    //        float progression = 0;
    //        while (progression < 1)
    //        {
    //            progression += Time.deltaTime * speed;
    //            Vector3 interpolate = Vector3.Lerp(startLocation, endLocation, progression);

    //            transform.position = interpolate;
    //            yield return null;
    //        }
    //    }

    //    Vector3 startLocation = transform.position;
    //    Vector3 endLocation = camController.transform.position;

    //    float progress = 0;
    //    while (progress < 1)
    //    {
    //        progress += Time.deltaTime * speed;
    //        float smooth = Mathf.SmoothStep(0, 1, progress);
    //        Vector3 interpolate = Vector3.Lerp(startLocation, endLocation, smooth);
    //        Quaternion interpolateRot = Quaternion.Lerp(transform.rotation, camController.transform.rotation, smooth);

    //        transform.position = interpolate;
    //        transform.rotation = interpolateRot;
    //        yield return null;
    //    }
    //    camController.gameObject.SetActive(true);
    //    gameObject.SetActive(false);
    //}
}
