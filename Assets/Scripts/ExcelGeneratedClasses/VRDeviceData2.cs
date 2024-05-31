using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VRDeviceData2
{
    [SerializeField, ReadOnly] public int id;
    [SerializeField, ReadOnly] public string name;
    [SerializeField, ReadOnly] public string type;
    [SerializeField, ReadOnly] public string modelName_Head;
    [SerializeField, ReadOnly] public string serialNumber_Head;
    [SerializeField, ReadOnly] public string tracked_Object_Head;
    [SerializeField, ReadOnly] public string steamVR_TrackedObject_Head;
    [SerializeField, ReadOnly] public string modelName_LeftHand;
    [SerializeField, ReadOnly] public string serialNumber_LeftHand;
    [SerializeField, ReadOnly] public string tracked_Object_LeftHand;
    [SerializeField, ReadOnly] public string steamVR_TrackedObject_LeftHand;
    [SerializeField, ReadOnly] public string modelName_RightHand;
    [SerializeField, ReadOnly] public string serialNumber_RightHand;
    [SerializeField, ReadOnly] public string tracked_Object_RightHand;
    [SerializeField, ReadOnly] public string steamVR_TrackedObject_RightHand;
    [SerializeField, ReadOnly] public string modelName_LeftFoot;
    [SerializeField, ReadOnly] public string serialNumber_LeftFoot;
    [SerializeField, ReadOnly] public string tracked_Object_LeftFoot;
    [SerializeField, ReadOnly] public string steamVR_TrackedObject_LeftFoot;
    [SerializeField, ReadOnly] public string modelName_RightFoot;
    [SerializeField, ReadOnly] public string serialNumber_RightFoot;
    [SerializeField, ReadOnly] public string tracked_Object_RightFoot;
    [SerializeField, ReadOnly] public string steamVR_TrackedObject_RightFoot;
    [SerializeField, ReadOnly] public string modelName_Waist;
    [SerializeField, ReadOnly] public string serialNumber_Waist;
    [SerializeField, ReadOnly] public string tracked_Object_Waist;
    [SerializeField, ReadOnly] public string steamVR_TrackedObject_Waist;


    public VRDeviceData2() { }
}
