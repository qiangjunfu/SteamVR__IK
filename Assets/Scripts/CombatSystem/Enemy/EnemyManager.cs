using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleTon<EnemyManager>, IManager
{
    [SerializeField] Transform spawnPos;
    [SerializeField] List<GameObject> patrol_Waypoints = new List<GameObject>();
    [SerializeField] List<EnemyCtrl> enemyList = new List<EnemyCtrl>();
    [SerializeField, ReadOnly] int maxEnemyNum = 0;
    float timer = 0;
    float checkTime = 3;

    public List<GameObject> Patrol_Waypoints
    {
        get => patrol_Waypoints;
        //set => patrol_Waypoints = value;
    }



    public void Init()
    {
        GameObject waypoints = GameObject.Find("WayPoints");
        if (waypoints == null)
        {
            string path = "NPC/WayPoints";
            waypoints = AssetsLoadManager.Instance.LoadObject<GameObject>(path);
        }
        spawnPos = waypoints.transform;
        this.patrol_Waypoints = UnityTools.GetAllChildrenGameObject(waypoints);


        maxEnemyNum = ExcelFileManager.Instance.GetSettingDataList()[0].maxEnemyNum;
        if (maxEnemyNum > 0)
        {
            NPCData enemyData = ExcelFileManager.Instance.GetNPCDataList()[0];
            AddEnemy(enemyData, index);
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > checkTime)
        {
            timer = 0;

            if (enemyList.Count < maxEnemyNum)
            {
                NPCData enemyData = ExcelFileManager.Instance.GetNPCDataList()[0];
                AddEnemy(enemyData, index);
            }
        }
    }



    int index = 1;  //selfid
    public void AddEnemy(NPCData enemyData, int selfid)
    {
        Vector3 pos = spawnPos.position + RandomVector3(-1, 1, 0, 0, -1, 1);
        EnemyCtrl newPlayer = ComponentPoolManager.Instance.GetObject<EnemyCtrl>(enemyData.prePath, pos, Quaternion.identity);
        newPlayer.SetData(enemyData, index);

        enemyList.Add(newPlayer);

        index++;
    }

    Vector3 RandomVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);
        return new Vector3(x, y, z);
    }

    public void RemovePlayer(EnemyCtrl enemyCtrl)
    {
        if (enemyCtrl != null)
        {
            enemyList.Remove(enemyCtrl);
            ComponentPoolManager.Instance.RecycleObject<EnemyCtrl>(enemyCtrl);
            Debug.Log("EnemyÉ¾³ý³É¹¦:  " + enemyCtrl.name);

        }
        else
        {
            Debug.Log("EnemyÎ´ÕÒµ½:  " + enemyCtrl.name);
        }
    }

    public EnemyCtrl GetPlayer(string name)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].name == name)
            {
                return enemyList[i];
            }
        }

        return null;
    }

}
