using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : GetFullComponent
{
    public bool isRun;
    public Joystick joystick;
    public CharacterController characterCtl;
    public ObjectData playerData;

    public Transform cameraTransform;
    [Header("CheckGround && jump")]
    public Vector3 velocity;
    public Transform checkSuffacePoint;
    public float gravity, checkSuffaceRadious;
    public LayerMask layerMask;

    public bool isAim;
    public bool isSprint;
    public Animator playerAnim;
    public static bool isOnSufface;

    [Header("SmoothAnim")]
    public float currentMoveStates;
    public void CallMethod()
    {
        if (isRun)
            return;
        isRun = true;
        StartCoroutine(VirutalUpdate());
    }
    private void Start()
    {
        CallMethod();
        AimBtn.ainBtnEven += PlayerAimEvent;

    }

    private void PlayerAimEvent()
    {
        if (!isAim)
        {
            isAim = true;
            playerAnim.SetBool(CONSTANT.AimMing, isAim);
            return;
        }
        isAim = false;
        playerAnim.SetBool(CONSTANT.AimMing, isAim);
    }

    IEnumerator VirutalUpdate()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        while (isRun)
        {
            CheckSufface();
            PlayerSimpleMove();
            Sprint();
            Aiming();
            yield return wait;
        }
    }
    void PlayerSimpleMove()
    {
        if (isSprint)
            return;
        float ver = joystick.Vertical;
        float hor = joystick.Horizontal;
        Vector3 dir = Vector3.right * hor + Vector3.up * 0 + Vector3.forward * ver;
        Vector3 playerDirection = GetPlayerDirection(dir);
        if (dir.magnitude > 0.1f)
        {
            characterCtl.Move(playerDirection * Time.deltaTime * playerData.moveSpeed);
            if (currentMoveStates < 0.5f)
            {// move 
                currentMoveStates = Mathf.MoveTowards(currentMoveStates, 0.5f, Time.deltaTime);
                playerAnim.SetFloat(CONSTANT.StateMove, currentMoveStates);
            }

        }
        else
        {
            if (currentMoveStates > 0.1f)
            {//idel
                currentMoveStates = Mathf.MoveTowards(currentMoveStates, 0.1f, Time.deltaTime);
                playerAnim.SetFloat(CONSTANT.StateMove, currentMoveStates);
            }
        }
    }
    void Sprint()
    {
        if (!isSprint)
            return;
        float ver = joystick.Vertical;
        float hor = joystick.Horizontal;
        Vector3 dir = Vector3.right * hor + Vector3.up * 0 + Vector3.forward * ver;
        ;
        Vector3 playerDirection = GetPlayerDirection(dir);
        if (dir.magnitude > 0.1f)
        {
            characterCtl.Move(playerDirection * Time.deltaTime * playerData.sprintSpeed);
            if (currentMoveStates < 1f)
            {// move 
                currentMoveStates = Mathf.MoveTowards(currentMoveStates, 1f, Time.deltaTime);
                playerAnim.SetFloat(CONSTANT.StateMove, currentMoveStates);
            }

        }
        else
        {
            if (currentMoveStates > 0.1f)
            {//idel
                currentMoveStates = Mathf.MoveTowards(currentMoveStates, 0.1f, Time.deltaTime);
                playerAnim.SetFloat(CONSTANT.StateMove, currentMoveStates);
            }
        }
    }
    public void jumpt()
    {
        if (!isOnSufface)
            return;
        velocity.y = Mathf.Sqrt(10f * gravity * Time.deltaTime * -2);
        playerAnim.SetTrigger(CONSTANT.Jump);

    }
    void CheckSufface()
    {
        isOnSufface = Physics.CheckSphere(checkSuffacePoint.position, checkSuffaceRadious, layerMask);
        if (!isOnSufface)
        {
            velocity.y = -2;
        }
        characterCtl.Move(velocity * Time.deltaTime);
    }
    public float RotationPlayer(Vector3 detal)
    {
        Quaternion currentRot = currentTransform.rotation;
        float targetAngle = Mathf.Atan2(detal.x, detal.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0);
        Quaternion smoothRot = Quaternion.Slerp(currentRot, rotation, Time.deltaTime / 0.1f);
        currentTransform.rotation = smoothRot;
        return targetAngle;
    }

    public Transform aimTarget;
    void Aiming()
    {
        if (!isAim)
            return;
        Vector3 worldAimTarget = aimTarget.position;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
    }
    Vector3 GetPlayerDirection(Vector3 dir)
    {

        Vector3 playerDirection = Vector3.zero;
        if (isAim)
        {
            Quaternion currentRot = currentTransform.rotation;
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0);
            playerDirection = rotation * Vector3.forward;

            return playerDirection;
        }
        if (dir.magnitude > 0.1f)
        {
            float angle = RotationPlayer(dir);
            playerDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        }
        return playerDirection;
    }
    [ContextMenu("Get all")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        playerAnim = GameObject.FindObjectOfType<Animator>();
        joystick = GameObject.FindObjectOfType<Joystick>();
        characterCtl = transform.GetComponent<CharacterController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "checkSuffacePoint")
            {
                checkSuffacePoint = transform.GetChild(i);
                break;
            }
        }
    }
}
