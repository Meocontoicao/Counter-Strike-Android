using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : PoolAbtract<BulletCtl>
{
    private static BulletPooling _instances;
    public static BulletPooling Instances => _instances;

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
    public override BulletCtl GetObjectFromPool(int indext)
    {

        BulletCtl bulletCtl = null;
        for (int i = 0; i < listPool.Count; i++)
        {
            if (listPool[i].objectIndext == indext)
            {
                if (!listPool[i].isUse)
                {
                    bulletCtl = listPool[i];
                    break;

                }
            }
        }

        return bulletCtl;
    }

    public override BulletCtl GetObjectFromRefab(int indext)
    {
        BulletCtl bulletCtl = null;
        for (int i = 0; i < listRefabs.Count; i++)
        {
            if (listRefabs[i].objectIndext == indext)
            {
                bulletCtl = listRefabs[i];
                break;
            }
        }
        return bulletCtl;
    }

    public override BulletCtl SpawObject(int indext , Vector3 position ,Quaternion rotation)
    {
        BulletCtl bulletCtl = GetObjectFromPool(indext);
        if (bulletCtl == null)
        {
            bulletCtl = Instantiate(GetObjectFromRefab(indext), position, rotation);
            listPool.Add(bulletCtl);
        }
        bulletCtl.isUse = true;
        bulletCtl.transform.parent = currentTransform;
        bulletCtl.transform.position = position;
        bulletCtl.transform.rotation = rotation;  
        bulletCtl.gameObject.SetActive(true);
        return bulletCtl;
    }
    
    [ContextMenu("Get all Compps")]
    protected override void GetAllCom()
    {
        base.GetAllCom();
    }

    public override void DespawObject(BulletCtl bulletCtl)
    {
        bulletCtl.isUse = false;
        bulletCtl.gameObject.SetActive(false);
    }
}
