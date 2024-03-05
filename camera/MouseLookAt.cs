using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseLookAt : GetFullComponent
{
    public Joystick lookJoyStick;
    public Joystick moveJoyStick;
    private Vector2 previousDetal = Vector2.zero;
    public float rotateSpeed = 5f; // Tốc độ quay của camera
    public float swipeThreshold = 50f; // Ngưỡng để xác định xem vuốt là vuốt ngang hay vuốt dọc
    public float smoothTime = 0.3f; // Thời gian mượt

    float currentVelocity;

    public Transform target;
    float newAngle;
    [Header("Speed To rotation Camera")]
    public float maxSpeedHorizontalRotaion = 5;
    public float minSpeedHorizontalRotation = 0.3f;
    public float currentHorizontalSpeed;

    public Vector3 offset;
    public float  currentX =0;
    public float currentY = 0;
    public float currentSensity = 4f;

    private void LateUpdate()
    {
        SetMousePostion();
        GetPostion();
        Vector2 input = GetInputValue();
        Vector2 direction = input - previousDetal;
        float targetAngle = transform.eulerAngles.y + direction.x;
        // Mềm mại hóa góc quay của camera
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        // Áp dụng góc quay mới cho camera
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
      
        transform.rotation = rotation;
        transform.position = target.position -( rotation * offset);
     
        previousDetal = input;
    }
    int touchNumber;
    Vector2 position_1;
    Vector2 position_2;
    #region Check number point 
    public Vector2 GetInputValue()
    {
        int vertical = 0;
        int horizontal = 0;
        if (lookJoyStick.Horizontal != 0)
        {
            horizontal = 1;
        }
        if (lookJoyStick.Vertical != 0)
        {
            vertical = 1;
        }

        Vector2 input = Vector3.right * horizontal * GetPostion().x + Vector3.up * vertical * GetPostion().y;
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
    // the distances of touch is more long will be have speed morer small
    public float GetHorizontalSpeed(float x)
    {
        float speed = maxSpeedHorizontalRotaion;
        if (x != 0)
        {
            speed = maxSpeedHorizontalRotaion / (x * Time.deltaTime);
        }
        speed = Mathf.Abs(speed);
        speed = Mathf.Clamp(speed, minSpeedHorizontalRotation, maxSpeedHorizontalRotaion);
  
        return speed;
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
