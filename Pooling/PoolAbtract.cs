using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolAbtract<T> : MonoBehaviour
{
    public List<T> listPool;
    public List<T> listRefabs;

    public Transform currentTransform;

    public abstract T SpawObject(int indext , Vector3 position , Quaternion rotation);
    public abstract T GetObjectFromPool(int indext);
    public abstract T GetObjectFromRefab(int indext);
    public abstract void DespawObject(T obj);
 
  
    protected virtual void GetAllCom()
    {
        currentTransform = transform;
    }
}

