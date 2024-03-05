using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : GetFullComponent
{
    public Animator animator;
    public UnitMove unitMove;
    public EnemyGunSystem enemyGunSystem;
    private void OnEnable() => unitMove.enemyAttackAction += EnemyActionAttack;

    private void EnemyActionAttack()
    {
        enemyGunSystem.Shooting();
    }
    [ContextMenu("Get all compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        animator = transform.GetComponent<Animator>();
        unitMove = transform.GetComponent<UnitMove>();
        enemyGunSystem = transform.GetComponent<EnemyGunSystem>();
    }
}
