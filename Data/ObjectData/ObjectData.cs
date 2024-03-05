using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectData")]
public class ObjectData : ScriptableObject
{
    public int health;
    public int amount;
    public float moveSpeed;
    public float sprintSpeed;

    public float visionRadious; // if object is AI
    public float shootingRadious;

    public float timeDestroy;

}
