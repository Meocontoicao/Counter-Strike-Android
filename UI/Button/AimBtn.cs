using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AimBtn : ButtonCtl
{
    public static event Action ainBtnEven;
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        ainBtnEven?.Invoke();
    }
    [ContextMenu("Get all")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
    }
}
