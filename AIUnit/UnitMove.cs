using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class UnitMove : GetFullComponent
{
    public bool isRun;
    public UnitCtl unitCtl;
    public ObjectData EnemyData;

    public bool isFollow; //  if  have target
    public Animator enemyAnimator;
    public NavMeshAgent navMeshAgent;
    [Header("indentify target")]
    public bool isPlayerInView;
    public bool isPlayerAIInView;
    public LayerMask playerLayer;

    public Objects targetObject;

    public event Action enemyAttackAction;
    public void CallMethod()
    {
        isRun = true;
        StartCoroutine(VirutalUpdate());
    }
    private void Start()
    {
        isFollow = false;
   
        navMeshAgent.stoppingDistance = EnemyData.shootingRadious - 0.5f; // to sure target in shooting range
        enemyAnimator.applyRootMotion = true;
    }

    private void OnEnable()
    {
        CallMethod(); 
    }
    IEnumerator VirutalUpdate()
    {
        WaitForFixedUpdate wait = null;
        while (isRun)
        {
            EnemyParTrol();
            EnemyPurcharPlayer();
            EnemyPurCharPlayerAi();
            yield return wait;
        }
    }

    // to combine animation with ai navmesh
    private void OnAnimatorMove()
    {
        // Nếu Animator đang chạy animation
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            // Lấy giá trị di chuyển từ Root Motion của animation
            Vector3 rootMotionDeltaPosition = enemyAnimator.deltaPosition;
            rootMotionDeltaPosition.y = currentTransform.position.y;
            Quaternion rootMotionDeltaRotation = enemyAnimator.deltaRotation;
            // Áp dụng di chuyển từ Root Motion vào NavMeshAgent
            navMeshAgent.Move(rootMotionDeltaPosition);
            // Cập nhật hướng của nhân vật
            currentTransform.rotation *= rootMotionDeltaRotation;
        }
    }
    void EnemyParTrol()
    {
        if (isFollow)
            return;
        enemyAnimator.SetInteger(CONSTANT.State, 0);
    }
    void EnemyPurcharPlayer()
    {
        isPlayerInView = Physics.CheckSphere(currentTransform.position, EnemyData.visionRadious, playerLayer);
        if (isPlayerInView && !isFollow) // check enemy or partner of enemy
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, EnemyData.visionRadious, playerLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                Objects newObject = colliders[i].GetComponent<Objects>();
                if (newObject != null)
                {
                    if (newObject.isUse)
                    {
                        targetObject = newObject;
                        isFollow = true;
                        break;
                    }

                }
            }

        }
        if (!isFollow)
            return;
        if (navMeshAgent.SetDestination(targetObject.transform.position))
        {
            enemyAnimator.SetInteger(CONSTANT.State, 1);
            enemyAnimator.SetBool(CONSTANT.Shoot, false);
        }
        float distances = (targetObject.transform.position - currentTransform.position).magnitude;
        if (distances <= navMeshAgent.stoppingDistance)
        {
            // chuyen sang trang thai tan cong 
            enemyAnimator.SetBool(CONSTANT.Shoot,true);
            enemyAttackAction?.Invoke();
            currentTransform.LookAt(targetObject.transform.position, Vector3.up);
            if (!targetObject.isUse)
            {
                isFollow = false;
                enemyAnimator.SetBool(CONSTANT.Shoot, false);
            }
        }
    }
    void EnemyPurCharPlayerAi()
    {

    }
    [ContextMenu("Get all compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        unitCtl = transform.GetComponent<UnitCtl>();
        enemyAnimator = transform.GetComponent<Animator>();
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }
}
