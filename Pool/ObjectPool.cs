using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> : MonoBehaviour, IObjectPool<T> where T : class
{
    public T refab;
    public List<T> pool;
    public virtual T GetObjectFromPool()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ReturnPool(T objects)
    {
        throw new System.NotImplementedException();
    }

    public virtual T SpawObject()
    {
        throw new System.NotImplementedException();
    }
}
