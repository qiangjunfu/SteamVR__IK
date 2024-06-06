using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Valve.VR;

public class DeviceTrackManager : MonoBehaviour
{
    [SerializeField] bool isBind_SteamVR_TrackedObject = false;
    [SerializeField] SteamVR_TrackedObject trackedObject_Leg_L;
    [SerializeField] SteamVR_TrackedObject trackedObject_Leg_R;
    [SerializeField] SteamVR_TrackedObject trackedObject_Pelvis;
    [SerializeField] PlayerData playerData;
    [SerializeField] List<VRDeviceData> vrDeviceDataList;
    [SerializeField] List<VRDeviceData2> vrDeviceDataList2;



    public void InitBind_DeviceTrack(PlayerData playerData)
    {
        this.playerData = playerData;
        vrDeviceDataList = ExcelFileManager.Instance.GetVRDeviceDataList();
        vrDeviceDataList2 = ExcelFileManager.Instance.GetVRDeviceDataList2();


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
                // 获取设备类型
                string deviceType = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String, ref error);

                Debug.Log("Device Index: " + i + " -- Model Name: " + modelName + " -- Serial Number: " + serialNumber + " -- DeviceType: " + deviceType);


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



    #region  修改为 VRDeviceData2 , 一个VRDeviceData2数据 包含整个vr头显信息
    public void InitBind_DeviceTrack2(PlayerData playerData)
    {
        this.playerData = playerData;
        vrDeviceDataList = ExcelFileManager.Instance.GetVRDeviceDataList();
        vrDeviceDataList2 = ExcelFileManager.Instance.GetVRDeviceDataList2();


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

        List<string> serialNumbers = new List<string>();
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
                // 获取设备类型
                string deviceType = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String, ref error);

                Debug.Log("Device Index: " + i + " -- Model Name: " + modelName + " -- Serial Number: " + serialNumber + " -- DeviceType: " + deviceType);
                /*
                 * 第二套头盔设备信息
                 * Device Index: 0 -- Model Name: Vive XR Streaming -- Serial Number: VIVE XR -- DeviceType: generic_hmd
                 * Device Index: 1 -- Model Name: vive_cosmos_controller -- Serial Number: CTL_RIGHT -- DeviceType: {htc_business_streaming}\rendermodels\vive_focus3_controller\vive_focus3_controller_right
                 * Device Index: 2 -- Model Name: vive_cosmos_controller -- Serial Number: CTL_LEFT -- DeviceType: {htc_business_streaming}\rendermodels\vive_focus3_controller\vive_focus3_controller_left
                 * Device Index: 3 -- Model Name: AIO MN -- Serial Number: 3B-3BD00294 -- DeviceType: {htc_business_streaming}\rendermodels\vive_ultimate_tracker  1
                 * Device Index: 4 -- Model Name: AIO MN -- Serial Number: 3B-3BD00752 -- DeviceType: {htc_business_streaming}\rendermodels\vive_ultimate_tracker  2
                 * Device Index: 5 -- Model Name: AIO MN -- Serial Number: 3B-3BD00802 -- DeviceType: {htc_business_streaming}\rendermodels\vive_ultimate_tracker  3
                 * */


                serialNumbers.Add(serialNumber);
            }
        }

        if (isBind_SteamVR_TrackedObject)
        {
            VRDeviceData2 vrDeviceData2 = ExcelFileManager.Instance.GetVRDeviceData2(serialNumbers.ToArray());
            if (vrDeviceData2 != null)
            {
                trackedObject_Leg_L.index = GetDeviceIndex(vrDeviceData2.steamVR_TrackedObject_LeftFoot);
                trackedObject_Leg_R.index = GetDeviceIndex(vrDeviceData2.steamVR_TrackedObject_RightFoot);
                trackedObject_Pelvis.index = GetDeviceIndex(vrDeviceData2.steamVR_TrackedObject_Waist);
            }
        }
    }

    #endregion

}


///// <summary>
///// VR设备硬件信息
///// </summary>
//public class VRDeviceData_Hardware
//{
//    [SerializeField, ReadOnly] public string modelName_Head;
//    [SerializeField, ReadOnly] public string serialNumber_Head;
//    [SerializeField, ReadOnly] public string modelName_LeftHand;
//    [SerializeField, ReadOnly] public string serialNumber_LeftHand;
//    [SerializeField, ReadOnly] public string modelName_RightHand;
//    [SerializeField, ReadOnly] public string serialNumber_RightHand;
//    [SerializeField, ReadOnly] public string modelName_LeftFoot;
//    [SerializeField, ReadOnly] public string serialNumber_LeftFoot;
//    [SerializeField, ReadOnly] public string modelName_RightFoot;
//    [SerializeField, ReadOnly] public string serialNumber_RightFoot;
//    [SerializeField, ReadOnly] public string modelName_Waist;
//    [SerializeField, ReadOnly] public string serialNumber_Waist;

//}


#region MyRegion
//using UnityEngine;
//using Valve.VR;

//public class VRDeviceInfo : MonoBehaviour
//{
//    void Start()
//    {

//        if (OpenVR.System == null)
//        {
//            Debug.LogError("OpenVR System not initialized. Make sure VR headset is connected and VR runtime is installed.");
//            return;
//        }
//        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
//        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
//        {
//            ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
//            if (deviceClass != ETrackedDeviceClass.Invalid)
//            {
//                string result = "";

//                // 尝试获取设备的模型编号
//                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String, ref error);
//                result += modelName + "\n";

//                // 尝试获取设备的序列号
//                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String, ref error);
//                result += serialNumber + "\n";

//                // 获取设备电量
//                float batteryPercentage = OpenVR.System.GetFloatTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_DeviceBatteryPercentage_Float, ref error);
//                if (error == ETrackedPropertyError.TrackedProp_Success)
//                {
//                    Debug.Log($"Battery Percentage: {batteryPercentage * 100}%");
//                }
//                else
//                {
//                    Debug.Log("Battery Percentage: N/A");
//                }

//                // 获取追踪系统名称
//                string trackingSystemName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_TrackingSystemName_String, ref error);
//                result.AppendLine($"Tracking System Name: {trackingSystemName}");

//                // 获取驱动版本
//                string driverVersion = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_DriverVersion_String, ref error);
//                result.AppendLine($"Driver Version: {driverVersion}");

//                // 获取硬件版本
//                string hardwareRevision = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_HardwareRevision_String, ref error);
//                result.AppendLine($"Hardware Revision: {hardwareRevision}");

//                // 获取固件版本
//                string firmwareVersion = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_Firmware_ManualUpdateURL_String, ref error);
//                result.AppendLine($"Firmware Version: {firmwareVersion}");

//                // 获取设备类型
//                string deviceType = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String, ref error);
//                result.AppendLine($"Device Type: {deviceType}");

//                Debug.Log("Device Index: " + i + " - Model Name: " + modelName + " - Serial Number: " + serialNumber);

//            }
//        }
//    }

//    private string GetTrackedDeviceString(uint deviceId, ETrackedDeviceProperty prop, ref ETrackedPropertyError error)
//    {
//        uint capacity = OpenVR.System.GetStringTrackedDeviceProperty(deviceId, prop, null, 0, ref error);
//        if (capacity > 1)
//        {
//            var result = new System.Text.StringBuilder((int)capacity);
//            OpenVR.System.GetStringTrackedDeviceProperty(deviceId, prop, result, capacity, ref error);
//            return result.ToString();
//        }
//        return "";
//    }

//}

#endregion