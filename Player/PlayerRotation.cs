using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : GetFullComponent
{
    public Transform target;

    private void Update()
    {
    

        transform.LookAt(target);
    }
    [ContextMenu("Get all compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
    }
}
