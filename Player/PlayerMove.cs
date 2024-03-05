using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : GetFullComponent
{
    public  bool isRun;
    public Joystick joystick;
    public CharacterController characterCtl;
    public ObjectData playerData;

    public Transform cameraTransform;
    [Header("CheckGround && jump")]
    public Vector3 velocity;
    public Transform checkSuffacePoint;
    public float gravity, checkSuffaceRadious;
    public LayerMask layerMask;

    public Animator playerAnim;
    public bool isSprint;
    public static bool isOnSufface;
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
    }
    IEnumerator VirutalUpdate()
    {
        WaitForFixedUpdate wait = null;
        while (isRun)
        {
            CheckSufface();
            PlayerSimpleMove();
            Sprint();
            jumpt();
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
        Vector3 playerDirection = currentTransform.forward * Time.deltaTime * playerData.moveSpeed;
        dir.Normalize();
        if (dir.magnitude > 0.1f)
        {
            RotationPlayer(dir);
            characterCtl.Move(playerDirection);
            playerAnim.SetInteger(CONSTANT.State, 1);
        }
        else
        {
            playerAnim.SetInteger(CONSTANT.State, 0);
        }
    }
    void Sprint()
    {
        if (!isSprint)
            return;
        float ver = joystick.Vertical;
        float hor = joystick.Horizontal;
        Vector3 dir = Vector3.right * hor + Vector3.up * 0 + Vector3.forward * ver;
        Vector3 playerDirection = currentTransform.forward * Time.deltaTime * playerData.sprintSpeed;
        dir.Normalize();
        if (dir.magnitude > 0.1f)
        {
            RotationPlayer(dir);
            characterCtl.Move(playerDirection);
            playerAnim.SetInteger(CONSTANT.State, 2);
        }
        else
        {
            playerAnim.SetInteger(CONSTANT.State, 0);
        }
    }
    void jumpt()
    {
        if (!isOnSufface)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(10f * gravity * Time.deltaTime * -2);
            playerAnim.SetTrigger(CONSTANT.Jump);
        }
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
    public void RotationPlayer(Vector3 detal)
    {
        Quaternion currentRot = currentTransform.rotation;
        float targetAngle = Mathf.Atan2(detal.x, detal.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0);
        Quaternion smoothRot = Quaternion.Slerp(currentRot, rotation, Time.deltaTime / 0.1f);
        currentTransform.rotation = smoothRot;
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
