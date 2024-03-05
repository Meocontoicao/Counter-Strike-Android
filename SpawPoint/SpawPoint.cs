using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawPoint : GetFullComponent
{
    public int currentIndext =0;
    public List<Transform> listSpawPoint;

    public Transform GetSpawPoint()
    {
        if (currentIndext > listSpawPoint.Count -1)
        {
            currentIndext = 0;
        }
        Transform spawPoint = listSpawPoint[currentIndext];
        currentIndext++;
        return spawPoint;
    }
    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        for (int i = 0; i < transform.childCount; i++)
        {
            listSpawPoint.Add(transform.GetChild(i));
        }
    }
}
