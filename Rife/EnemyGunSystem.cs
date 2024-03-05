using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunSystem : Guns
{

    public Animator enemyAnim;
    public Transform cameraTransform;
    //public EnemyCtl EnemyCtl;
    private void Start()
    {
        presentAmuation = gunData.maximumAmuation;
    }

    public override void BeforeReload()
    {
        base.BeforeReload();
        enemyAnim.SetInteger(CONSTANT.State, 0);
        enemyAnim.SetBool(CONSTANT.Shoot, false);
    }
    public override void AffterReload()
    {
        base.AffterReload();
        enemyAnim.SetInteger(CONSTANT.State, 0);
        enemyAnim.SetBool(CONSTANT.Shoot, false);
    }

    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        enemyAnim = transform.GetComponent<Animator>();
        //EnemyCtl = GameObject.FindObjectOfType<EnemyCtl>();
    }
}
