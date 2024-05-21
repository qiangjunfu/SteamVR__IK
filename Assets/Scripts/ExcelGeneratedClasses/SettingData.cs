using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SettingData
{
    [SerializeField, ReadOnly] public int id;
    [SerializeField, ReadOnly] public bool showLog;
    [SerializeField, ReadOnly] public int[] resolution;
    [SerializeField, ReadOnly] public bool fullScreen;
    [SerializeField, ReadOnly] public string language;
    [SerializeField, ReadOnly] public string quality;
    [SerializeField, ReadOnly] public int volume;
    [SerializeField, ReadOnly] public float viewDistance;
    [SerializeField, ReadOnly] public float fieldOfView;
    [SerializeField, ReadOnly] public bool vsync;
    [SerializeField, ReadOnly] public int maxEnemyNum;


    public SettingData() { }
}
