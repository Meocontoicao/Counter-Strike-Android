using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseLookAt : GetFullComponent
{
    public Joystick lookJoyStick;
    public Joystick moveJoyStick;
    private Vector2 previousDetal = Vector2.zero;
    public float currentTime = 0;
    public float values;
    public Transform playerTransform;
    public CinemachineInputProvider inputProvider;

    public bool isRun;

    private void OnEnable()
    {
        CallMethod();
    }
    private void OnDisable() => isRun = false;

    public void CallMethod()
    {
        isRun = true;
        StartCoroutine(VirtualUpdate());

    }
    IEnumerator VirtualUpdate()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while(isRun)
        {
            SetMousePostion();
            GetPostion();
            Vector2 input = GetInputValue();
            Vector2 direction = input - previousDetal;

            previousDetal = input;
            if (direction.magnitude == 0)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
            }
            if (currentTime < 0.2f)
            {
                if (!inputProvider.enabled)
                {
                    inputProvider.enabled = true;
                }
            }
            else if (currentTime > 0.2f)
            {
                if (inputProvider.enabled)
                {
                    inputProvider.enabled = false;
                }
            }
            yield return wait;
        }
      
    }


    public void AimRotationCamera(Vector2 direction)
    {
        if (direction.magnitude == 0)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
        }
        transform.Rotate(Vector3.up);

    }


    #region Check number point 
    [Header("Touch handle")]
    int touchNumber;
    Vector2 position_1;
    Vector2 position_2;
    public Vector2 GetInputValue()
    {

        Vector2 input = Vector3.right * GetPostion().x + Vector3.up * GetPostion().y;
        return input;

    }
    public void SetMousePostion()
    {

        if (Input.touchCount == 0)
        {
            touchNumber = 0;
            return;
        }
        if (Input.touchCount > 0)
        {
            touchNumber = 1;
            position_1 = Input.GetTouch(0).position;
        }
        if (Input.touchCount == 2)
        {
            touchNumber = 2;
            position_2 = Input.GetTouch(1).position;
        }
    }
    public Vector2 GetPostion()
    {
        Vector2 joyStick = Vector2.right * moveJoyStick.Horizontal * Vector2.up * moveJoyStick.Vertical;
        if (touchNumber == 1 && joyStick.magnitude == 0)
        {
            return position_1;
        }
        else
            if (touchNumber == 1 && joyStick.magnitude != 0)
        {
            return Vector2.zero;
        }
        return (position_1.x > position_2.x) ? position_1 : position_2;
    }

    #endregion

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
