using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver_Track : MonoBehaviour
{
    [SerializeField] Track__Type solverTrackType = Track__Type.None;
    [SerializeField] Vector3 pos = Vector3.zero;
    [SerializeField] Vector3 rot;

    public Track__Type GetSolverTrackType() { return solverTrackType; }


    public void Bind_VRTrack(VR_Track vr_Track, bool worldPositionStays = false)
    {
        this.transform.SetParent(vr_Track.transform, worldPositionStays);

        //this.transform.localPosition = pos;  //¡Ÿ ±≤‚ ‘
        //if (solverTrackType == Track__Type.VRCamera)
        //{
        //    this.transform.localPosition += new Vector3(0, 0, -0.15f);
        //}
    }
    public void Bind_VRTrack(VR_Track vr_Track, Vector3 localPos, Vector3 localRot, bool worldPositionStays)
    {
        this.transform.SetParent(vr_Track.transform, worldPositionStays);
        //this.transform.localPosition = localPos;
        ////this.transform.localEulerAngles = localRot;

    }
}
