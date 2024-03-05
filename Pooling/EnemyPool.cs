using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool :PoolAbtract<UnitCtl>
{
    //private static EnemyPool _instances;
    //public static EnemyPool Instances => _instances;

    public SpawPoint spawObject;

    //private void Awake()
    //{
    //    if (_instances != null && _instances != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        _instances = this;
    //    }

    
    //}

    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            Transform newStransform = spawObject.GetSpawPoint();
            SpawObject(0, newStransform.position, newStransform.rotation);
        }
    }

    public override void DespawObject(UnitCtl obj)
    {
        obj.isUse = false;
        obj.gameObject.SetActive(false);
    }

    public override UnitCtl GetObjectFromPool(int indext)
    {
        UnitCtl enemyCtl = null;
        for (int i = 0; i < listPool.Count; i++)
        {
            if (listPool[i].objectIndext == indext)
            {
                if (!listPool[i].isUse)
                {
                    enemyCtl = listPool[i];
                    break;

                }
            }
        }

        return enemyCtl;
    }

    public override UnitCtl GetObjectFromRefab(int indext)
    {
        UnitCtl enemyCtl = null;
        for (int i = 0; i < listRefabs.Count; i++)
        {
            if (listRefabs[i].objectIndext == indext)
            {
                enemyCtl = listRefabs[i];
                break;
            }
        }
        return enemyCtl;
    }

    public override UnitCtl SpawObject(int indext, Vector3 position, Quaternion rotation)
    {
        UnitCtl enemyCtl = GetObjectFromPool(indext);
        if (enemyCtl == null)
        {
            enemyCtl = Instantiate(GetObjectFromRefab(indext), position, rotation);
            listPool.Add(enemyCtl);
        }
        enemyCtl.isUse = true;
        enemyCtl.transform.parent = currentTransform;
        enemyCtl.transform.position = position;
        enemyCtl.transform.localRotation = rotation;
        enemyCtl.gameObject.SetActive(true);
        return enemyCtl;
    }
}
