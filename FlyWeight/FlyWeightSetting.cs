using UnityEngine;

[CreateAssetMenu(menuName="FlyWeight/FlyWeight Setting" )]

public class FlyWeightSetting : ScriptableObject
{
    public float speed;
    public FlyWeight flyHeightRefabs;
    public float respawDelayTime;

}
