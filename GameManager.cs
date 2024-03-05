using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GetFullComponent
{

    private static GameManager _instances;
    public static GameManager Instances => _instances;
    private void OnEnable()
    {
        UnitCtl.addEnemyUnit += UpdateEnemyUnit;
        UnitCtl.addPlayerUnit += UpdatePlayerUnit;
        UnitCtl.reducEnemyUnit += DeductEnemyUnit;
        UnitCtl.reducPlayerUnit += DeductPlyerUnit;
    }

    private void Awake()
    {
        if (_instances != null && _instances != this)
        {
            Destroy(this);
        }
        else
        {
            _instances = this;
        }
    }
    [Header("Game logic about team")]
    public int blueTeam = 0;
    public int redTeam = 0;
    public ScoreUI scoreUI;

    public bool isWin = false;
    #region Score
    void UpdateEnemyUnit()
    {
        redTeam++;
        scoreUI.UpdateRedTeam(redTeam);
      
    }
    void UpdatePlayerUnit()
    {
        blueTeam++;
        scoreUI.UpdateBlueTeam(blueTeam);
    }
    void DeductEnemyUnit()
    {
        redTeam--;
        if (redTeam <= 0)
        {
            redTeam = 0;
            scoreUI.UpdateContent(2);
        }
        scoreUI.UpdateRedTeam(redTeam);

    }
    void DeductPlyerUnit()
    {
        blueTeam--;
        if (blueTeam <= 0)
        {
            blueTeam = 0;
            scoreUI.UpdateContent(1);
        }
        scoreUI.UpdateBlueTeam(blueTeam);

    }
    #endregion

    [ContextMenu("Get all compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        scoreUI = GameObject.FindObjectOfType<ScoreUI>();
    }

}
