using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateUI : GetFullComponent
{
    public ObjectData playerData;
    [Header("Health bar")]
    public bool isRun;
    int damgeRecive = 0;
    public Image healthBar;

    [Header("Ammo")]
    public Text ammoTxt;
    private void OnEnable()
    {
        damgeRecive = 0;
        healthBar.fillAmount = 1;
        PlayerCtl.playerHealthUpdateUI += UpdateHealthBar;
        PlayerGunSystem.updateAmmo += UpdateAmmo;
    }

    #region CaculaterHp
    public void UpdateHealthBar(int reciveDmg)
    {
        damgeRecive += reciveDmg;
        if (currentGameObject.activeSelf && !isRun)
        {
            isRun = true;
            StartCoroutine(SmoothBar());
        }
    }
    IEnumerator SmoothBar()
    {
        float cout = 0;
        float speed = 1 /(float) playerData.health; // speed will be 1 / max Hp of player beacause use img not use slider
        while (damgeRecive > 0 && healthBar.fillAmount > 0)
        {
            healthBar.fillAmount -= speed * Time.deltaTime;
            cout += speed * Time.deltaTime;
            if (cout >= 1)
            {
                damgeRecive--;
                cout = 0;
            }
            yield return null;
        }
        isRun = false;
        damgeRecive = 0;
    }
    #endregion
    #region Caculator ammo
    public void UpdateAmmo(int ammoNumber)
    {
        ammoTxt.text = ammoNumber.ToString();
    }

    #endregion

    [ContextMenu("Get all compose")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
       
    }
}
