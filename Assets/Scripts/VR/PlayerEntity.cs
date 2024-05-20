using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    [SerializeField] private VRIK vrik;
    [SerializeField] List<Solver_Track> solverTrackList = new List<Solver_Track>();
    [SerializeField] bool isBindTrackLeg = true;
    [SerializeField, ReadOnly] float originalHeight = 190f;
    [SerializeField, ReadOnly] float scaleRatio = 1;


    public List<Solver_Track> GetSolverTrackList()
    {
        return solverTrackList;
    }
    public void SetData(PlayerData playerData)
    {
        this.data = playerData;

        scaleRatio = RoundToDecimalPlaces(data.playerheight / originalHeight, 3);
        transform.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);


        InitData();
    }



    float weight0 = 0.0f;
    float weight1 = 1f;
    public void InitData()
    {
        if (vrik != null) { vrik = GetComponent<VRIK>(); }
        if (solverTrackList.Count == 0)
        {
            solverTrackList = UnityTools.GetAllChildrenComponents<Solver_Track>(this.gameObject);
        }


        //if (weapon == null)
        //{
        //    weaponName = "Weapons/Gun/MP-40";
        //    weapon = AssetsLoadManager.Instance.LoadComponent<IWeapon>(weaponName, weaponParent);
        //    weapon.transform.localPosition = Vector3.zero;
        //    weapon.transform.localRotation = Quaternion.identity;
        //}


        //if (isBindTrackLeg)
        //{
        //    vrik.solver.spine.pelvisPositionWeight = weight1;
        //    vrik.solver.spine.pelvisRotationWeight = weight1;

        //    vrik.solver.leftLeg.positionWeight = weight1;
        //    vrik.solver.leftLeg.rotationWeight = weight1;

        //    vrik.solver.rightLeg.positionWeight = weight1;
        //    vrik.solver.rightLeg.rotationWeight = weight1;
        //}
        //else
        //{
        //    vrik.solver.spine.pelvisPositionWeight = weight0;
        //    vrik.solver.spine.pelvisRotationWeight = weight0;

        //    vrik.solver.leftLeg.positionWeight = weight0;
        //    vrik.solver.leftLeg.rotationWeight = weight0;

        //    vrik.solver.rightLeg.positionWeight = weight0;
        //    vrik.solver.rightLeg.rotationWeight = weight0;
        //}
    }


    // 保留指定位数的小数的方法
    private float RoundToDecimalPlaces(float value, int decimalPlaces)
    {
        float multiplier = Mathf.Pow(10, decimalPlaces);
        return Mathf.Round(value * multiplier) / multiplier;
    }

}
