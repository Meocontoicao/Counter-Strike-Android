using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerSound : GetFullComponent
{
    [Header("Foot step handle")]
    public AudioClip[] audioClip; // just use for step
    public AudioSource audioSources;

    public void FootStep()
    {
        int random = Random.Range(0, audioClip.Length - 1);
        audioSources.PlayOneShot(audioClip[random]);
    }

    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        audioSources = transform.GetComponent<AudioSource>();
    }
}
