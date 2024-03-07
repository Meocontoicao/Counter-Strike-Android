using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCameraCtl : GetFullComponent
{


    public Joystick lookJoyStick;
    public Joystick moveJoyStick;

    public float cinemachineTargetYaw;
    public float cinemachineTargetPitch;

    public Transform cinemachineTarget;
    // Update is called once per frame
    void LateUpdate()
    {
        cinemachineTargetYaw += lookJoyStick.Horizontal * Time.deltaTime;
        cinemachineTargetPitch += lookJoyStick.Vertical * Time.deltaTime;
    }

    [ContextMenu("Get all compose")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        Joystick[] arr =
        GameObject.FindObjectsOfType<Joystick>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].name == "lookJoyStick")
            {
                lookJoyStick = arr[i];
            }
            if (arr[i].name == "moveJoyStick")
            {
                moveJoyStick = arr[i];
            }
        }
    }

}
