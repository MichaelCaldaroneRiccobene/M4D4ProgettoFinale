using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource audioGeneral;
    [SerializeField] private AudioClip[] footstepClips;

    public void PlayFootstepPlayer()
    {
        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioGeneral.PlayOneShot(clip);
    }
}
