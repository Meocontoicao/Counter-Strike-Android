using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUI : GetFullComponent
{
    public Text blueTeamText;
    public Text redTeamText;
    public Text noticeText;

    private static string _notice = "TEAM NEED 10 KILLS TO WIN";
    private static string _blueTeamWin = "BLUE TEAM WIN";
    private static string _redTeamWin = "RED TEAM WIN";

    private void Start()
    {
        UpdateContent(0);
    }
    public void UpdateBlueTeam(int number)
    {
        blueTeamText.text = number.ToString();
    }
    public void UpdateRedTeam(int number)
    {
        redTeamText.text = number.ToString();
    }
    public void UpdateContent(int indext)
    {
        switch (indext)
        {
            case 0:
                noticeText.text = _notice;
                break;
            case 1:
                noticeText.text = _blueTeamWin;
                break;
            case 2:
                noticeText.text = _redTeamWin;
                break;
        }
    }
    [ContextMenu("Get all compose")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
    }
}
