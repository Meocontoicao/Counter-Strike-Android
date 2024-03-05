using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GetFullComponent : MonoBehaviour
{
    public Transform currentTransform;
    public GameObject currentGameObject;
    protected virtual void GetAllCompos()
    {
        // to overrider and get all compos
        currentTransform = transform;
        currentGameObject = gameObject;
    }
}
