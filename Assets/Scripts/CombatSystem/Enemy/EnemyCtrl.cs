using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCtrl : ICharacter, IDamage
{
    [SerializeField] NPCData data;

    // ---- 绑定行为树
    [SerializeField] private Actions actions;
    [SerializeField] private BehaviorTree behaviorTree;
    [SerializeField] List<GameObject> Patrol_Waypoints;
    [SerializeField] private NPCState enemyState = NPCState.Stay;


    // ---- 绑定武器
    public Transform rightGunBone;
    public IWeapon weapon;


    #region MyRegion
    private void OnEnable()
    {
        MessageManager.AddListener<NPCState, int>(GameEventType.EnemyStateChange, OnEnemyStateChange);

        //if (actions == null) actions = GetComponent<Actions>();
        //actions.AddAnimationEvent(actions.GetAnimator(), "Fire SniperRifle", "StartFireEvent", 0);
    }
    private void OnDisable()
    {
        MessageManager.RemoveListener<NPCState, int>(GameEventType.EnemyStateChange, OnEnemyStateChange);

        //actions.CleanAllEvent(actions.GetAnimator());
    }

    private void OnEnemyStateChange(NPCState arg1, int arg2)
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


        enemyState = arg1;
        switch (arg1)
        {
            case NPCState.Stay:
                actions.Stay();
                break;
            case NPCState.Patrol:
                actions.Walk();
                break;
            case NPCState.Pursue:
                actions.Run();
                break;
            case NPCState.Attack:
                actions.Attack();
                break;
            case NPCState.Damage:
                break;
            case NPCState.Flee:
                actions.Run();
                break;
            default:
                break;
        }

    }

    private void StartFireEvent()
    {
        // Debug.Log("开始播放 Attack动画  selfid: " + data.selfid );

        //// 射击
        //if (weapon != null && fireCount >0 ) weapon.Shoot();

        //fireCount++;
    }
    #endregion



    public override int GetId()
    {
        return data.selfid;
    }
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
        data.flee_HP = enemyData.flee_HP;
        data.safe_Dis = enemyData.safe_Dis;


        InitData();

        WeaponData weaponData = ExcelFileManager.Instance.GetWeaponDataList()[0];
        SetWeapon(weaponData);
    }
    public void SetWeapon(WeaponData weaponData)
    {
        if (weapon == null)
        {
            Transform parent = rightGunBone.Find(weaponData.name);
            weapon = AssetsLoadManager.Instance.LoadComponent<IWeapon>(weaponData.prePath, parent );
        }
        weapon.SetData(weaponData, this);
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

            behaviorTree.SetVariable("SelfId", (SharedInt)(data.selfid));
            behaviorTree.SetVariable("WayPoints", (SharedGameObjectList)Patrol_Waypoints);
            behaviorTree.SetVariable("PatrolSpeed", (SharedFloat)(data.patrol_Speed));
            behaviorTree.SetVariable("PursueSpeed", (SharedFloat)(data.chase_Speed));
            behaviorTree.SetVariable("PursueDis", (SharedFloat)(data.chase_Dis));
            behaviorTree.SetVariable("ViewDis", (SharedFloat)(data.view_Dis));
            behaviorTree.SetVariable("AttackDis", (SharedFloat)(data.attack_Dis));
            behaviorTree.SetVariable("FleeHP", (SharedInt)(data.flee_HP));
            behaviorTree.SetVariable("SafeDis", (SharedFloat)(data.safe_Dis));
        }


        //WeaponData weaponData = JsonFileManager.Instance.GetWeaponDataList()[0];
        //if (weapon == null) weapon = AssetsLoadManager.Instance.LoadComponent<IWeapon>(weaponData.prePath, rightGunBone);
        //weapon.SetData(weaponData);

    }

    void Start()
    {
        //单独测试();

        InitData();
    }
    private void OnDestroy()
    {

    }


    void 单独测试()
    {

        NPCData enemyData = ExcelFileManager.Instance.GetNPCDataList()[0];
        SetData(enemyData, 1);
    }

    void Update()
    {
        if (enemyState == NPCState.Attack)
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



public enum NPCState
{
    Stay = 1,
    Patrol,
    Pursue,
    Attack,
    Damage,
    Flee,

}
