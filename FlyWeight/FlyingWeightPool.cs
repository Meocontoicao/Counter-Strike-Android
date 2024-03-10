using System.Collections.Generic;
using UnityEngine;

public class FlyingWeightPool :  ObjectPool<FlyWeight> 
{
    public override FlyWeight SpawObject()
    {
        FlyWeight flyHeight = GetObjectFromPool();

        if (flyHeight == null)
        {
            flyHeight = Instantiate(refab);
            flyHeight.transform.parent = transform;
        }
        flyHeight.isUse = true;
        flyHeight.gameObject.SetActive(false);
        return flyHeight;
    }
    public override void ReturnPool(FlyWeight objects)
    {
        objects.isUse = false;
        objects.gameObject.SetActive(false);
    }
    public override FlyWeight GetObjectFromPool()
    {
        FlyWeight flyHeight = null;
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].isUse )
            {
                flyHeight = pool[i];
            }
        }

        return flyHeight;
    }
}