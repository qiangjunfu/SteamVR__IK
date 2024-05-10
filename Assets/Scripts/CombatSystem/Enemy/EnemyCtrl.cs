using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCtrl : MonoBehaviour , IDamage
{
    [SerializeField] NPCData data;

    // ---- 绑定行为树
    [SerializeField] private Actions actions;
    [SerializeField] private BehaviorTree behaviorTree;
    [SerializeField] List<GameObject> Patrol_Waypoints;
    [SerializeField] private string enemyState;


    // ---- 绑定武器
    public Transform rightGunBone;
    public IWeapon weapon;


    #region MyRegion
    private void OnEnable()
    {
        MessageManager.AddListener<int, int>(GameEventType.EnemyStateChange, OnEnemyStateChange);

        //if (actions == null) actions = GetComponent<Actions>();
        //actions.AddAnimationEvent(actions.GetAnimator(), "Fire SniperRifle", "StartFireEvent", 0);
    }


    private void OnDisable()
    {
        MessageManager.RemoveListener<int, int>(GameEventType.EnemyStateChange, OnEnemyStateChange);

        //actions.CleanAllEvent(actions.GetAnimator());
    }
    private void OnEnemyStateChange(int arg1, int arg2)
    {
        // 1:Stay 
        // 2:Walk 
        // 3:Run 
        // 4:Sitting 
        // 5:Jump 
        // 6:Aiming 
        // 7:Attack 
        // 8:Damage 
        // 9:Death Reset 
        if (arg2 != data.selfid) return;
        if (actions == null) { actions = GetComponent<Actions>(); }


        string aniName = "";
        switch (arg1)
        {
            case 1:
                aniName = "Stay";
                actions.Stay();
                break;
            case 2:
                aniName = "Walk";
                actions.Walk();
                break;
            case 3:
                aniName = "Run";
                actions.Run();
                break;
            case 4:
                aniName = "Sitting";
                break;
            case 5:
                aniName = "Jump";
                break;
            case 6:
                aniName = "Aiming";
                break;
            case 7:
                aniName = "Attack";
                break;
            case 8:
                aniName = "Damage";
                break;
            case 9:
                aniName = "Death Reset";
                break;
            default:
                break;
        }
        //actions.SendMessage(aniName, SendMessageOptions.DontRequireReceiver);

        enemyState = aniName;
    }

    private void StartFireEvent()
    {
        // Debug.Log("开始播放 Attack动画  selfid: " + data.selfid );

        //// 射击
        //if (weapon != null && fireCount >0 ) weapon.Shoot();

        //fireCount++;
    }
    #endregion


    public NPCData GetData() { return data; }
    public void SetData(NPCData enemyData, int selfid)
    {
        data.id = enemyData.id;
        data.selfid = selfid;
        data.name = enemyData.name;
        data.type = enemyData.type;
        data.prePath = enemyData.prePath;
        data.hp = enemyData.hp;
        data.defence = enemyData.defence;
        data.speed = enemyData.speed;
        data.attack = enemyData.attack;
        data.attackSpeed = enemyData.attackSpeed;
        data.reloadTime = enemyData.reloadTime;
        data.patrol_Speed = enemyData.patrol_Speed;
        data.chase_Speed = enemyData.chase_Speed;
        data.view_Dis = enemyData.view_Dis;
        data.chase_Dis = enemyData.chase_Dis;
        data.attack_Dis = enemyData.attack_Dis;


        InitData();
    }

    private void InitData()
    {
        if (actions == null) actions = GetComponent<Actions>();
        if (behaviorTree == null) behaviorTree = GetComponent<BehaviorTree>();


        if (Patrol_Waypoints.Count == 0)
        {
            //GameObject parent = GameObject.Find("Patrol_Waypoints");
            //Patrol_Waypoints = UnityTools.GetAllChildrenGameObject(parent);
            Patrol_Waypoints = EnemyManager.Instance.Patrol_Waypoints;

            behaviorTree.SetVariable("Patrol_Waypoints", (SharedGameObjectList)Patrol_Waypoints);
            behaviorTree.SetVariable("Patrol_Speed", (SharedFloat)(data.patrol_Speed));
            behaviorTree.SetVariable("Chase_Speed", (SharedFloat)(data.chase_Speed));
            behaviorTree.SetVariable("View_Dis", (SharedFloat)(data.view_Dis));
            behaviorTree.SetVariable("Chase_Dis", (SharedFloat)(data.chase_Dis));
            behaviorTree.SetVariable("Attack_Dis", (SharedFloat)(data.attack_Dis));
        }


        WeaponData weaponData = JsonFileManager.Instance.GetWeaponDataList()[0];
        if (weapon == null) weapon = AssetsLoadManager.Instance.LoadComponent<IWeapon>(weaponData.prePath, rightGunBone);
        weapon.SetData(weaponData);

    }

    void Start()
    {
        InitData();

        //actions.AddAnimationEvent(actions.GetAnimator(), "Fire SniperRifle", "StartFireEvent", 0.05f);
    }
    private void OnDestroy()
    {
        //if (actions != null) actions.CleanAllEvent(actions.GetAnimator());
    }


    void Update()
    {
        if (enemyState == "Attack")
        {
            Attack();
        }
    }


    void Attack()
    {
        // 检查是否可以射击
        if (weapon != null && weapon.GetBulletCount() > 0 && weapon.IsCanShoot())
        {
            actions.Attack();
            weapon.Shoot();
        }
    }


    public void TakeDamage(int amount)
    {
        data.hp -= amount;
        if (data.hp <= 0)
        {
            Die();
        }

        EnemyManager.Instance.RemovePlayer(this);
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");

    }

}
