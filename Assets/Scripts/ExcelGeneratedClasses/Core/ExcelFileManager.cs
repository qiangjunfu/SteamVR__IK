using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


public class ExcelFileManager : MonoSingleTon<ExcelFileManager>, IManager
{
    [SerializeField, ReadOnly] string folderPath = "";
    [SerializeField] List<PlayerData> playerDataList = new List<PlayerData>();
    [SerializeField] List<NPCData> npcDataList = new List<NPCData>();
    [SerializeField] List<WeaponData> weapDataList = new List<WeaponData>();
    [SerializeField] List<BulletData> bulletDataList = new List<BulletData>();

    [SerializeField] List<VRDeviceData> vrDeviceDataList = new List<VRDeviceData>();
    [SerializeField] List<VRDeviceData2> vrDeviceData2List = new List<VRDeviceData2>();
    [SerializeField] List<SettingData> settingDataList = new List<SettingData>();



    public void Init()
    {
        folderPath = Path.Combine(Application.dataPath, "StreamingAssets/Excels");
        ReadExcelFiles(folderPath);
    }


    #region MyRegion

    public List<PlayerData> GetPlayerDataList()
    {
        return new List<PlayerData>(this.playerDataList);
    }
    public List<NPCData> GetNPCDataList()
    {
        return new List<NPCData>(this.npcDataList);
    }
    public List<WeaponData> GetWeaponDataList()
    {
        return new List<WeaponData>(this.weapDataList);
    }
    public List<BulletData> GetBulletDataList()
    {
        return new List<BulletData>(this.bulletDataList);
    }

    public List<VRDeviceData> GetVRDeviceDataList()
    {
        return new List<VRDeviceData>(this.vrDeviceDataList);
    }
    public List<VRDeviceData2> GetVRDeviceDataList2()
    {
        return new List<VRDeviceData2>(this.vrDeviceData2List);
    }

    /// <summary>
    /// 通过传入设备序列号 获取对应的设备配置表信息
    /// </summary>
    public VRDeviceData2 GetVRDeviceData2(params string[] serialNumbers)
    {
        HashSet<string> serialNumberSet = new HashSet<string>(serialNumbers);

        foreach (var device in vrDeviceData2List)
        {
            HashSet<string> deviceSerialNumbers = new HashSet<string>
            {
                device.serialNumber_Head,
                device.serialNumber_LeftHand,
                device.serialNumber_RightHand,
                device.serialNumber_LeftFoot,
                device.serialNumber_RightFoot,
                device.serialNumber_Waist
            };

            //if (serialNumberSet.Overlaps(deviceSerialNumbers))   ////传入的任意一个序列号存在于设备对象的序列号集合
            if (serialNumberSet.IsSubsetOf(deviceSerialNumbers))   ////传入的所有序列号都必须存在于设备对象的序列号集合中 
            {
                return device;
            }
        }

        return null; 
    }

    public List<SettingData> GetSettingDataList()
    {
        return new List<SettingData>(this.settingDataList);
    }

    #endregion


