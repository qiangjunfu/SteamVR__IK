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
    [SerializeField] PlayerHitCollder playerHitCollder;


    [SerializeField] IWeapon weapon;
    [SerializeField] Transform weaponParent;


    public override int GetId()
    {
        return data.selfid;
    }
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
        data.playerEntityPath = playerData.playerEntityPath;    
        data.playerheight = playerData.playerheight;    


        WeaponData weaponData = ExcelFileManager.Instance.GetWeaponDataList()[0];
        SetWeapon(weaponData);
    }
    public void SetWeapon(WeaponData weaponData)
    {
        if (weapon == null)
        {
            Transform parent = weaponParent.Find(weaponData.name);
            weapon = AssetsLoadManager.Instance.LoadComponent<IWeapon>(weaponData.prePath, parent);
        }
        weapon.SetData(weaponData, this);
    }



    public int HP_GetSet(int? value = null)
    {
        if (value.HasValue)
        {
            data.hp = value.Value;
        }

        return data.hp;
    }




    void Start()
    {
        if (deviceTrackManager == null) deviceTrackManager = GetComponent<DeviceTrackManager>();
        if (playerEntity == null) playerEntity = transform.Find("PlayerEntity").GetComponent<PlayerEntity>();
        if (playerHitCollder == null) playerHitCollder = transform.Find("PlayerHitCollder").GetComponent<PlayerHitCollder>();
        if (vr_trackList.Count == 0) vr_trackList = UnityTools.GetAllChildrenComponents<VR_Track>(this.gameObject);


        PlayerData playerData = ExcelFileManager.Instance.GetPlayerDataList()[0];
        SetData(playerData);
        deviceTrackManager.InitBind_DeviceTrack(data);


        playerEntity.SetData(data);
        //solverTrackList = playerEntity.GetSolverTrackList();
        //for (int i = 0; i < vr_trackList.Count; i++)
        //{
        //    VRTrack_Bind_SolverTrack(vr_trackList[i]);
        //}

        playerHitCollder.SetData(this);



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
        int hp = HP_GetSet() - amount;
        HP_GetSet (hp);
    }
    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");

    }

}


