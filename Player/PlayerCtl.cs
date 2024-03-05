using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerCtl : Objects
{
    public PlayerMove playerMove;
    public PlayerAttack playerAttack;
    public Animator playerAnimator;

    public static event Action<int> playerHealthUpdateUI;
    public override void ObjectDie()
    {
        base.ObjectDie();
        playerMove.isRun = false;
        playerAttack.isRun = false;
        playerAnimator.SetBool(CONSTANT.Death, true) ;

    }

    public override void DeductVitality(int damgeRecive)
    {
       
        base.DeductVitality(damgeRecive);
        if (!isUse)
            return;
        playerHealthUpdateUI?.Invoke(damgeRecive);
    }
    [ContextMenu("Get all compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        playerAnimator = transform.GetComponent<Animator>();
        playerMove = transform.GetComponent<PlayerMove>();
        playerAttack = transform.GetComponent<PlayerAttack>();
    }
}
