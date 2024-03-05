using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : GetFullComponent
{ // just use with object have Data
    public bool isUse;
    public int objectIndext;
    public ObjectData objectData; // have or not 

    [Header("Health")]
    public int currentHp;
    public int currentMount;
    [Header("Target")]
    public Transform targetPoint;
    protected virtual void OnEnable()
    {
        isUse = true;
    }
    protected virtual void Start()
    {
        if (objectData == null)
            return;
        currentHp = objectData.health;
        currentMount = objectData.amount;
    }
    protected virtual void DespawSelf()
    {
         // to respawObject if have
    }

    public virtual void DeductVitality(int damgeRecive)
    {
        if (!isUse)
            return;
        int realDamge = DeductAmount(damgeRecive);
        currentHp -= realDamge;
        if (currentHp <= 0)
        {
            currentHp = 0;
            isUse = false;
            ObjectDie();
        }
    }
    public virtual int DeductAmount(int damgeRecive)
    {
        int realDamge = damgeRecive - currentMount;
        currentMount -= damgeRecive;
        if (currentMount <= 0)
        { currentMount = 0; }

        return realDamge;
    }
    public virtual void ObjectDie()
    {
        // to overrid
        isUse = false;
    }
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "targetPoint")
            {
                targetPoint = transform.GetChild(i);
                break;
            }
        }
    }
}
