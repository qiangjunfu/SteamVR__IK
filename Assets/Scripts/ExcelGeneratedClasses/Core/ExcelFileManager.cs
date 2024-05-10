using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ExcelFileManager : MonoSingleTon<ExcelFileManager>, IManager
{
    [SerializeField, ReadOnly] string folderPath = ""; // JSON文件存放的文件夹路径
    [SerializeField] List<PlayerData> playerDataList = new List<PlayerData>();
    [SerializeField] List<NPCData> npcDataList = new List<NPCData>();
    [SerializeField] List<WeaponData> weapDataList = new List<WeaponData>();
    [SerializeField] List<BulletData> bulletDataList = new List<BulletData>();

    [SerializeField] List<VRDeviceData> vrDeviceDataList = new List<VRDeviceData>();


    public void Init()
    {
        folderPath = Path.Combine(Application.dataPath, "StreamingAssets/Jsons");

    }



}
