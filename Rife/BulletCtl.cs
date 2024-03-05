using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtl : Objects
{
    [Header("Physich && interaction")]
    public Rigidbody rb;
    public float moveSpeed;
    public int reciveDamage;
    [Header("Despaw")]
    public float lifeTime;
    public Transform spawEffectPoint;
    public void BulletFly(Vector3 direction)
    {
        rb.AddForce(direction * moveSpeed, ForceMode.Impulse);
        Invoke("DespawSelf", lifeTime);
    }
    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        rb = transform.GetComponent<Rigidbody>();  
    }
    protected override void DespawSelf()
    {
        base.DespawSelf();
        rb.velocity = Vector3.zero;
        BulletPooling.Instances.DespawObject(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Emviroment"))
        {
            EffectCtl effectCtl =
               EffectPool.Instances.SpawObject(1, spawEffectPoint.position, currentTransform.rotation);
            effectCtl.CallDespaw();
            DespawSelf();

        }
        if (collision.gameObject.CompareTag("player") || collision.gameObject.CompareTag("enemy"))
        {
            EffectCtl effectCtl =
                  EffectPool.Instances.SpawObject(2, spawEffectPoint.position, currentTransform.rotation);

            Objects newObj = collision.gameObject.GetComponent<Objects>();
            newObj.DeductVitality(reciveDamage);
            effectCtl.CallDespaw();
            DespawSelf();
        }
       
    }

}
