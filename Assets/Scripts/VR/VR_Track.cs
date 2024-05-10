using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Track : MonoBehaviour
{
    [SerializeField] Track__Type vrTrackType = Track__Type.None; 


    public Track__Type GetVRTrackType() {  return vrTrackType; }


}

public enum Track__Type 
{
    None,
    VRCamera,
    LeftHand,
    RightHand,
    Leg_L,
    Leg_R,
    Pelvis
}