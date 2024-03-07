using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinButton : ButtonCtl
{
    public PlayerMove playerMove;
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        playerMove.isSprint = !playerMove.isSprint;


    }
    [ContextMenu("Get all compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        playerMove = GameObject.FindObjectOfType<PlayerMove>();
    }
}