    private void ReadExcelFiles(string path)
    {
        string[] filePaths = Directory.GetFiles(path, "*.xlsx", SearchOption.AllDirectories);
        foreach (string filePath in filePaths)
        {
            ProcessExcelFile(filePath);
        }
    }
    private void ProcessExcelFile(string filePath)
    {
        Debug.Log($"Reading Excel file: {filePath}");
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                foreach (DataTable table in result.Tables)
                {
                    string dataType = Path.GetFileNameWithoutExtension(filePath);
                    //Type type = Type.GetType($"{dataType}");
                    Debug.Log($"Processing {table.TableName} , {dataType}");
                    var list = ConvertDataTableToList(table, Path.GetFileNameWithoutExtension(filePath));
                    var jsonData = JsonConvert.SerializeObject(list, Formatting.Indented);
                    //Debug.Log(jsonData);

                    switch (dataType)
                    {
                        case "PlayerData":
                            List<PlayerData> PlayerDataList = JsonConvert.DeserializeObject<List<PlayerData>>(jsonData);
                            this.playerDataList.AddRange(PlayerDataList);
                            break;
                        case "NPCData":
                            List<NPCData> NPCDataList = JsonConvert.DeserializeObject<List<NPCData>>(jsonData);
                            this.npcDataList.AddRange(NPCDataList);
                            break;
                        case "WeaponData":
                            List<WeaponData> WeaponDataList = JsonConvert.DeserializeObject<List<WeaponData>>(jsonData);
                            this.weapDataList.AddRange(WeaponDataList);
                            break;
                        case "BulletData":
                            List<BulletData> BulletDataList = JsonConvert.DeserializeObject<List<BulletData>>(jsonData);
                            this.bulletDataList.AddRange(BulletDataList);
                            break;

                        case "VRDeviceData":
                            List<VRDeviceData> VRDeviceDataList = JsonConvert.DeserializeObject<List<VRDeviceData>>(jsonData);
                            this.vrDeviceDataList.AddRange(VRDeviceDataList);
                            break;
                        case "VRDeviceData2":
                            List<VRDeviceData2> VRDeviceData2List = JsonConvert.DeserializeObject<List<VRDeviceData2>>(jsonData);
                            this.vrDeviceData2List.AddRange(VRDeviceData2List);
                            break;
                        case "SettingData":
                            List<SettingData> SettingDataList = JsonConvert.DeserializeObject<List<SettingData>>(jsonData);
                            this.settingDataList.AddRange(SettingDataList);
                            break;
                        default:
                            Debug.LogWarning($"Unsupported data type {dataType}");
                            break;
                    }



                }
            }
        }
    }

    //   Type type = Type.GetType($"{className}");
    private List<object> ConvertDataTableToList(DataTable table, string className)
    {
        List<object> list = new List<object>();
        //// 替换为你的类的命名空间和名称
        //Type type = Type.GetType($"YourNamespace.{className}", true);
        Type type = Type.GetType($"{className}", true);

        if (type == null)
        {
            Debug.LogError($"Class not found for file/class: {className}");
            return list;
        }

        foreach (DataRow row in table.Rows)
        {
            var instance = Activator.CreateInstance(type);
            foreach (DataColumn column in table.Columns)
            {
                // 尝试获取属性，如果失败则获取字段
                MemberInfo member = type.GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                                ?? (MemberInfo)type.GetField(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (member is PropertyInfo propertyInfo)
                {
                    SetMemberValue(propertyInfo, instance, row[column]);
                }
                else if (member is FieldInfo fieldInfo)
                {
                    SetMemberValue(fieldInfo, instance, row[column]);
                }
                else
                {
                    Debug.LogError($"No field or property found for column: {column.ColumnName}");
                }
            }
            list.Add(instance);
        }

        return list;
    }
    // 通用设置成员值的方法
    private void SetMemberValue(MemberInfo member, object instance, object value)
    {
        if (value != DBNull.Value)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var propertyInfo = (PropertyInfo)member;
                if (propertyInfo.PropertyType.IsArray)
                {
                    SetPropertyArrayValue(propertyInfo, instance, value.ToString());
                }
                else
                {
                    propertyInfo.SetValue(instance, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                }
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                var fieldInfo = (FieldInfo)member;
                if (fieldInfo.FieldType.IsArray)
                {
                    SetFieldArrayValue(fieldInfo, instance, value.ToString());
                }
                else
                {
                    fieldInfo.SetValue(instance, Convert.ChangeType(value, fieldInfo.FieldType));
                }
            }
        }
        else
        {
            Debug.Log($"Null value for {member.Name} in row.");
        }
    }
    private void SetMemberValue2(MemberInfo member, object instance, object value)
    {
        if (value != DBNull.Value)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var propertyInfo = (PropertyInfo)member;
                if (propertyInfo.PropertyType.IsArray)
                {
                    SetPropertyArrayValue(propertyInfo, instance, value.ToString());
                }
                else
                {
                    // 检查属性类型是否为 float 或 double，并进行适当的转换
                    if (propertyInfo.PropertyType == typeof(float))
                    {
                        propertyInfo.SetValue(instance, Convert.ToSingle(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(double))
                    {
                        propertyInfo.SetValue(instance, Convert.ToDouble(value), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(instance, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                    }
                }
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                var fieldInfo = (FieldInfo)member;
                if (fieldInfo.FieldType.IsArray)
                {
                    SetFieldArrayValue(fieldInfo, instance, value.ToString());
                }
                else
                {
                    // 检查字段类型是否为 float 或 double，并进行适当的转换
                    if (fieldInfo.FieldType == typeof(float))
                    {
                        fieldInfo.SetValue(instance, Convert.ToSingle(value));
                    }
                    else if (fieldInfo.FieldType == typeof(double))
                    {
                        fieldInfo.SetValue(instance, Convert.ToDouble(value));
                    }
                    else
                    {
                        fieldInfo.SetValue(instance, Convert.ChangeType(value, fieldInfo.FieldType));
                    }
                }
            }
        }
        else
        {
            Debug.Log($"Null value for {member.Name} in row.");
        }
    }


    private void SetPropertyArrayValue(PropertyInfo propertyInfo, object instance, string stringValue)
    {
        Type elementType = propertyInfo.PropertyType.GetElementType();
        string[] stringValues = stringValue.Split(',');
        Array array = Array.CreateInstance(elementType, stringValues.Length);
        for (int i = 0; i < stringValues.Length; i++)
        {
            array.SetValue(Convert.ChangeType(stringValues[i].Trim(), elementType), i);
        }
        propertyInfo.SetValue(instance, array, null);
    }

    private void SetFieldArrayValue(FieldInfo fieldInfo, object instance, string stringValue)
    {
        Type elementType = fieldInfo.FieldType.GetElementType();
        string[] stringValues = stringValue.Split(',');
        Array array = Array.CreateInstance(elementType, stringValues.Length);
        for (int i = 0; i < stringValues.Length; i++)
        {
            array.SetValue(Convert.ChangeType(stringValues[i].Trim(), elementType), i);
        }
        fieldInfo.SetValue(instance, array);
    }


}
