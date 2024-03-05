using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : GetFullComponent
{
    bool isAttack;
    public PlayerGunSystem gun;
    public PlayerAttack playerAttack;
    public void OnPointDown()
    {
        if (!isAttack)
        {
            playerAttack.CallMethod();
        }
    }
    public void OnPointUp()
    {
        isAttack = false;
        playerAttack.isRun = false;
    }
 
    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        gun = GameObject.FindObjectOfType<PlayerGunSystem>();
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
    }
}
