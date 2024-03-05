using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Guns : GetFullComponent
{
    public GunData gunData;

    [Header("Gun attack")]
    public bool canAttack = true;
    public Transform attackPoint;
    public Animator currentAnim;

    public int presentAmuation;
    public bool allowAttack = false;
    public bool isRealoadding = false;

    public ParticleSystem currentPartical;
    protected virtual void Awake()
    {
        presentAmuation = gunData.maximumAmuation;
    }

    public virtual void Shooting()
    {

        if (allowAttack && canAttack && !isRealoadding)
        {

            canAttack = false;
            currentPartical.Play();
            presentAmuation--;
            SoundManager.Instance.PlaySoundEffectByAudioVsDistances(gunData.audioClipName,currentTransform);
            BulletCtl bulletCtl = BulletPooling.Instances.SpawObject(0, attackPoint.position, attackPoint.rotation);
            bulletCtl.reciveDamage = gunData.giveDamage;
            bulletCtl.BulletFly(attackPoint.forward);
            StartCoroutine(waitToAttack());
            if (presentAmuation <= 0)
            {
                allowAttack = false;
                isRealoadding = true;
                StartCoroutine(ReLoading());
            }
        }
    }
    public void StopShooting()
    {
        if (!allowAttack)
            return;
        currentPartical.Stop();
        allowAttack = false;

    }
    public void Fire()
    {
        allowAttack = true;
    }
    public void UnFire()
    {
        allowAttack = false;
    }
    IEnumerator waitToAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(1 / gunData.fireCharge);
        yield return wait;
        canAttack = true;
    }

    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        currentAnim = transform.GetComponent<Animator>();
        currentPartical = transform.GetComponentInChildren<ParticleSystem>();

    }
    // whent reload player can not move

    public virtual void BeforeReload()
    {

    }
    public virtual void AffterReload()
    {

    }
    IEnumerator ReLoading()
    {

        WaitForSeconds wait = new WaitForSeconds(gunData.realoadingTime);
        yield return wait;
        isRealoadding = false;
        presentAmuation = gunData.maximumAmuation;
        AffterReload();

    }
}
