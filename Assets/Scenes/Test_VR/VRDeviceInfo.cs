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
                //// ���Ի�ȡ�豸��ģ�ͱ��
                //string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String, ref error);
                //// ���Ի�ȡ�豸�����к�
                //string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String, ref error);
                //Debug.Log("Device Index: " + i + " - Model Name: " + modelName + " - Serial Number: " + serialNumber);


                string result = "";

                // ���Ի�ȡ�豸��ģ�ͱ��
                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String, ref error);
                result += $"Driver modelName: {modelName}\n";

                // ���Ի�ȡ�豸�����к�
                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String, ref error);
                result += $"Driver serialNumber: {serialNumber}\n";

                // ��ȡ�豸����
                float batteryPercentage = OpenVR.System.GetFloatTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_DeviceBatteryPercentage_Float, ref error);
                if (error == ETrackedPropertyError.TrackedProp_Success)
                {
                    result += $"Battery Percentage: {batteryPercentage * 100}%";
                }
                else
                {
                    result += "Battery Percentage: N/A";
                }

                // ��ȡ׷��ϵͳ����
                string trackingSystemName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_TrackingSystemName_String, ref error);
                result += $"--Tracking System Name: {trackingSystemName}";

                // ��ȡ�����汾
                string driverVersion = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_DriverVersion_String, ref error);
                result += $"--Driver Version: {driverVersion}";

                // ��ȡӲ���汾
                string hardwareRevision = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_HardwareRevision_String, ref error);
                result += $"Hardware Revision: {hardwareRevision}";

                // ��ȡ�̼��汾
                string firmwareVersion = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_Firmware_ManualUpdateURL_String, ref error);
                result += $"--Firmware Version: {firmwareVersion}";

                // ��ȡ�豸����
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
                // ��ȡ�豸��Ϣ
                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String);
                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String);
                string manufacturerName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ManufacturerName_String);
                string renderModelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String);
                string trackingSystemName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_TrackingSystemName_String);

                // ��ȡ�豸Ψһ��ʶ�������磬Ӳ���޶�������Ψһ���ԣ�
                string uniqueId = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_HardwareRevision_String); // ����һ��ʾ�����ԣ����Ի����ʺ��������
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
                // ��ȡ�豸��Ϣ
                string modelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ModelNumber_String);
                string serialNumber = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_SerialNumber_String);
                string manufacturerName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_ManufacturerName_String);
                string renderModelName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_RenderModelName_String);
                string trackingSystemName = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_TrackingSystemName_String);

                // ��ȡ�豸Ψһ��ʶ�������磬Ӳ���޶�������Ψһ���ԣ�
                string uniqueId = GetTrackedDeviceString(i, ETrackedDeviceProperty.Prop_HardwareRevision_String); 
                 // ��ȡ�豸����
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
