using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GunData")]
public class GunData : ScriptableObject
{
    public int mag;
    public float fireCharge;
    public int maximumAmuation;
    public float realoadingTime;
    public int giveDamage;
    public AudioClip audioClipName;
}
