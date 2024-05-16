using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;

public class DeviceTrackManager : MonoBehaviour
{
    [SerializeField] bool isBind_SteamVR_TrackedObject = false;
    [SerializeField] SteamVR_TrackedObject trackedObject_Leg_L;
    [SerializeField] SteamVR_TrackedObject trackedObject_Leg_R;
    [SerializeField] SteamVR_TrackedObject trackedObject_Pelvis;
    [SerializeField] PlayerData playerData;
    [SerializeField] List<VRDeviceData> vrDeviceDataList;



    public void InitBind_DeviceTrack(PlayerData playerData)
    {
        this.playerData = playerData;
        vrDeviceDataList = ExcelFileManager.Instance.GetVRDeviceDataList();


        if (trackedObject_Pelvis == null)
        {
            List<SteamVR_TrackedObject> list = UnityTools.GetAllChildrenComponents<SteamVR_TrackedObject>(this.gameObject);
            for (int i = 0; i < list.Count; i++)
            {
                switch (list[i].gameObject.name)
                {
                    case "Leg_L":
                        trackedObject_Leg_L = list[i];
                        break;
                    case "Leg_R":
                        trackedObject_Leg_R = list[i];
                        break;
                    case "Pelvis":
                        trackedObject_Pelvis = list[i];
                        break;
                    default:
                        break;
                }
            }

        }


        if (OpenVR.System == null)
        {
            Debug.LogError("OpenVR System not initialized. Make sure VR headset is connected and VR runtime is installed.");
            return;
        }
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
            if (deviceClass != ETrackedDeviceClass.Invalid)
            {
                // 尝试获取设备的模型编号
                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String, ref error);
                // 尝试获取设备的序列号
                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String, ref error);

                Debug.Log("Device Index: " + i + " - Model Name: " + modelName + " - Serial Number: " + serialNumber);


                if (isBind_SteamVR_TrackedObject)
                {
                    BindTrackedObject(i, modelName, serialNumber);
                }
            }
        }
    }

    private string GetTrackedDeviceString(uint deviceId, ETrackedDeviceProperty prop, ref ETrackedPropertyError error)
    {
        uint capacity = OpenVR.System.GetStringTrackedDeviceProperty(deviceId, prop, null, 0, ref error);
        if (capacity > 1)
        {
            var result = new System.Text.StringBuilder((int)capacity);
            OpenVR.System.GetStringTrackedDeviceProperty(deviceId, prop, result, capacity, ref error);
            return result.ToString();
        }
        return "";
    }


    void BindTrackedObject(uint deviceId, string modelName, string serialNumber)
    {

        for (int i = 0; i < vrDeviceDataList.Count; i++)
        {
            if (vrDeviceDataList[i].serialNumber == serialNumber)
            {
                switch (vrDeviceDataList[i].tracked_Object)
                {
                    case "Leg_L":
                        trackedObject_Leg_L.index = GetDeviceIndex(vrDeviceDataList[i].steamVR_TrackedObject);
                        break;
                    case "Leg_R":
                        trackedObject_Leg_R.index = GetDeviceIndex(vrDeviceDataList[i].steamVR_TrackedObject);
                        break;
                    case "Pelvis":
                        trackedObject_Pelvis.index = GetDeviceIndex(vrDeviceDataList[i].steamVR_TrackedObject);
                        break;
                    default:
                        break;
                }
            }
        }

        //if (modelName == "AIO MN")
        //{
        //    switch (serialNumber)
        //    {
        //        case "3B-3BE00489":
        //            trackedObject_Pelvis.index = SteamVR_TrackedObject.EIndex.Device4;
        //            break;
        //        case "3B-3BE00500":
        //            trackedObject_Leg_R.index = SteamVR_TrackedObject.EIndex.Device3;
        //            break;
        //        case "3B-3BE00501":
        //            trackedObject_Leg_L.index = SteamVR_TrackedObject.EIndex.Device5;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //for (int i = 0; i < playerData.serialNumber.Length; i++)
        //{
        //    string _serialNumber = playerData.serialNumber[i];
        //    string _tracked_Object = playerData.tracked_Object[i];
        //    string steamVR_TrackedObject = playerData.steamVR_TrackedObject[i];

        //    if (_serialNumber == serialNumber)
        //    {
        //        switch (_tracked_Object)
        //        {
        //            case "Leg_L":
        //                trackedObject_Leg_L.index = GetDeviceIndex(steamVR_TrackedObject);
        //                break;
        //            case "Leg_R":
        //                trackedObject_Leg_R.index = GetDeviceIndex(steamVR_TrackedObject);
        //                break;
        //            case "Pelvis":
        //                trackedObject_Pelvis.index = GetDeviceIndex(steamVR_TrackedObject);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}


    }

    SteamVR_TrackedObject.EIndex GetDeviceIndex(string device)
    {
        SteamVR_TrackedObject.EIndex eIndex = SteamVR_TrackedObject.EIndex.None;
        switch (device)
        {
            case "Hmd":
                eIndex = SteamVR_TrackedObject.EIndex.Hmd;
                break;
            case "Device1":
                eIndex = SteamVR_TrackedObject.EIndex.Device1;
                break;
            case "Device2":
                eIndex = SteamVR_TrackedObject.EIndex.Device2;
                break;
            case "Device3":
                eIndex = SteamVR_TrackedObject.EIndex.Device3;
                break;
            case "Device4":
                eIndex = SteamVR_TrackedObject.EIndex.Device4;
                break;
            case "Device5":
                eIndex = SteamVR_TrackedObject.EIndex.Device5;
                break;
            default:
                break;
        }

        return eIndex;
    }

}
