using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : GetFullComponent
{
    public bool isRun;
    public PlayerGunSystem gun;
    public Animator playerAnim;
    public PlayerCtl playerCtl;
    public PlayerMove playerMove;

    public void CallMethod()
    {
        if (!playerCtl.isUse)
            return;
        isRun = true;
        StartCoroutine(VirutalUpdate());
    }
    IEnumerator VirutalUpdate()
    {
        playerAnim.SetBool(CONSTANT.Shoot, true);
        WaitForFixedUpdate wait = null;
        while (isRun && !gun.isRealoadding)
        {
            gun.Shooting();

            yield return wait;
        }
        if (gun.isRealoadding)
        {
            playerAnim.SetBool("Reloading", true);
            playerMove.isRun = false;
            WaitUntil wait_until = new WaitUntil(() => !gun.isRealoadding);
            yield return wait_until;
            playerMove.CallMethod();
            playerAnim.SetBool("Reloading", false);
            if (isRun)
            {
                CallMethod();
            }
        }
        if (!isRun)
        {
          
            playerAnim.SetBool(CONSTANT.Shoot, false);
     

        }

    }
    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        playerCtl = transform.GetComponent<PlayerCtl>();
        playerAnim = transform.GetComponent<Animator>();
        playerMove = transform.GetComponent<PlayerMove>();
    }
}
