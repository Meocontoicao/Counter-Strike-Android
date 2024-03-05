using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerGunSystem : Guns
{
    public Animator playerAnim;
    public Transform cameraTransform;
    public static event Action <int>updateAmmo;
    //public PlayerScript playerScript;
    private void Start()
    {
        updateAmmo?.Invoke(presentAmuation);
    }

    public override void Shooting()
    {
        base.Shooting();
        // update for ui
        updateAmmo?.Invoke(presentAmuation);
    }

    public override void BeforeReload()
    {
        base.BeforeReload();
        //playerScript.isRun = false;
        playerAnim.SetBool("Reloading", true);
    }
    public override void AffterReload()
    {
        base.AffterReload();
        playerAnim.SetBool("Reloading", false);
        playerAnim.SetBool(CONSTANT.Shoot, false);
        updateAmmo?.Invoke(presentAmuation);
        //playerScript.CallVirtualMethod();
    }

    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        playerAnim = transform.GetComponent<Animator>();
        //playerScript = GameObject.FindObjectOfType<PlayerScript>();
    }
}
