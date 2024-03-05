using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtl : Objects
{
    public float lifeTime;

 
    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
    
    }
    public void CallDespaw()
    {
        Invoke("DespawSelf", lifeTime);
    }
    protected override void DespawSelf()    
    {
        base.DespawSelf();
        EffectPool.Instances.DespawObject(this);
        
    }

}
