using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;


public class JsonFileManager : MonoSingleTon<JsonFileManager>, IManager
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

        LoadAllData();
        //PrintAllData();
    }


    void LoadAllData()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        FileInfo[] files = directoryInfo.GetFiles("*.json");

        foreach (FileInfo file in files)
        {
            string content = File.ReadAllText(file.FullName);
            string dataType = Path.GetFileNameWithoutExtension(file.Name);

            try
            {
                switch (dataType)
                {
                    case "PlayerData":
                        List<PlayerData> PlayerDataList = JsonConvert.DeserializeObject<List<PlayerData>>(content);
                        this.playerDataList.AddRange(PlayerDataList);
                        break;
                    case "NPCData":
                        List<NPCData> NPCDataList = JsonConvert.DeserializeObject<List<NPCData>>(content);
                        this.npcDataList.AddRange(NPCDataList);
                        break;
                    case "WeaponData":
                        List<WeaponData> WeaponDataList = JsonConvert.DeserializeObject<List<WeaponData>>(content);
                        this.weapDataList.AddRange(WeaponDataList);
                        break;
                    case "BulletData":
                        List<BulletData> BulletDataList = JsonConvert.DeserializeObject<List<BulletData>>(content);
                        this.bulletDataList.AddRange(BulletDataList);
                        break;

                    case "VRDeviceData":
                        List<VRDeviceData> gunDataDataList = JsonConvert.DeserializeObject<List<VRDeviceData>>(content);
                        this.vrDeviceDataList.AddRange(gunDataDataList);
                        break;
                    default:
                        Debug.LogWarning($"Unsupported data type {dataType}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error processing file {file.Name}: {ex.Message}");
            }
        }
    }

    #region MyRegion

    public List<PlayerData> GetPlayerDataList()
    {
        return this.playerDataList;
    }
    public List<NPCData> GetNPCDataList() 
    {
        return this.npcDataList;
    }
    public List<WeaponData> GetWeaponDataList() 
    {
        return this.weapDataList;
    }
    public List<BulletData> GetBulletDataList() 
    {
        return this.bulletDataList; 
    }

    public List<VRDeviceData> GetVRDeviceDataList() 
    {
        return this.vrDeviceDataList;
    }

    #endregion

    void PrintAllData()
    {
        Debug.Log("Printing Player Data:");
        foreach (var player in playerDataList)
        {
            Debug.Log(JsonConvert.SerializeObject(player, Formatting.Indented));
        }


    }

}
