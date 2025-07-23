using System.Collections;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [Header("Setting Animation Coin")]
    [SerializeField] private Transform refCoin;
    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float timeInterpolateMinScale = 1.5f;

    public void AnimationTakeCoin() => StartCoroutine(OnAnimationTakeCoin());

    private IEnumerator OnAnimationTakeCoin()
    {
        Vector3 oriScale = refCoin.transform.localScale;
        Vector3 minimalScale = new Vector3(minScale, minScale, minScale);

        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime * timeInterpolateMinScale;
            float smooth = Mathf.SmoothStep(0,1,progress);

            refCoin.transform.localScale = Vector3.Lerp(oriScale, minimalScale, smooth);
            yield return null;
        }

        Destroy(gameObject);
    }
}
