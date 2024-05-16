using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BulletData
{
    [SerializeField, ReadOnly] public int id;
    [SerializeField, ReadOnly] public string name;
    [SerializeField, ReadOnly] public string prePath;
    [SerializeField, ReadOnly] public string type;
    [SerializeField, ReadOnly] public int speed;
    [SerializeField, ReadOnly] public int lifeTime;
    [SerializeField, ReadOnly] public int attack;
    [SerializeField, ReadOnly] public int fireRange;
    [SerializeField, ReadOnly] public string bulletHolePrePath;
    [SerializeField, ReadOnly] public string bulletHolePrePath_Chara;


    public BulletData() { }
}
