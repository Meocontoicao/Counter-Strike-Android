using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UnitCtl : Objects
{
    public Animator animator;
    public UnitMove enemyMove;
    public string objectLayerName;

    public bool isEnemy;
    public static event Action addEnemyUnit, reducEnemyUnit;
    public static event Action addPlayerUnit, reducPlayerUnit;
    protected override void OnEnable()
    {
        base.OnEnable();
        currentGameObject.layer = LayerMask.NameToLayer(objectLayerName);
        if (isEnemy)
        {
            addEnemyUnit?.Invoke();
        }
        else
        {
            addPlayerUnit?.Invoke();
        }
    }
    public override void ObjectDie()
    {
        base.ObjectDie();
        enemyMove.isRun = false;
        animator.SetBool(CONSTANT.Death, true);
        currentGameObject.layer = 14;
        if (isEnemy)
        {
            reducEnemyUnit?.Invoke();
        }
        else
        {
            reducPlayerUnit?.Invoke();
        }
    }
    [ContextMenu("GetAll")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        animator = transform.GetComponent<Animator>();
        enemyMove = transform.GetComponent<UnitMove>();
    }
}
