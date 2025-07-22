using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLevel : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform bigWall;
    [SerializeField] private float newHeightWall;
    [SerializeField] private float speedLerp = 1;

    public UnityEvent onWin;

    private bool canFinishLevel;
    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player = other.GetComponent<Player_Controller>();
        if (player != null && canFinishLevel) onWin?.Invoke();
    }

    public void OnFinishLevel()
    {
        canFinishLevel = true;
        Vector3 oriPos = bigWall.position;
        oriPos.y += newHeightWall;

        StartCoroutine(WallUpAnimation(oriPos));
    }

    private IEnumerator WallUpAnimation(Vector3 newPos)
    {
        Vector3 startPos = bigWall.position;
        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime * speedLerp;
            bigWall.position = Vector3.Lerp(startPos, newPos, progress);

            yield return null;
        }
    }
}
