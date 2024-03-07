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
        AimBtn.ainBtnEven += GunAimAction;
    }

    private void GunAimAction()
    {
        allowAttack = !allowAttack;
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
        playerAnim.SetBool(CONSTANT.Reload, true);
    }
    public override void AffterReload()
    {
        base.AffterReload();
        playerAnim.SetBool(CONSTANT.Reload, false);
        playerAnim.SetBool(CONSTANT.Shoot, false);
        updateAmmo?.Invoke(presentAmuation);
    }

    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        playerAnim = transform.GetComponent<Animator>();

    }
}
