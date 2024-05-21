using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRDeviceInfo : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(PrintAllXRDevices());
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

}
