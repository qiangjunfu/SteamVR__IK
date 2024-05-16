using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeaponData
{
    [SerializeField, ReadOnly] public int id;
    [SerializeField, ReadOnly] public string name;
    [SerializeField, ReadOnly] public string prePath;
    [SerializeField, ReadOnly] public string type;
    [SerializeField, ReadOnly] public int attack;
    [SerializeField, ReadOnly] public int maxAmmo;
    [SerializeField, ReadOnly] public int reloadTime;
    [SerializeField, ReadOnly] public int recoil;
    [SerializeField, ReadOnly] public int fireRate;
    [SerializeField, ReadOnly] public int fireRange;
    [SerializeField, ReadOnly] public string bulletPrePath;
    [SerializeField, ReadOnly] public string flashPrePath;
    [SerializeField, ReadOnly] public string audioPrePath;


    public WeaponData() { }
}
