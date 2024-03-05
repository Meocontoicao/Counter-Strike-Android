using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : PoolAbtract<EffectCtl>
{
    private static EffectPool _instances;
    public static EffectPool Instances => _instances;

    private void Awake()
    {
        if (_instances != null && _instances != this)
        {
            Destroy(this);
        }
        else
        {
            _instances = this;
        }
    }



    [ContextMenu("Get all Compps")]
    protected override void GetAllCom()
    {
        base.GetAllCom();
    }

    public override EffectCtl SpawObject(int indext, Vector3 position, Quaternion rotation)
    {
        EffectCtl effectCtl = GetObjectFromPool(indext);
        if (effectCtl == null)
        {
            effectCtl = Instantiate(GetObjectFromRefab(indext), position, rotation);
            listPool.Add(effectCtl);
        }
        effectCtl.isUse = true;
        effectCtl.transform.parent = currentTransform;
        effectCtl.transform.position = position;
        effectCtl.transform.localRotation = rotation;
        effectCtl.gameObject.SetActive(true);
        return effectCtl;
    }

    public override EffectCtl GetObjectFromPool(int indext)
    {
        EffectCtl effectCtl = null;
        for (int i = 0; i < listPool.Count; i++)
        {
            if (listPool[i].objectIndext == indext)
            {
                if (!listPool[i].isUse)
                {
                    effectCtl = listPool[i];
                    break;

                }
            }
        }

        return effectCtl;
    }

    public override EffectCtl GetObjectFromRefab(int indext)
    {
        EffectCtl effectCtl = null;
        for (int i = 0; i < listRefabs.Count; i++)
        {
            if (listRefabs[i].objectIndext == indext)
            {
                effectCtl = listRefabs[i];
                break;
            }
        }
        return effectCtl; 
    }

    public override void DespawObject(EffectCtl obj)
    {
        obj.isUse = false;
        obj.gameObject.SetActive(false);
    }
}
