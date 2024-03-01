
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Move")]
    public float playerSpeed;
    public float sprintSpeed;
    public Transform playerTransform;

    [Header("Player animator && gravity ")]
    public float gravity;
    public CharacterController characterCtl;

    [Header("Player Camera")]
    public Transform cameraTransform;

    [Header("Player jump & velocity")]
    public float turnCalmTime = 0.1f;
    bool onSufaces;
    float turnClamVelocity;
    public Transform sufaceCheck;
    public float sufaceDistances = 0.4f;
    public LayerMask layerMask;
    Vector3 velocity;
    public float jumpRange;
    public bool isSprint;

    public Animator playerAnimator;

    // Update is called once per frame
    void Update()
    {
        ChechOnSuface();
        if (!isSprint)
        {
            PlayerMove();

        }
        else
        {
            Sprint();
        }

        PlayerJump();
    }

    #region Player Handle
    void PlayerMove()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector3 dir = Vector3.right * hor + Vector3.up * 0 + Vector3.forward * ver;
        Vector3 playerDirection = playerTransform.forward * Time.deltaTime * playerSpeed;
        dir.Normalize();
        if (dir.magnitude > 0.1f)
        {
            RotationPlayer(dir);
            characterCtl.Move(playerDirection);
            playerAnimator.SetInteger(CONSTANT.State, 1);
        }
        else if (dir.magnitude == 0)
        {
            if (characterCtl.velocity.magnitude > 0)
            {
                characterCtl.Move(Vector3.zero);
            }
            playerAnimator.SetInteger(CONSTANT.State, 0);
        }


    }

    public void RotationPlayer(Vector3 detal)
    {
        Quaternion currentRot = playerTransform.rotation;
        float targetAngle = Mathf.Atan2(detal.x, detal.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0);
        Quaternion smoothRot = Quaternion.Slerp(currentRot, rotation, Time.deltaTime / 0.1f);
        playerTransform.rotation = smoothRot;
    }
    void ChechOnSuface()
    {
        onSufaces = Physics.CheckSphere(sufaceCheck.position, sufaceDistances, layerMask);
        if (!onSufaces)
        {
            velocity.y = -2;
        }
        velocity.y += gravity * Time.deltaTime;
        characterCtl.Move(velocity * Time.deltaTime);

    }
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onSufaces)
        {
            velocity.y = Mathf.Sqrt(jumpRange * Time.deltaTime * gravity * -2);
            playerAnimator.SetInteger(CONSTANT.State, 3);
        }

    }
    void Sprint()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector3 dir = Vector3.right * hor + Vector3.up * 0 + Vector3.forward * ver;
        Vector3 playerDirection = playerTransform.forward * sprintSpeed * Time.deltaTime * playerSpeed;
        dir.Normalize();
        if (dir.magnitude > 0.1f)
        {
            RotationPlayer(dir);
            characterCtl.Move(playerDirection);
            playerAnimator.SetInteger(CONSTANT.State, 2);
        }
        else if (dir.magnitude == 0)
        {
            if (characterCtl.velocity.magnitude > 0)
            {
                characterCtl.Move(Vector3.zero);

            }
            playerAnimator.SetInteger(CONSTANT.State, 0);
        }


    }
    #endregion

    [ContextMenu("Get compos")]
    private void GetAllComponent()
    {
        playerAnimator = transform.GetComponent<Animator>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        characterCtl = gameObject.GetComponent<CharacterController>();
        playerTransform = transform;
    }
}
