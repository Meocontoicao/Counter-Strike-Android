using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSound : GetFullComponent
{
    [Header("Foot step handle")]
    public AudioClip[] audioClip; // just use for step
    public SoundManager soundManager;
    public void FootStep()
    {
        int random = Random.Range(0, audioClip.Length - 1);
        soundManager.PlaySoundEffectByAudioVsDistances(audioClip[random], currentTransform);
    }

    [ContextMenu("Get all Compose")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
    }
}
