using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool canAttack;
    public float giveDamage;
    public Transform attackPoint;

    public float timeAllowAttack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Fire();
        }
    }
    public void Fire()
    {
        if (canAttack)
        {
            canAttack = false;
            Invoke("AllowTack", timeAllowAttack);
        }
    }
    public void AllowTack()
    {
        canAttack = true;
    }
}
