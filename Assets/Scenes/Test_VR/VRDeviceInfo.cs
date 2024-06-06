using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;
using Valve.VR;

public class VRDeviceInfo : MonoBehaviour
{

    private void Start()
    {
        //StartCoroutine(PrintAllXRDevices());

        StartCoroutine(PrintAllSteamVRDevices());
    }


    IEnumerator PrintAllXRDevices()
    {
        yield return new WaitForSeconds(2.0f);

        if (OpenVR.System == null)
        {
            Debug.LogError("OpenVR System not initialized. Make sure VR headset is connected and VR runtime is installed.");
            yield return null;
        }

        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
            if (deviceClass != ETrackedDeviceClass.Invalid)
            {
                //// 尝试获取设备的模型编号
                //string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String, ref error);
                //// 尝试获取设备的序列号
                //string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String, ref error);
                //Debug.Log("Device Index: " + i + " - Model Name: " + modelName + " - Serial Number: " + serialNumber);


                string result = "";

                // 尝试获取设备的模型编号
                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String, ref error);
                result += $"Driver modelName: {modelName}\n";

                // 尝试获取设备的序列号
                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String, ref error);
                result += $"Driver serialNumber: {serialNumber}\n";

                // 获取设备电量
                float batteryPercentage = OpenVR.System.GetFloatTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_DeviceBatteryPercentage_Float, ref error);
                if (error == ETrackedPropertyError.TrackedProp_Success)
                {
                    result += $"Battery Percentage: {batteryPercentage * 100}%";
                }
                else
                {
                    result += "Battery Percentage: N/A";
                }

                // 获取追踪系统名称
                string trackingSystemName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_TrackingSystemName_String, ref error);
                result += $"--Tracking System Name: {trackingSystemName}";

                // 获取驱动版本
                string driverVersion = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_DriverVersion_String, ref error);
                result += $"--Driver Version: {driverVersion}";

                // 获取硬件版本
                string hardwareRevision = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_HardwareRevision_String, ref error);
                result += $"Hardware Revision: {hardwareRevision}";

                // 获取固件版本
                string firmwareVersion = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_Firmware_ManualUpdateURL_String, ref error);
                result += $"--Firmware Version: {firmwareVersion}";

                // 获取设备类型
                string deviceType = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String, ref error);
                result += $"--Device Type: {deviceType}";

                Debug.Log(result);

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




    IEnumerator PrintAllSteamVRDevices() 
    {
        yield return new WaitForSeconds(2.0f);

        if (OpenVR.System == null)
        {
            Debug.LogError("OpenVR System not initialized. Make sure VR headset is connected and VR runtime is installed.");
            yield return null;
        }

        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
            if (deviceClass != ETrackedDeviceClass.Invalid)
            {
                // 获取设备信息
                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String);
                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String);
                string manufacturerName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ManufacturerName_String);
                string renderModelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String);
                string trackingSystemName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_TrackingSystemName_String);

                // 获取设备唯一标识符（例如，硬件修订或其他唯一属性）
                string uniqueId = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_HardwareRevision_String); // 这是一个示例属性，可以换成适合你的属性
                string deviceType = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String);

                Debug.Log("Device Index: " + i + " - Model Name: " + modelName + " - Serial Number: " + serialNumber  + " -- DeviceType: " + deviceType);
                //Debug.Log("Device ID: " + uniqueId + " - Manufacturer: " + manufacturerName);
                //Debug.Log("Render Model: " + renderModelName + " - Tracking System: " + trackingSystemName);

            }
        }
    }

    IEnumerator PrintAllSteamVRDevices2()
    {
        yield return new WaitForSeconds(2.0f);

        if (OpenVR.System == null)
        {
            Debug.LogError("OpenVR System not initialized. Make sure VR headset is connected and VR runtime is installed.");
            yield return null;
        }

        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
            if (deviceClass != ETrackedDeviceClass.Invalid)
            {
                // 获取设备信息
                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String);
                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String);
                string manufacturerName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ManufacturerName_String);
                string renderModelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String);
                string trackingSystemName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_TrackingSystemName_String);

                // 获取设备唯一标识符（例如，硬件修订或其他唯一属性）
                string uniqueId = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_HardwareRevision_String); 
                 // 获取设备类型
                string deviceType = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String);

                Debug.Log("Device Index: " + i + " - Model Name: " + modelName + " - Serial Number: " + " -- DeviceType: " + deviceType);
                Debug.Log("Device ID: " + uniqueId + " - Manufacturer: " + manufacturerName);
                Debug.Log("Render Model: " + renderModelName + " - Tracking System: " + trackingSystemName);

            }
        }
    }
    private string GetTrackedDeviceString(uint deviceId, ETrackedDeviceProperty prop)
    {
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        uint capacity = OpenVR.System.GetStringTrackedDeviceProperty(deviceId, prop, null, 0, ref error);
        if (capacity > 1)
        {
            var result = new StringBuilder((int)capacity);
            OpenVR.System.GetStringTrackedDeviceProperty(deviceId, prop, result, capacity, ref error);
            return result.ToString();
        }
        return string.Empty;
    }

}
