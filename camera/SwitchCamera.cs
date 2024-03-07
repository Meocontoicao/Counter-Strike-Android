using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : GetFullComponent
{

    public GameObject aimCrossHair;
    public GameObject tpsCrossHair;

    public GameObject aimCamera;
    public GameObject tpsCamera;

    private void OnEnable() => AimBtn.ainBtnEven += SwithCamera;

    private void SwithCamera()
    {
         if (!aimCamera.activeSelf)
        {
            aimCamera.gameObject.SetActive(true) ;
            tpsCrossHair.gameObject.SetActive(false);
            aimCrossHair.gameObject.SetActive(true);
            tpsCamera.gameObject.SetActive(false);
            return;
        }
        aimCamera.gameObject.SetActive(false);
        tpsCrossHair.gameObject.SetActive(true);
        aimCrossHair.gameObject.SetActive(false);
        tpsCamera.gameObject.SetActive(true);
    }

    [ContextMenu("Get all")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
   
    }

}
