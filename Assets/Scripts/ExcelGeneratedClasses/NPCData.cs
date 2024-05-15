using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NPCData
{
    [SerializeField, ReadOnly] public int id;
    [SerializeField, ReadOnly] public int selfid;
    [SerializeField, ReadOnly] public string name;
    [SerializeField, ReadOnly] public string type;
    [SerializeField, ReadOnly] public string prePath;
    [SerializeField, ReadOnly] public int hp;
    [SerializeField, ReadOnly] public int defence;
    [SerializeField, ReadOnly] public int speed;
    [SerializeField, ReadOnly] public int attack;
    [SerializeField, ReadOnly] public float attackSpeed;
    [SerializeField, ReadOnly] public float reloadTime;
    [SerializeField, ReadOnly] public int patrol_Speed;
    [SerializeField, ReadOnly] public int chase_Speed;
    [SerializeField, ReadOnly] public int view_Dis;
    [SerializeField, ReadOnly] public int chase_Dis;
    [SerializeField, ReadOnly] public int attack_Dis;
    [SerializeField, ReadOnly] public int flee_HP;
    [SerializeField, ReadOnly] public int safe_Dis;


    public NPCData() { }
}
