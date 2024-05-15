using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;


public class PlayerVR : ICharacter, IDamage
{
    [SerializeField] private PlayerData data;
    [SerializeField] PlayerEntity playerEntity;
    [SerializeField] DeviceTrackManager deviceTrackManager;
    [SerializeField] List<VR_Track> vr_trackList = new List<VR_Track>();
    [SerializeField] List<Solver_Track> solverTrackList = new List<Solver_Track>();

    [SerializeField] IWeapon weapon;
    [SerializeField] Transform weaponParent;


    public void SetData(PlayerData playerData)
    {
        data.id = playerData.id;
        data.selfid = playerData.selfid;
        data.name = playerData.name;
        data.type = playerData.type;
        data.prePath = playerData.prePath;
        data.hp = playerData.hp;
        data.defence = playerData.defence;
        data.speed = playerData.speed;
        data.attack = playerData.attack;
        data.attackSpeed = playerData.attackSpeed;
        data.reloadTime = playerData.reloadTime;


        WeaponData weaponData = ExcelFileManager.Instance.GetWeaponDataList()[0];
        SetWeapon(weaponData);
    }
    public void SetWeapon(WeaponData weaponData)
    {
        if (weapon == null)
        {
            weapon = AssetsLoadManager.Instance.LoadComponent<IWeapon>(weaponData.prePath, weaponParent);
        }
        weapon.SetData(weaponData, this);
    }



    void Start()
    {
        if (deviceTrackManager == null) deviceTrackManager = GetComponent<DeviceTrackManager>();
        //if (playerEntity == null) playerEntity = AssetsLoadManager.Instance.LoadComponent<PlayerEntity>("VR/PlayerEntity");
        if (playerEntity == null) playerEntity = transform.Find("PlayerEntity").GetComponent<PlayerEntity>();
        if (vr_trackList.Count == 0) vr_trackList = UnityTools.GetAllChildrenComponents<VR_Track>(this.gameObject);


        PlayerData playerData  = ExcelFileManager.Instance.GetPlayerDataList()[0];
        SetData(playerData);
        deviceTrackManager.InitBind_DeviceTrack(data);


        //playerEntity.SetData(data);
        //solverTrackList = playerEntity.GetSolverTrackList();
        //for (int i = 0; i < vr_trackList.Count; i++)
        //{
        //    VRTrack_Bind_SolverTrack(vr_trackList[i]);
        //}


   

        // 扳机
        SteamVR_Actions.default_GrabPinch.onChange += Default_GrabPinch_onChange;
    }

    private void Default_GrabPinch_onChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        log.LogFormat("Default_GrabPinch_onChange: {0} -- {1} ", fromAction.activeDevice, newState);

        //按下扳机  开火
        if (newState && fromAction.activeDevice.ToString() == "RightHand")
        {
           Shoot();
        }

    }



    void VRTrack_Bind_SolverTrack(VR_Track vr_Track)
    {
        for (int i = 0; i < solverTrackList.Count; i++)
        {
            if (vr_Track.GetVRTrackType() == solverTrackList[i].GetSolverTrackType())
            {
                solverTrackList[i].Bind_VRTrack(vr_Track);
            }

        }
    }

    void Update()
    {

    }




    public void Shoot()
    {
        if (weapon != null && weapon.GetBulletCount() > 0 && weapon.IsCanShoot())
        {
            weapon.Shoot();
        }
    }

    public void ContinueShoot()
    {

    }
    public void TakeDamage(int amount)
    {
        data.hp -= amount;
        if (data.hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");

    }

}


