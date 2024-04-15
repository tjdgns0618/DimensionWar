using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower",menuName ="Scriptble Object/Tower   Data")]
public class Tower_Data : ScriptableObject
{
    public enum TowerType { Melee, Range}

    [Header("# Main Info")]
    public TowerType tower_Type;
    public int tower_Id;
    public string tower_Name;

    [TextArea]
    public string towerDesc;
    public GameObject TowerObj;


    [Header("# Tower")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;


    [Header("# Skill")]
    public GameObject projectile;
    public Sprite skillEffect;
}
