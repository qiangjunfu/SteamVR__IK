using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using UnityEngine;


public class ExcelFileManager : MonoSingleTon<ExcelFileManager>, IManager
{
    [SerializeField, ReadOnly] string folderPath = ""; 
    [SerializeField] List<PlayerData> playerDataList = new List<PlayerData>();
    [SerializeField] List<NPCData> npcDataList = new List<NPCData>();
    [SerializeField] List<WeaponData> weapDataList = new List<WeaponData>();
    [SerializeField] List<BulletData> bulletDataList = new List<BulletData>();

    [SerializeField] List<VRDeviceData> vrDeviceDataList = new List<VRDeviceData>();



    public void Init()
    {
        folderPath = Path.Combine(Application.dataPath, "StreamingAssets/Excels");
        ReadExcelFiles(folderPath);
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
                            List<VRDeviceData> gunDataDataList = JsonConvert.DeserializeObject<List<VRDeviceData>>(jsonData);
                            this.vrDeviceDataList.AddRange(gunDataDataList);
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
