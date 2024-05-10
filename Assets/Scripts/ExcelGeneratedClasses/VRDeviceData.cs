using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VRDeviceData
{
    [SerializeField, ReadOnly] public int id;
    [SerializeField, ReadOnly] public string name;
    [SerializeField, ReadOnly] public string type;
    [SerializeField, ReadOnly] public string modelName;
    [SerializeField, ReadOnly] public string serialNumber;
    [SerializeField, ReadOnly] public string tracked_Object;
    [SerializeField, ReadOnly] public string steamVR_TrackedObject;


    public VRDeviceData() { }
}
