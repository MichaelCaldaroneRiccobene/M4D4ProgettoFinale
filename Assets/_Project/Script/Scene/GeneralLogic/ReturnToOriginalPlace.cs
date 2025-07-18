using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReturnToOriginalPlace : MonoBehaviour, I_Touch_Water
{
    [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject poofVfx;

    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startScale;
    private ParticleSystem vfx;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;

        if(poofVfx != null )
        {
            GameObject particol = Instantiate(poofVfx, transform.position, Quaternion.identity, mesh.transform);
            vfx = particol.GetComponent<ParticleSystem>();
        }
    }

    public void Water()
    {
        StartCoroutine(GoStartLocation());
    }

    IEnumerator GoStartLocation()
    {
        Vector3 currentPos = transform.position;
        Vector3 endPos = startPos;
        Quaternion currentRot = transform.rotation;

        

        endPos.y += 10;
        rb.isKinematic = true;
        mesh.gameObject.SetActive(false);

        float progress = 0;

        while(progress < 1)
        {
            progress += Time.deltaTime * 2;
            Vector3 newPos = Vector3.Lerp(currentPos, endPos, progress);
            Quaternion newRot = Quaternion.Lerp(currentRot,startRot, progress);

            transform.position = newPos;
            transform.rotation = newRot;
            yield return null;
        }

        transform.localScale = startScale;
        rb.isKinematic = false;
        mesh.gameObject.SetActive(true);
        PlayPoofVFX();
    }

    private void PlayPoofVFX()
    {
        if(vfx == null) return;

        vfx.Play();
    }
}
